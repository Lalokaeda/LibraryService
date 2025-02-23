using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookRentService.Domain.Entities;

namespace BookRentService.Domain.Interfaces
{
    public interface IFineRepository : IBaseRepository<Fine>
    {
        Task<bool> ExistsAsync(Expression<Func<Fine, bool>> expression);    
    }
}