using MediatR;
using MG.LeaveManagement.Application.Dtos;
using MG.LeaveManagement.Application.Dtos.LeaveType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveTypes.Requests.Commands
{
    public class CreateLeaveTypeCommand : IRequest<int>
    {
        public CreateLeaveTypeDtp LeaveTypeDto { get; set; }
    }
}
