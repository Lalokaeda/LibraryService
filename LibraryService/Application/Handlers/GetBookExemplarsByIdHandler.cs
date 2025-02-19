using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using LibraryService.Application.Queries;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class GetBookExemplarsByIdHandler : IRequestHandler<GetBookExemplarsByIdQuery, List<BookExemplarDto>>
    {
        private readonly IBaseRepository<BookExemplar> _bookExemplarRepository;

        public GetBookExemplarsByIdHandler(IBaseRepository<BookExemplar> bookExemplarRepository, IBaseRepository<Book> bookRepository)
        {
            _bookExemplarRepository = bookExemplarRepository;
        }

        public async Task<List<BookExemplarDto>> Handle(GetBookExemplarsByIdQuery request, CancellationToken cancellationToken)
        {
           var exemplars = await _bookExemplarRepository.GetByConditionAsync(x=> request.Ids.Contains(x.Id));
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