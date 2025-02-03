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
        private readonly IBaseRepository<Author> _authorRepository;

        public CreateBookHandler(IBaseRepository<Book> bookRepository, IBaseRepository<Author> authorRepository)
        {
            _bookRepository=bookRepository;
            _authorRepository=authorRepository;
        }

        public async Task<int> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var authors = await _authorRepository.GetByConditionAsync(x=> request.BookDto.AuthorsId.Contains(x.Id));
            var book = new Book{
                Title=request.BookDto.Title.Trim(),
                Authors = authors.ToList(),
                PublishingYear = request.BookDto.PublishingYear
            };
            await _bookRepository.AddAsync(book);
            return book.Id;
        }
    }
}