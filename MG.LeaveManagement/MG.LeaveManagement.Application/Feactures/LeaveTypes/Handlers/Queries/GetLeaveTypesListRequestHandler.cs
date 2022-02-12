using AutoMapper;
using MediatR;
using MG.LeaveManagement.Application.Contracts.Persistence;
using MG.LeaveManagement.Application.Dtos;
using MG.LeaveManagement.Application.Feactures.LeaveTypes.Requests.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;


namespace MG.LeaveManagement.Application.Feactures.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypesListRequestHandler : IRequestHandler<GetLeaveTypesListRequest, List<LeaveTypeDto>>
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        private readonly IMapper _mapper;

        public GetLeaveTypesListRequestHandler(ILeaveTypeRepository leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesListRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _leaveTypeRepository.GetAll();

            return _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
        }
    }
}
