using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BookRentService.Application.Exceptions;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;

namespace BookRentService.Infrastructure.Repositories
{
    public class FineRepository : IFineRepository
    {
        private readonly BookRentDbContext _context;

        public FineRepository(BookRentDbContext context)
        {
            _context=context;
        }

        public async Task AddAsync(Fine fine)
        {
            try
            {
                _context.Fines.Add(fine);

                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.AddAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при добавлении штрафа!");
            }
        }

        public async Task DeleteAsync(int Id)
        {
            try
            {
                var fine = await _context.Fines.FindAsync(Id);

                _context.Remove(fine);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.DeleteAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при удалении штрафа!");
            }
        }

        public async Task<IEnumerable<Fine>> GetAllAsync()
        {
            try
            {
                return await _context.Fines.Include(x=>x.BookRent).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.GetAllAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка штрафов!");
            }
        }

        public async Task<IEnumerable<Fine>> GetByConditionAsync(Expression<Func<Fine, bool>> expression)
        {
             try
            {
                return await _context.Fines.Include(x=>x.BookRent).Where(expression).ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.GetByConditionAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка штрафов!");
            }
        }

        public async Task<Fine?> GetByIdAsync(int Id)
        {
            try
            {
                return await _context.Fines.Include(x=>x.BookRent).FirstOrDefaultAsync(x => x.Id == Id);
            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.GetByIdAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении штрафа!");
            }
        }

        public async Task<IEnumerable<Fine>> GetSortedAsync(Expression<Func<Fine, object>> sortExpression, bool descending = false)
        {
            try
            {
                var query = _context.Fines.Include(x=>x.BookRent).AsQueryable();
                if (descending)
                    query = query.OrderByDescending(sortExpression);
                else
                    query = query.OrderBy(sortExpression);
                return await query.ToListAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.GetSortedAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при получении списка штрафов!");
            }
        }

        public async Task UpdateAsync(Fine fine)
        {
           try
            {
                _context.Update(fine);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.UpdateAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при обновлении штрафа!");
            }
        }

        public async Task DeleteRangeAsync(int[] Ids)
        {
            try
            {
                var fines = await _context.Fines.Where(x=>Ids.Contains(x.Id)).ToListAsync();

                _context.RemoveRange(fines);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.DeleteRangeAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при удалении штрафов!");
            }
        }

        public async Task<bool> ExistsAsync(Expression<Func<Fine, bool>> expression)
        {
          try
            {
                return await _context.Fines.Include(x=>x.BookRent).AnyAsync(expression);

            }
            catch (Exception e)
            {
                Console.WriteLine("[FineRepository.ExistsAsync]: " + e.Message);
                throw new DataBaseException("Ошибка при проверке наличия штрафов!");
            }   
        }
    }
}