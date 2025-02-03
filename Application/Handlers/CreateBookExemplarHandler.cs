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
    public class CreateBookExemplarHandler : IRequestHandler<CreateBookExemplarCommand, int>
    {
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IBaseRepository<BookExemplar> _bookExemplarRepository;

        public CreateBookExemplarHandler(IBaseRepository<Book> bookRepository, IBaseRepository<BookExemplar> bookExemplarRepository)
        {
            _bookExemplarRepository = bookExemplarRepository;
            _bookRepository = bookRepository;
        }

        public async Task<int> Handle(CreateBookExemplarCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.ExemplarDto.BookId);
            if (book==null)
            {
                throw new NotFoundException($"Книга {request.ExemplarDto.BookId} не найдена");
            }

            var bookExemplar = new BookExemplar{
                Book = book,
                Shelf = request.ExemplarDto.Shelf,
                DateAdded = DateTime.Now.Date
            };

            await _bookExemplarRepository.AddAsync(bookExemplar);
            return bookExemplar.Id;
        }
    }
}