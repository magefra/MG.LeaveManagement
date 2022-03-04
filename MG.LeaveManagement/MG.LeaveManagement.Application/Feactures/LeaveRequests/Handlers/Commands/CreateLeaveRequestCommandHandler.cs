using AutoMapper;
using MediatR;
using MG.LeaveManagement.Application.Constants;
using MG.LeaveManagement.Application.Contracts.Infraestructure;
using MG.LeaveManagement.Application.Contracts.Persistence;
using MG.LeaveManagement.Application.Dtos.LeaveRequest.Validators;
using MG.LeaveManagement.Application.Models;
using MG.LeaveManagement.Application.Responses;
using MG.LeaveManagement.Domain;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
      
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateLeaveRequestCommandHandler(IUnitOfWork unitOfWork,
            IEmailSender emailSender,
            IHttpContextAccessor httpContextAccessor,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidators(_unitOfWork.LeaveTypeRepository);
            var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
            var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(
                   q => q.Type == CustomClaimTypes.Uid)?.Value;



            var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(userId, request.LeaveRequestDto.LeaveTypeId);
            if (allocation is null)
            {
                validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(nameof(request.LeaveRequestDto.LeaveTypeId),
                    "You do not have any allocations for this leave type."));
            }
            else
            {
                int daysRequested = (int)(request.LeaveRequestDto.EndDate - request.LeaveRequestDto.StartDate).TotalDays;
                if (daysRequested > allocation.NumberOfDays)
                {
                    validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure(
                        nameof(request.LeaveRequestDto.EndDate), "You do not have enough days for this request"));
                }
            }



            if (validationResult.IsValid == false)
            {
                response.Success = false;
                response.Message = "Request Failed";
                response.Errors = validationResult.Errors.Select(q => q.ErrorMessage).ToList();
            }
            else
            {
                var leaveRequest = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);
                leaveRequest.RequestingEmployeeId = userId;
                leaveRequest = await _unitOfWork.LeaveRequestRepository.Add(leaveRequest);
                //await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Request Created Successfully";
                response.Id = leaveRequest.Id;

                try
                {
                    var emailAddress = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value;

                    var email = new Email
                    {
                        To = emailAddress,
                        Body = $"Your leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} " +
                        $"has been submitted successfully.",
                        Subject = "Leave Request Submitted"
                    };

                    await _emailSender.SendEmail(email);
                }
                catch (Exception ex)
                {
                    //// Log or handle error, but don't throw...
                }
            }

            return response;
        }
    }
}
