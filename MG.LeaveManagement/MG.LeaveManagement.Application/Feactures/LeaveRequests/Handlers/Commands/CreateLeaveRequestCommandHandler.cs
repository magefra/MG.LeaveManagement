using AutoMapper;
using MediatR;
using MG.LeaveManagement.Application.Dtos.LeaveRequest.Validators;
using MG.LeaveManagement.Application.Persistence.Contracts;
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
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(ILeaveRequestRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            var response = new BaseCommandResponse();
            var validator = new CreateLeaveRequestDtoValidators(_leaveTypeRepository);
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

            return response;
        }
    }
}
