using AutoMapper;
using MediatR;
using MG.LeaveManagement.Application.Contracts.Persistence;
using MG.LeaveManagement.Application.Dtos.LeaveRequest.Validators;
using MG.LeaveManagement.Application.Exceptions;
using System.Threading;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
       
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveRequest= await _unitOfWork.LeaveRequestRepository.Get(request.Id);

            if (request.LeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_unitOfWork.LeaveTypeRepository);
                var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
                if (validationResult.IsValid == false)
                    throw new ValidationException(validationResult);
                _mapper.Map(request.LeaveRequestDto, leaveRequest);


                await _unitOfWork.LeaveRequestRepository.Update(leaveRequest);
            }
            else if(request.ChangeLeaveRequestApprovalDto != null)
            {

                await _unitOfWork.LeaveRequestRepository.ChangeApprovalStatus(leaveRequest, request.ChangeLeaveRequestApprovalDto.Approved);
                if (request.ChangeLeaveRequestApprovalDto.Approved.Value)
                {
                    var allocation = await _unitOfWork.LeaveAllocationRepository.GetUserAllocations(leaveRequest.RequestingEmployeeId, leaveRequest.LeaveTypeId);
                    int daysRequested = (int)(leaveRequest.EndDate - leaveRequest.StartDate).TotalDays;

                    allocation.NumberOfDays -= daysRequested;

                    await _unitOfWork.LeaveAllocationRepository.Update(allocation);
                }

            }


            return Unit.Value;
        }
    }
}
