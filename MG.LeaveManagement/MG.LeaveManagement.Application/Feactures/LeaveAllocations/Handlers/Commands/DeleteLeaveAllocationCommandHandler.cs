using AutoMapper;
using MediatR;
using MG.LeaveManagement.Application.Exceptions;
using MG.LeaveManagement.Application.Feactures.LeaveAllocations.Request.Commands;
using MG.LeaveManagement.Application.Persistence.Contracts;
using MG.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand>
    {
        private readonly ILeaveAllocationRepository _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var leaveType = await _leaveAllocationRepository.Get(request.Id);

            if (leaveType == null)
                throw new NotFoundException(nameof(LeaveAllocation), request.Id);

            await _leaveAllocationRepository.Delete(leaveType);

            return Unit.Value;
        }
    }
}
