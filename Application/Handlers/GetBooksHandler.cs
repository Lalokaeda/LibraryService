using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.AuthorsDto;
using LibraryService.Application.DTO.BookExemplarsDto;
using LibraryService.Application.DTO.BooksDTO;
using LibraryService.Application.Queries;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class GetBooksHandler : IRequestHandler<GetBooksQuery, List<BookDto>>
    {
        private readonly IBaseRepository<Book> _bookRepository;

        public GetBooksHandler(IBaseRepository<Book> bookRepository)
        {
            _bookRepository=bookRepository;
        }

        public async Task<List<BookDto>> Handle(GetBooksQuery request, CancellationToken cancellationToken)
        {
            var books = await _bookRepository.GetAllAsync();

            return books.Select(book=> new BookDto{
                Id=book.Id,
                Title = book.Title,
                Authors = book.Authors.Select(author => author.GetFullName()).ToList(),
                BooksCount = book.GetBooksCount(),
                bookExemplars=book.BookExemplars.Select(exemplar=> new BookExemplarDto{
                    Id = exemplar.Id,
                    Shelf = exemplar.Shelf,
                    DateAdded = exemplar.DateAdded
                    }).ToList()
                }).ToList();
            }
        }
    }