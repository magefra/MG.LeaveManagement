using AutoMapper;
using MediatR;
using MG.LeaveManagement.Application.Dtos.LeaveRequest.Validators;
using MG.LeaveManagement.Application.Exceptions;
using MG.LeaveManagement.Application.Persistence.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, Unit>
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository,
            IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var leaveAllocation = await _leaveRequestRepository.Get(request.Id);

            if (request.LeaveRequestDto != null)
            {
                var validator = new UpdateLeaveRequestDtoValidator(_leaveRequestRepository);
                var validationResult = await validator.ValidateAsync(request.LeaveRequestDto);
                if (validationResult.IsValid == false)
                    throw new ValidationException(validationResult);


                _mapper.Map(request.LeaveRequestDto, leaveAllocation);

                await _leaveRequestRepository.Update(leaveAllocation);
            }
            else if(request.ChangeLeaveRequestApprovalDto != null)
            {
                
                await _leaveRequestRepository.ChangeApprovalStatus(leaveAllocation, request.ChangeLeaveRequestApprovalDto.Approved);
            }
            
           
            return Unit.Value;
        }
    }
}
