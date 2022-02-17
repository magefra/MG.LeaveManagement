using MG.LeaveManagement.MVC.Models;
using MG.LeaveManagement.MVC.Services.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MG.LeaveManagement.MVC.Contracts
{
    public interface IleaveTypeService
    {
        Task<List<LeaveTypeVM>> GetLeaveTypes();
        Task<LeaveTypeVM> GetLeaveTypeDetails(int id);
        Task<Response<int>> CreateLeaveType(CreateLeaveTypeVM leaveType);
        Task<Response<int>> UpdateLeaveType(int id, LeaveTypeVM leaveType);
        Task<Response<int>> DeleteLeaveType(int id);
    }
}
