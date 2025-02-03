using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;

namespace LibraryService.Infrastructure.Repositories
{
    public class AuthorRepository : IBaseRepository<Author>
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context=context;
        }

        public Task AddAsync(Author entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Author>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Author>> GetByConditionAsync(Expression<Func<Author, bool>> expression)
        {
             return await _context.Authors.Where(expression).ToListAsync();
        }

        public async Task<Author?> GetByIdAsync(int Id)
        {
            return await _context.Authors
                                        .FirstOrDefaultAsync(x => x.Id==Id);
        }

        public Task<IEnumerable<Author>> GetSortedAsync(Expression<Func<Author, object>> sortExpression, bool descending = false)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Author entity)
        {
            throw new NotImplementedException();
        }
    }
}