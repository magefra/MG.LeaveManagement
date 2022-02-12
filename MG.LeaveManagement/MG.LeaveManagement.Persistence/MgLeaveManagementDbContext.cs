using MG.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Persistence
{
    public class MgLeaveManagementDbContext : DbContext
    {
        public MgLeaveManagementDbContext(DbContextOptions<MgLeaveManagementDbContext> options): 
            base(options) 
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MgLeaveManagementDbContext).Assembly);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return base.SaveChangesAsync(cancellationToken);
        }



        public DbSet<Domain.LeaveRequest> LeaveRequests { get; set; }

        public DbSet<LeaveType>  LeaveTypes { get; set; }

        public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    }
}
