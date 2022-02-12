using MG.LeaveManagement.Application.Persistence.Contracts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MgLeaveManagementDbContext _mgLeaveManagementDbContext;

        public GenericRepository(MgLeaveManagementDbContext mgLeaveManagementDbContext)
        {
            _mgLeaveManagementDbContext = mgLeaveManagementDbContext;
        }

        public async Task<T> Add(T entity)
        {
            await _mgLeaveManagementDbContext.AddAsync(entity);
            await _mgLeaveManagementDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(T id)
        {
            _mgLeaveManagementDbContext.Set<T>().Remove(id);
            await _mgLeaveManagementDbContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var entity = await Get(id);
            return entity != null;
        }

        public async Task<T> Get(int id)
        {
            return await _mgLeaveManagementDbContext.Set<T>().FindAsync(id);
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _mgLeaveManagementDbContext.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
             _mgLeaveManagementDbContext.Entry(entity).State = EntityState.Modified;
            await _mgLeaveManagementDbContext.SaveChangesAsync();
        }

      
    }
}
