using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;
using EventBus.Events;
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
        private readonly IEventBus _eventBus;

        public DeleteBookExemplarHandler(IBaseRepository<BookExemplar> bookExemplarRepository, IEventBus eventBus)
        {
            _bookExemplarRepository=bookExemplarRepository;
            _eventBus = eventBus;
        }

        public async Task<bool> Handle(DeleteBookExemplarCommand request, CancellationToken cancellationToken)
        {
           var exemplar = await _bookExemplarRepository.GetByIdAsync(request.Id);

           if (exemplar==null)
           {
                throw new NotFoundException($"Экземпляр {request.Id} книги не найден");
           }

           await _bookExemplarRepository.DeleteAsync(exemplar.Id);
            var integrationEvent = new BookExemplarDeletedEvent(request.Id);
            await _eventBus.PublishAsync(integrationEvent);
           return true;
        }
    }
}