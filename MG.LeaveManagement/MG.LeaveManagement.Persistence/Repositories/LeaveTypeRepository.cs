using MG.LeaveManagement.Application.Persistence.Contracts;
using MG.LeaveManagement.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
