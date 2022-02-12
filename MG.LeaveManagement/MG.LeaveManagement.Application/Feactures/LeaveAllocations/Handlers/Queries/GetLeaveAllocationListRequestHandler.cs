using AutoMapper;
using MediatR;
using MG.LeaveManagement.Application.Contracts.Persistence;
using MG.LeaveManagement.Application.Dtos.LeaveAllocation;
using MG.LeaveManagement.Application.Dtos.LeaveAllocation.Request.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Dtos.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, List<LeaveAllocationDto>>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public GetLeaveAllocationListRequestHandler(ILeaveAllocationRepository leaveAllocationRepository,
             IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            var leaveAllocations = new List<MG.LeaveManagement.Domain.LeaveAllocation>();
            var allocations = new List<LeaveAllocationDto>();


            leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetails();
            allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);


            return allocations;
        }
    }
}
