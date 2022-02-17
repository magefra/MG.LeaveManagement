using MG.LeaveManagement.MVC.Services.Base;
using System.Threading.Tasks;

namespace MG.LeaveManagement.MVC.Contracts
{
    public interface ILeaveAllocationService
    {
        Task<Response<int>> CreateLeaveAllocations(int leaveTypeId);
    }
}
