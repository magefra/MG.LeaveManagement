using MG.LeaveManagement.Application.Contracts.Persistence;
using MG.LeaveManagement.Domain;

namespace MG.LeaveManagement.Persistence.Repositories
{
    public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
    {
        private readonly MgLeaveManagementDbContext _dbContext;

        public LeaveTypeRepository(MgLeaveManagementDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
