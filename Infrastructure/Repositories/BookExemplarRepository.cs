using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using LibraryService.Application.Exceptions;

namespace LibraryService.Infrastructure.Repositories
{
    public class BookExemplarRepository : IBaseRepository<BookExemplar>
    {
        private readonly LibraryDbContext _context;

        public BookExemplarRepository(LibraryDbContext libraryDbContext)
        {
            _context = libraryDbContext;
        }

        public async Task AddAsync(BookExemplar bookExemplar)
        {
            try
            {
                _context.BookExemplars.Add(bookExemplar);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRepository.AddAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при добавлении экземпляра книги!");
            }
        }

        public async Task DeleteAsync(int Id)
        {
            try
            {
                var bookExemplar = await _context.BookExemplars.FindAsync(Id);
                _context.BookExemplars.Remove(bookExemplar);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRepository.DeleteAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при удалении экземпляра книги!");
            }
        }

        public async Task<IEnumerable<BookExemplar>> GetAllAsync()
        {
            try
            {
                return await _context.BookExemplars.Include(x => x.Book).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRepository.GetAll]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка экземпляров книги!");
            }
        }

        public async Task<IEnumerable<BookExemplar>> GetByConditionAsync(Expression<Func<BookExemplar, bool>> expression)
        {
            try
            {
                return await _context.BookExemplars.Include(x => x.Book).Where(expression).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRepository.GetByConditionAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка экземпляров книги!");
            }
        }

        public async Task<BookExemplar?> GetByIdAsync(int Id)
        {
            try
            {
                return await _context.BookExemplars.Where(x=>x.Id==Id).Include(x => x.Book).ThenInclude(x => x.Authors).FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRepository.GetByIdAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении экземпляра книги!");
            }
        }

        public async Task<IEnumerable<BookExemplar>> GetSortedAsync(Expression<Func<BookExemplar, object>> sortExpression, bool descending = false)
        {
            try
            {
                var query = _context.BookExemplars.Include(x => x.Book).ThenInclude(x => x.Authors).AsQueryable();

                if (descending)
                    query = query.OrderByDescending(sortExpression);
                else
                    query = query.OrderBy(sortExpression);
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRepository.GetSortedAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка экземпляров книги!");
            }
        }

        public async Task UpdateAsync(BookExemplar bookExemplar)
        {
            try
            {
                _context.Update(bookExemplar);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRepository.UpdateAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при обновлении экземпляра книги!");
            }

        }
    }
}