using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.Commands;
using LibraryService.Application.Exceptions;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using LibraryService.Infrastructure.Repositories;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class UpdateBookExemplarHandler : IRequestHandler<UpdateBookExemplarCommand, bool>
    {
        private readonly IBaseRepository<BookExemplar> _bookExemplarRepository;
        private readonly IBaseRepository<Book> _bookRepository;

        public UpdateBookExemplarHandler(IBaseRepository<BookExemplar> bookExemplarRepository, IBaseRepository<Book> bookRepository)
        {
            _bookExemplarRepository = bookExemplarRepository;
            _bookRepository = bookRepository;
        }

        public async Task<bool> Handle(UpdateBookExemplarCommand request, CancellationToken cancellationToken)
        {
            var exemplar = await _bookExemplarRepository.GetByIdAsync(request.Id);
            
            if (exemplar==null)
            {
                throw new NotFoundException($"Экземпляр {request.Id} книги не найден");
            }

            if (request.ExemplarDto.BookId.HasValue)
            {
                var book = await _bookRepository.GetByIdAsync((int)request.ExemplarDto.BookId);
                if (book==null)
                {
                    throw new NotFoundException($"Книга {request.ExemplarDto.BookId} не найдена");
                }
                exemplar.Book=book;
            }

            if (request.ExemplarDto.Shelf.HasValue)
                exemplar.Shelf= (int)request.ExemplarDto.Shelf;

            return true;
        }
    }
}