using MediatR;
using MG.LeaveManagement.Application.Dtos;
using MG.LeaveManagement.Application.Dtos.LeaveType;
using MG.LeaveManagement.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveTypes.Requests.Commands
{
    public class CreateLeaveTypeCommand : IRequest<BaseCommandResponse>
    {
        public CreateLeaveTypeDtp LeaveTypeDto { get; set; }
    }
}
