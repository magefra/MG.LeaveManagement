using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Dtos.LeaveType
{
    public class CreateLeaveTypeDtp
    {
        public string Name { get; set; }

        public int DefaultDays { get; set; }
    }
}
