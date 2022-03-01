using MediatR;
using MG.LeaveManagement.Application.Dtos.LeaveAllocation;
using MG.LeaveManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveAllocations.Request.Commands
{
    public class CreateLeaveAllocationCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveAllocationDto LeaveAllocationDto { get; set; }


    }
}




