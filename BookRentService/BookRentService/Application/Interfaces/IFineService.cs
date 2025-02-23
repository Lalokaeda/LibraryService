using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Domain.Entities;

namespace BookRentService.Application.Interfaces
{
    public interface IFineService
    {
        public Task CheckAndApplyFinesAsync();
        public decimal CalculateFine(BookRent rent);
    }
}