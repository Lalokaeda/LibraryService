using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.Interfaces;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;
using BookRentService.Infrastructure.Repositories;

namespace BookRentService.Infrastructure.Service
{
    public class FineService : IFineService
    {
        private readonly IFineRepository _fineRepository;
        private readonly IBaseRepository<BookRent> _bookRentRepository;

        public FineService(IFineRepository fineRepository, IBaseRepository<BookRent> bookRentRepository)
        {
            _fineRepository = fineRepository;
            _bookRentRepository = bookRentRepository;
        }

        public decimal CalculateFine(BookRent rent)
        {
            int overdueDays = (DateTime.UtcNow - rent.EndDate).Days;
            return overdueDays*100;
        }

        public async Task CheckAndApplyFinesAsync()
        {
            var overdueRents = await _bookRentRepository.GetByConditionAsync(x=>x.EndDate<DateTime.UtcNow && x.ReturnDate==null);

            foreach (var rent in overdueRents)
            {
                bool fineExists = await _fineRepository.ExistsAsync(x=>x.BookRentId == rent.Id 
                                                                    && x.BookRent.ReturnDate==null);
                if (!fineExists)
                {
                    var fine = new Fine{
                        BookRentId=rent.Id,
                        Amount=CalculateFine(rent)
                    };
                    await _fineRepository.AddAsync(fine);
                }
                else
                {
                    var fine = (await _fineRepository.GetByConditionAsync(x=>x.BookRentId==rent.Id)).FirstOrDefault();

                    fine.Amount=CalculateFine(rent);

                    await _fineRepository.UpdateAsync(fine);
                }

                rent.RentStatusId=4;
                await _bookRentRepository.UpdateAsync(rent);

            }


        }
    }
}