using System.Linq.Expressions;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LibraryService.Infrastructure.Repositories
{
    public class BookRepository : IBaseRepository<Book>
    {
        private readonly LibraryDbContext _context;

        public BookRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }

        public async Task AddAsync(Book book)
        {
            _context.Books.Add(book);
            foreach (var author in book.Authors)
            {
                _context.Entry(author).State = EntityState.Unchanged; 
            }

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int Id)
        {
            var book = await _context.Books.FindAsync(Id);

            _context.Remove(book);
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            return await _context.Books.Include(x => x.Authors).Include(x => x.BookExemplars).ToListAsync();
        }

        public async Task<IEnumerable<Book>> GetByConditionAsync(Expression<Func<Book, bool>> expression)
        {
            return await _context.Books.Include(x => x.Authors)
                                        .Include(x => x.BookExemplars).Where(expression).ToListAsync();
        }

        public async Task<Book?> GetByIdAsync(int Id)
        {
            return await _context.Books.Include(x => x.Authors)
                                        .Include(x => x.BookExemplars)
                                        .FirstOrDefaultAsync(x => x.Id == Id);
        }

        public async Task<IEnumerable<Book>> GetSortedAsync(Expression<Func<Book, object>> sortExpression, bool descending = false)
        {
            var query = _context.Books.Include(x => x.Authors)
                                        .Include(x => x.BookExemplars)
                                        .AsQueryable();
            if (descending)
                query = query.OrderByDescending(sortExpression);
            else
                query = query.OrderBy(sortExpression);
            return await query.ToListAsync();
        }

        public async Task UpdateAsync(Book book)
        {
            _context.Update(book);
             foreach (var author in book.Authors)
            {
                _context.Entry(author).State = EntityState.Unchanged; 
            }
            await _context.SaveChangesAsync();
        }
    }
}