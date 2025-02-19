using EventBus.Abstractions;
using EventBus.Events;
using LibraryService.Application.Commands;
using LibraryService.Application.Exceptions;
using LibraryService.Domain;
using LibraryService.Domain.Interfaces;
using MediatR;

namespace LibraryService.Application.Handlers
{
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBaseRepository<Book> _bookRepository;
        private readonly IEventBus _eventBus;

        public DeleteBookHandler(IBaseRepository<Book> bookRepository, IEventBus eventBus)
        {
            _bookRepository=bookRepository;
            _eventBus=eventBus;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepository.GetByIdAsync(request.Id);

            if (book==null)
            {
                throw new NotFoundException($"Книга {request.Id} не найдена");
            }

            await _bookRepository.DeleteAsync(book.Id);
            var integrationEvent = new BookExemplarDeletedEvent(request.Id);
            await _eventBus.PublishAsync(integrationEvent);
            return true;
        }
    }
}