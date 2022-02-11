using MediatR;
using MG.LeaveManagement.Application.Dtos.LeaveAllocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveAllocations.Request.Commands
{
    public class CreateLeaveAllocationCommand : IRequest<int>
    {
        public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }


    }
}




