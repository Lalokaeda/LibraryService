using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using LibraryService.Application.DTO.BooksDTO;
using LibraryService.Application.Exceptions;
using LibraryService.Application.Queries;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public GetBookByIdHandler(IBaseRepository<Book> bookRepository)
        {
            _bookRepository=bookRepository;
        }
        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);
            if (book==null)
            {
                throw new NotFoundException($"Книга {request.Id} не найдена");
            }

            return new BookDto{
                Title=book.Title,
                Authors=book.Authors.Select(author => author.GetFullName()).ToList(),
                BooksCount = book.GetBooksCount(),
                bookExemplars=book.BookExemplars.Select(exemplar=> new BookExemplarForBookDto{
                    Id = exemplar.Id,
                    Shelf = exemplar.Shelf,
                    DateAdded = exemplar.DateAdded
                    }).ToList(),
                PublishingYear=book.PublishingYear
            };
        }
    }
}