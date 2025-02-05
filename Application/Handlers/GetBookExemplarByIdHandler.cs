using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using LibraryService.Application.Exceptions;
using LibraryService.Application.Queries;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class GetBookExemplarByIdHandler : IRequestHandler<GetBookExemplarByIdQuery, BookExemplarDto>
    {
        private readonly IBaseRepository<BookExemplar> _bookExemplarRepository;
        public GetBookExemplarByIdHandler(IBaseRepository<BookExemplar> bookExemplarRepository)
        {
            _bookExemplarRepository = bookExemplarRepository;
        }

        public async Task<BookExemplarDto> Handle(GetBookExemplarByIdQuery request, CancellationToken cancellationToken)
        {
            var exemplar = await _bookExemplarRepository.GetByIdAsync(request.Id);
            if (exemplar == null)
            {
                throw new NotFoundException($"Экземпляр {request.Id} не найден");
            }

            return new BookExemplarDto
            {
                Title = exemplar.Book.Title,
                Authors = exemplar.Book.Authors.Select(x => x.GetFullName()).ToList(),
                PublishingYear = exemplar.Book.PublishingYear,
                Shelf = exemplar.Shelf,
                DateAdded = exemplar.DateAdded
            };
        }
    }
}