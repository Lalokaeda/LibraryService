using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using LibraryService.Application.Queries;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class GetBookExemplarsSortedHandler : IRequestHandler<GetBookExemplarsSortedQuery, List<BookExemplarDto>>
    {
        private readonly IBaseRepository<BookExemplar> _bookExemplarRepository;
        private readonly IBaseRepository<Book> _bookRepository;

        public GetBookExemplarsSortedHandler(IBaseRepository<BookExemplar> bookExemplarRepository, IBaseRepository<Book> bookRepository)
        {
            _bookExemplarRepository=bookExemplarRepository;
            _bookRepository = bookRepository;
        }

        async Task<List<BookExemplarDto>> IRequestHandler<GetBookExemplarsSortedQuery, List<BookExemplarDto>>.Handle(GetBookExemplarsSortedQuery request, CancellationToken cancellationToken)
        {
           Expression<Func<BookExemplar, object>> expression = request.SortBy switch 
           {
                "Shelf" => x=>x.Shelf,
                "DateAdded" => x=>x.DateAdded,
                _ => x=>x.Shelf
           };

            var exemplars = await _bookExemplarRepository.GetSortedAsync(expression, request.Descending);
            return exemplars.Select(exemplar=> new BookExemplarDto{
                    Title = exemplar.Book.Title,
                    Authors = exemplar.Book.Authors.Select(x=> x.GetFullName()).ToList(),
                    PublishingYear = exemplar.Book.PublishingYear,
                    Shelf = exemplar.Shelf,
                    DateAdded = exemplar.DateAdded
                }).ToList();
        }
    }
}