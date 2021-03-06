using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MG.LeaveManagement.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(int id);

        Task<IReadOnlyList<T>> GetAll();

        Task<T> Add(T entity);

        Task Update(T entity);

        Task<bool> Exists(int id);

        Task Delete(T id);

    }
}
