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
    public class DeleteBookExemplarHandler : IRequestHandler<DeleteBookExemplarCommand, bool>
    {
        private IBaseRepository<BookExemplar> _bookExemplarRepository;

        public DeleteBookExemplarHandler(IBaseRepository<BookExemplar> bookExemplarRepository)
        {
            _bookExemplarRepository=bookExemplarRepository;
        }

        public async Task<bool> Handle(DeleteBookExemplarCommand request, CancellationToken cancellationToken)
        {
           var exemplar = await _bookExemplarRepository.GetByIdAsync(request.Id);

           if (exemplar==null)
           {
                throw new NotFoundException($"Экземпляр {request.Id} книги не найден");
           }

           await _bookExemplarRepository.DeleteAsync(exemplar.Id);
           return true;
        }
    }
}