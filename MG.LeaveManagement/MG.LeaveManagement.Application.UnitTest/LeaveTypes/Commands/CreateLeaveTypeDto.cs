using MG.LeaveManagement.Application.Dtos.LeaveType;

namespace MG.LeaveManagement.Application.UnitTest.LeaveTypes.Commands
{
    internal class CreateLeaveTypeDto : CreateLeaveTypeDtp
    {
        public int DefaultDays { get; set; }
        public string Name { get; set; }
    }
}