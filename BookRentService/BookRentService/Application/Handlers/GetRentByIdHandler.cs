using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.DTO.BookRentDTO;
using BookRentService.Application.Exceptions;
using BookRentService.Application.Interfaces;
using BookRentService.Application.Queries;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;
using MediatR;

namespace BookRentService.Application.Handlers
{
    public class GetRentByIdHandler : IRequestHandler<GetRentByIdQuery, BookRentDto>
    {
        private readonly IBaseRepository<BookRent> _bookRentRepository;
        private readonly IBookService _bookService;

        public GetRentByIdHandler(IBaseRepository<BookRent> bookRentRepository, IBookService bookService)
        {
            _bookRentRepository = bookRentRepository;
            _bookService = bookService;
        }

        public async Task<BookRentDto> Handle(GetRentByIdQuery request, CancellationToken cancellationToken)
        {
            var rent = await _bookRentRepository.GetByIdAsync(request.Id);
            if (rent == null)
            {
                throw new NotFoundException($"Аренда {request.Id} не найдена");
            }
           var exemplars = await _bookService.GetBooksByIdsAsync(rent.BookExemplarRents.Select(x=>x.BookExemplarId).ToList());
           return new BookRentDto{
                Id = rent.Id,
                Renter = rent.Renter.GetFullName(),
                StartDate = rent.StartDate,
                EndDate = rent.EndDate,
                RentStatus = rent.RentStatus.Name,
                ReturnDate = rent.ReturnDate,
                BooksCount = rent.GetBooksCount(),
                BookExemplars = exemplars
            };
        }
    }
}