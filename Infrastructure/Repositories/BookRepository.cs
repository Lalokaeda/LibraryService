using System.Linq.Expressions;
using LibraryService.Application.Exceptions;
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
            try
            {
                _context.Books.Add(book);
                foreach (var author in book.Authors)
                {
                    _context.Entry(author).State = EntityState.Unchanged;
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRepository.AddAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при добавлении книги!");
            }
        }

        public async Task DeleteAsync(int Id)
        {
            try
            {
                var book = _context.Books.FindAsync(Id);

                _context.Remove(book);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRepository.DeleteAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при удалении книги!");
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            try
            {
                return await _context.Books.Include(x => x.Authors).Include(x => x.BookExemplars).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRepository.GetAllAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка книг!");
            }
        }

        public async Task<IEnumerable<Book>> GetByConditionAsync(Expression<Func<Book, bool>> expression)
        {
            try
            {
                return await _context.Books.Include(x => x.Authors)
                                        .Include(x => x.BookExemplars).Where(expression).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRepository.GetByConditionAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка книг!");
            }
        }

        public async Task<Book?> GetByIdAsync(int Id)
        {
            try
            {
                return await _context.Books.Include(x => x.Authors)
                                        .Include(x => x.BookExemplars)
                                        .FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRepository.GetByIdAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении книги!");
            }
        }

        public async Task<IEnumerable<Book>> GetSortedAsync(Expression<Func<Book, object>> sortExpression, bool descending = false)
        {
            try
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
            catch (Exception e)
            {
                Console.WriteLine("[BookRepository.GetSortedAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка книг!");
            }
        }

        public async Task UpdateAsync(Book book)
        {
            try
            {
                _context.Update(book);
                foreach (var author in book.Authors)
                {
                    _context.Entry(author).State = EntityState.Unchanged;
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRepository.UpdateAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при обновлении книги!");
            }
        }
    }
}