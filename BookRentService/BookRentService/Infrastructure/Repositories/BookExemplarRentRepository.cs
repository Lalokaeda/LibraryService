using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookRentService.Application.Exceptions;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BookRentService.Infrastructure.Repositories
{
    public class BookExemplarRentRepository : IBaseRepository<BookExemplarRent>
    {
        private readonly BookRentDbContext _context;

        public BookExemplarRentRepository(BookRentDbContext context)
        {
            _context=context;
        }

        public async Task AddAsync(BookExemplarRent bookExemplarRent)
        {
          try
            {
                _context.BookExemplarRents.Add(bookExemplarRent);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRentRepository.AddAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при добавлении книг в аренду!");
            }   
        }

        public async Task DeleteAsync(int Id)
        {
             try
            {
                var bookExemplarRent = await _context.BookExemplarRents.FindAsync(Id);

                _context.Remove(bookExemplarRent);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRentRepository.DeleteAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при удалении книги из аренды!");
            }
        }

        public async Task<IEnumerable<BookExemplarRent>> GetAllAsync()
        {
            try
            {
                return await _context.BookExemplarRents.Include(x=>x.BookRent).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRentRepository.GetAllAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка книг в арендах!");
            }
        }

        public async Task<IEnumerable<BookExemplarRent>> GetByConditionAsync(Expression<Func<BookExemplarRent, bool>> expression)
        {
            
             try
            {
                return await _context.BookExemplarRents.Include(x=> x.BookRent).Where(expression).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRentRepository.GetByConditionAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка книг в арендах!");
            }
        }

        public async Task<BookExemplarRent?> GetByIdAsync(int Id)
        {
           try
            {
                return await _context.BookExemplarRents.Include(x=>x.BookRent)
                                        .FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRentRepository.GetByIdAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка книг в аренде!");
            }
        }

        public async Task<IEnumerable<BookExemplarRent>> GetSortedAsync(Expression<Func<BookExemplarRent, object>> sortExpression, bool descending = false)
        {
            try
            {
                var query = _context.BookExemplarRents.Include(x=>x.BookRent).AsQueryable();
                if (descending)
                    query = query.OrderByDescending(sortExpression);
                else
                    query = query.OrderBy(sortExpression);
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRentRepository.GetSortedAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка книг в арендах!");
            }
        }

        public async Task UpdateAsync(BookExemplarRent bookExemplarRent)
        {
            try
            {
                _context.Update(bookExemplarRent);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookExemplarRentRepository.UpdateAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при обновлении книг в аренде!");
            }
        }
    }
}