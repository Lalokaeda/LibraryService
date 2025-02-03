using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.Commands;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using LibraryService.Infrastructure.Repositories;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, int>
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public CreateBookHandler(IBaseRepository<Book> bookRepository)
        {
            _bookRepository=bookRepository;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var book = new Book{
                Title=request.BookDto.Title.Trim(),
                Authors = request.BookDto.AuthorsId.Select(id=> new Author{Id=id}).ToList(),
                PublishingYear = request.BookDto.PublishingYear
            };
            await _bookRepository.AddAsync(book);
            return book.Id;
        }
    }
}