using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MultiCam.Domain.Repository
{
    public interface IRepository<Type>
    {
        Task<Type> GetByIdAsync(int id);
        Task<IEnumerable<Type>> GetAllAsync();
        Task<IEnumerable<Type>> FindAsync(Func<Type, bool> predicate);

        Task InsertAsync(Type entity);
        Task UpdateAsync(Type entity);
        Task DeleteAsync(int id);
    }
}
