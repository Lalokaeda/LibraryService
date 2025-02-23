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

        public DeleteRentHandler(IBaseRepository<BookRent> bookRentRepository)
        {
            _bookRentRepository=bookRentRepository;
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