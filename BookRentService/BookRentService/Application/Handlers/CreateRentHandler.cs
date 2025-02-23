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
        private readonly IBookService _bookService;

        public CreateRentHandler(IBaseRepository<BookRent> bookRentRepository, IBookService bookService)
        {
            _bookRentRepository = bookRentRepository;
            _bookService = bookService;
        }

        public async Task<int> Handle(CreateRentCommand request, CancellationToken cancellationToken)
        {
            var books = await _bookService.GetBooksByIdsAsync(request.RentDto.BookExemplarsId);

            if (books == null || books.Count == 0)
            {
                throw new NotFoundException($"Книги не найдены");
            }

            var overlappingRents = await _bookRentRepository.GetByConditionAsync(
                                                            x => x.BookExemplarRents.Any(be => request.RentDto.BookExemplarsId.Contains(be.BookExemplarId)) &&
                                                            x.StartDate < request.RentDto.EndDate &&
                                                            x.EndDate > request.RentDto.StartDate);

            if (overlappingRents!=null && overlappingRents.Count()!=0)
            {
                throw new ValidationException("Одна или несколько книг уже арендованы на выбранные даты.");
            }


            var rent = new BookRent
            {
                RenterId = request.RentDto.RenterId,
                StartDate = request.RentDto.StartDate,
                EndDate = request.RentDto.EndDate,
                RentStatusId = request.RentDto.RentStatusId,
                BookExemplarRents = new List<BookExemplarRent>()
            };

            await _bookRentRepository.AddAsync(rent);

            foreach (var bookExemplarId in request.RentDto.BookExemplarsId)
            {
                rent.BookExemplarRents.Add(new BookExemplarRent
                {
                    BookRentId = rent.Id,
                    BookExemplarId = bookExemplarId
                });
            }
            await _bookRentRepository.UpdateAsync(rent);
            return rent.Id;
        }
    }
}