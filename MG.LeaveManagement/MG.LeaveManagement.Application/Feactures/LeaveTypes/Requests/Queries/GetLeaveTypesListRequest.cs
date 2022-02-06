using MediatR;
using MG.LeaveManagement.Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Feactures.LeaveTypes.Requests.Queries
{
    public class GetLeaveTypesListRequest : IRequest<List<LeaveTypeDto>>
    {

    }
}
