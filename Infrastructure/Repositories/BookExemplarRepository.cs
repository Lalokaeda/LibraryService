using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;

namespace LibraryService.Infrastructure.Repositories
{
    public class BookExemplarRepository : IBaseRepository<BookExemplar>
    {
        private readonly LibraryDbContext _context;

        public BookExemplarRepository(LibraryDbContext libraryDbContext)
        {
            _context=libraryDbContext;
        }

        public async Task AddAsync(BookExemplar bookExemplar)
        {
            _context.BookExemplars.Add(bookExemplar);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var bookExemplar = _context.BookExemplars.FindAsync(Id);
            _context.Remove(bookExemplar);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<BookExemplar>> GetAllAsync()
        {
           return await _context.BookExemplars.Include(x=>x.Book).ToListAsync();
        }

        public async Task<IEnumerable<BookExemplar>> GetByConditionAsync(Expression<Func<BookExemplar, bool>> expression)
        {
           return await _context.BookExemplars.Include(x=>x.Book).Where(expression).ToListAsync();
        }

        public async Task<BookExemplar?> GetByIdAsync(int Id)
        {
           return await  _context.BookExemplars.Include(x=>x.Book).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<BookExemplar>> GetSortedAsync(Expression<Func<BookExemplar, object>> sortExpression, bool descending = false)
        {
            var query = _context.BookExemplars.Include(x=>x.Book).ThenInclude(x=>x.Authors).AsQueryable();

            if (descending)
                query=query.OrderByDescending(sortExpression);
            else
                query=query.OrderBy(sortExpression);
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(BookExemplar bookExemplar)
        {
            _context.Update(bookExemplar);
            await _context.SaveChangesAsync();
        }
    }
}