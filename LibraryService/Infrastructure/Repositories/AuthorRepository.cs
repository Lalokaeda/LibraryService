using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using LibraryService.Application.Exceptions;

namespace LibraryService.Infrastructure.Repositories
{
    public class AuthorRepository : IBaseRepository<Author>
    {
        private readonly LibraryDbContext _context;

        public AuthorRepository(LibraryDbContext context)
        {
            _context = context;
        }

        public Task AddAsync(Author entity)
        {
            throw new NotImplementedException("Функционал не реализован!");
        }

        public Task DeleteAsync(int Id)
        {
            throw new NotImplementedException("Функционал не реализован!");
        }

        public Task<IEnumerable<Author>> GetAllAsync()
        {
            throw new NotImplementedException("Функционал не реализован!");
        }

        public async Task<IEnumerable<Author>> GetByConditionAsync(Expression<Func<Author, bool>> expression)
        {
            try
            {
                return await _context.Authors.Where(expression).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[AuthorRepository.GetByConditionAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка авторов!");
            }
        }

        public async Task<Author?> GetByIdAsync(int Id)
        {
            try
            {
                return await _context.Authors.FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("[AuthorRepository.GetByIdAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении автора!");
            }
        }

        public Task<IEnumerable<Author>> GetSortedAsync(Expression<Func<Author, object>> sortExpression, bool descending = false)
        {
            throw new NotImplementedException("Функционал не реализован!");
        }

        public Task UpdateAsync(Author entity)
        {
            throw new NotImplementedException("Функционал не реализован!");
        }
    }
}