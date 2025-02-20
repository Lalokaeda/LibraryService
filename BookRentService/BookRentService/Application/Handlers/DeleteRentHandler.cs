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
    public class DeleteRentHandler : IRequestHandler<DeleteRentCommand, bool>
    {
        private readonly IBaseRepository<BookRent> _bookRentRepository;
        private readonly IBaseRepository<BookExemplarRent> _bookExemplarRentRepository;
        private readonly IBookService _bookService;

        public DeleteRentHandler(IBaseRepository<BookRent> bookRentRepository, IBaseRepository<BookExemplarRent> bookExemplarRentRepository, IBookService bookService)
        {
            _bookRentRepository=bookRentRepository;
            _bookExemplarRentRepository=bookExemplarRentRepository;
            _bookService=bookService;
        }

        public async Task<bool> Handle(DeleteRentCommand request, CancellationToken cancellationToken)
        {
           var rent = await _bookRentRepository.GetByIdAsync(request.Id);

            if (rent == null)
            {
                throw new NotFoundException($"Аренда {request.Id} не найдена");
            }
            await _bookRentRepository.DeleteAsync(rent.Id);
            return true;
        }
    }
}