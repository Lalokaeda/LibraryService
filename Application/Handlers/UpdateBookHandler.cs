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
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IBaseRepository<Book> _bookRepository;
        public UpdateBookHandler(IBaseRepository<Book> bookRepository)
        {
            _bookRepository=bookRepository;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book==null)
                {
                    throw new NotFoundException($"Книга {request.Id} не найдена");
                }

            if (!string.IsNullOrEmpty(request.BookDto.Title))
                book.Title=request.BookDto.Title.Trim();

            if(request.BookDto.AuthorsId != null && request.BookDto.AuthorsId.Count != 0)
                book.Authors=request.BookDto.AuthorsId
                                    .Select(id=> new Author{Id=id})
                                    .ToList();
            if(request.BookDto.PublishingYear.HasValue)
                book.PublishingYear= (int)request.BookDto.PublishingYear;

            return true;
        }
    }
}