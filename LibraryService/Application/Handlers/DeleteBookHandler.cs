using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.Commands;
using LibraryService.Application.Exceptions;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public DeleteBookHandler(IBaseRepository<Book> bookRepository)
        {
            _bookRepository=bookRepository;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book==null)
            {
                throw new NotFoundException($"Книга {request.Id} не найдена");
            }

            await _bookRepository.DeleteAsync(book.Id);
            return true;
        }
    }
}