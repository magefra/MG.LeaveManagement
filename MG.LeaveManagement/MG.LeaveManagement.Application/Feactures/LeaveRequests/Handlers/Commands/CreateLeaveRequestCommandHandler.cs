using AutoMapper;
using MediatR;
using MG.LeaveManagement.Application.Contracts.Infraestructure;
using MG.LeaveManagement.Application.Contracts.Persistence;
using MG.LeaveManagement.Application.Dtos.LeaveRequest.Validators;
using MG.LeaveManagement.Application.Models;
using MG.LeaveManagement.Application.Responses;
using MG.LeaveManagement.Domain;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly ILeaveRequestRepository _leaveTypeRepository;
        private readonly ILeaveTypeRepository leaveTypeRepository1;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveTypeRepository,
            ILeaveTypeRepository leaveTypeRepository1,
            IEmailSender emailSender,
            IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            this.leaveTypeRepository1 = leaveTypeRepository1;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidators(leaveTypeRepository1);
            var validatorResult = await validator.ValidateAsync(request.LeaveRequestDto);

            if (!validatorResult.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatorResult.Errors.Select(x => x.ErrorMessage).ToList();
            }


            var leaveType = _mapper.Map<LeaveRequest>(request.LeaveRequestDto);

            leaveType = await _leaveTypeRepository.Add(leaveType);


            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveType.Id;


            var email = new Email
            {
                To = "magefra9@hotmail.com",
                Body = $"Your leave request for {request.LeaveRequestDto.StartDate:D} to {request.LeaveRequestDto.EndDate:D} " +
                        $"has been submitted successfully.",
                Subject = "Leave Request Submitted"
            };

            try
            {
                await _emailSender.SendEmail(email);
            }
            catch (System.Exception ex)
            {

                throw;
            }


            return response;
        }
    }
}
