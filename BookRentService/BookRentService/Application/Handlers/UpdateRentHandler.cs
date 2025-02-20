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
    public class UpdateRentHandler : IRequestHandler<UpdateRentCommand, bool>
    {
        private readonly IBaseRepository<BookRent> _bookRentRepository;
        private readonly IBaseRepository<BookExemplarRent> _bookExemplarRentRepository;
        private readonly IBookService _bookService;
        public UpdateRentHandler(IBaseRepository<BookRent> bookRentRepository,
                                IBaseRepository<BookExemplarRent> bookExemplarRentRepository, IBookService bookService)
        {
            _bookRentRepository = bookRentRepository;
            _bookService = bookService;
            _bookExemplarRentRepository = bookExemplarRentRepository;
        }

        public async Task<bool> Handle(UpdateRentCommand request, CancellationToken cancellationToken)
        {
            var rent = await _bookRentRepository.GetByIdAsync(request.Id);

            if (rent == null)
            {
                throw new NotFoundException($"Аренда {request.Id} не найдена");
            }

            if (request.UpdateRentDto.StartDate != null)
            {
                rent.StartDate = (DateTime)request.UpdateRentDto.StartDate;
            }

            if (request.UpdateRentDto.EndDate != null)
            {
                rent.EndDate = (DateTime)request.UpdateRentDto.EndDate;
            }

            if (request.UpdateRentDto.ReturnDate != null)
            {
                rent.ReturnDate = request.UpdateRentDto.ReturnDate;
            }

            if (request.UpdateRentDto.RenterId != null)
            {
                rent.RenterId = (int)request.UpdateRentDto.RenterId;
            }

            if (request.UpdateRentDto.RentStatusId != null)
            {
                rent.RentStatusId = (int)request.UpdateRentDto.RentStatusId;
            }

            if (request.UpdateRentDto.BookExemplarsId != null && request.UpdateRentDto.BookExemplarsId.Count != 0)
            {
                rent.BookExemplarRents.Clear();
                foreach (var bookExemplarId in request.UpdateRentDto.BookExemplarsId)
                {
                    rent.BookExemplarRents.Add(new BookExemplarRent
                    {
                        BookRentId = rent.Id,
                        BookExemplarId = bookExemplarId
                    });
                }
            }

            await _bookRentRepository.UpdateAsync(rent);
            return true;
        }
    }
}