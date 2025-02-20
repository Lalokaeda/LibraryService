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
    public class BookRentRepository :IBaseRepository<BookRent>
    {
        private readonly BookRentDbContext _context;

        public BookRentRepository(BookRentDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(BookRent bookRent)
        {
            try
            {
                _context.BookRents.Add(bookRent);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRentRepository.AddAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при добавлении записи об аренде!");
            }
        }

        public async Task DeleteAsync(int Id)
        {
            try
            {
                var bookRent = await _context.BookRents.FindAsync(Id);

                _context.Remove(bookRent);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRentRepository.DeleteAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при удалении записи об аренде!");
            }
        }

        public async Task<IEnumerable<BookRent>> GetAllAsync()
        {
            try
            {
                return await _context.BookRents.Include(x => x.Renter).Include(x => x.BookExemplarRents).Include(x=>x.RentStatus).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRentRepository.GetAllAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка записей об арендах!");
            }
        }

        public async Task<IEnumerable<BookRent>> GetByConditionAsync(Expression<Func<BookRent, bool>> expression)
        {
             try
            {
                return await _context.BookRents.Include(x => x.Renter)
                                        .Include(x => x.BookExemplarRents).Include(x=>x.RentStatus).Where(expression).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRentRepository.GetByConditionAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка записей об арендах!");
            }
        }

        public async Task<BookRent?> GetByIdAsync(int Id)
        {
            try
            {
                return await _context.BookRents.Include(x => x.Renter)
                                        .Include(x => x.BookExemplarRents)
                                        .Include(x=>x.RentStatus)
                                        .FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRentRepository.GetByIdAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении записи об аренде!");
            }
        }

        public async Task<IEnumerable<BookRent>> GetSortedAsync(Expression<Func<BookRent, object>> sortExpression, bool descending = false)
        {
            try
            {
                var query = _context.BookRents.Include(x => x.Renter)
                                        .Include(x => x.BookExemplarRents)
                                        .Include(x=>x.RentStatus)
                                        .AsQueryable();
                if (descending)
                    query = query.OrderByDescending(sortExpression);
                else
                    query = query.OrderBy(sortExpression);
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRentRepository.GetSortedAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка записей об арендах!");
            }
        }

        public async Task UpdateAsync(BookRent bookRent)
        {
           try
            {
                _context.Update(bookRent);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRentRepository.UpdateAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при обновлении записи об аренде!");
            }
        }

        public async Task DeleteRangeAsync(int[] Ids)
        {
            try
            {
                var bookRents = await _context.BookRents.Where(x=>Ids.Contains(x.Id)).ToListAsync();

                _context.RemoveRange(bookRents);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[BookRentRepository.DeleteRangeAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при удалении записей об аренде!");
            }
        }
    }
}