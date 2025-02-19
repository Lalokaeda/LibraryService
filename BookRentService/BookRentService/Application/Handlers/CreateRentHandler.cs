using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.Commands;
using BookRentService.Application.Exceptions;
using BookRentService.Application.Interfaces;
using BookRentService.Domain.Entities;
using BookRentService.Domain.Interfaces;
using MediatR;

namespace BookRentService.Application.Handlers
{
    public class CreateRentHandler : IRequestHandler<CreateRentCommand, int>
    {
        private readonly IBaseRepository<BookRent> _bookRentRepository;
        private readonly IBaseRepository<BookExemplarRent> _bookExemplarRentRepository;
        private readonly IBookService _bookService;

        public CreateRentHandler(IBaseRepository<BookRent> bookRentRepository, IBaseRepository<BookExemplarRent> bookExemplarRentRepository, IBookService bookService)
        {
            _bookRentRepository = bookRentRepository;
            _bookExemplarRentRepository = bookExemplarRentRepository;
            _bookService = bookService;
        }

        public async Task<int> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var books = await _bookService.GetBooksByIdsAsync(request.RentDto.BookExemplarsId);
            
            if (books==null || books.Count==0)
            {
                throw new NotFoundException($"Книги {request.RentDto.BookExemplarsId.ToString} не найдены");
            }


            var rent = new BookRent{
                RenterId=request.RentDto.RenterId,
                StartDate=request.RentDto.StartDate,
                EndDate=request.RentDto.EndDate,
                RentStatusId=request.RentDto.RentStatusId
            };

            foreach (var bookExemplarId in request.RentDto.BookExemplarsId)
            {
                rent.BookExemplarRents.Add(new BookExemplarRent{
                    BookRentId=rent.Id,
                    BookExemplarId=bookExemplarId
                });
            }
            await _bookRentRepository.AddAsync(rent);
            return rent.Id;
        }
    }
}