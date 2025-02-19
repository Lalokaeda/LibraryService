using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.DTO.BookRentDTO;
using BookRentService.Application.Queries;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;
using MediatR;

namespace BookRentService.Application.Handlers
{
    public class GetRentsHandler : IRequestHandler<GetRentsQuery, List<BookRentForListDto>>
    {
        private readonly IBaseRepository<BookRent> _bookRentRepository;

        public GetRentsHandler(IBaseRepository<BookRent> bookRentRepository)
        {
            _bookRentRepository = bookRentRepository;
        }

        public async Task<List<BookRentForListDto>> Handle(GetRentsQuery request, CancellationToken cancellationToken)
        {
            var rents = await _bookRentRepository.GetAllAsync();
            return rents.Select(rent=> new BookRentForListDto{
                Id = rent.Id,
                Renter = rent.Renter.GetFullName(),
                StartDate = rent.StartDate,
                EndDate = rent.EndDate,
                RentStatus = rent.RentStatus.Name,
                ReturnDate = rent.ReturnDate,
                BooksCount = rent.GetBooksCount()
            }).ToList();
        }
    }
}