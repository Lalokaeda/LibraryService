using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BookRentService.Domain.Interfaces
{
     public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int Id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int Id);
        Task DeleteRangeAsync(int[] Ids); 
        Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetSortedAsync(Expression<Func<T, object>> sortExpression, bool descending = false);
    }
}