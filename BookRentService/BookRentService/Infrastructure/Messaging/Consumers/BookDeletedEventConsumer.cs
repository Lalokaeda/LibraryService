using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.Commands;
using BookRentService.Application.Handlers;
using EventBus.Abstractions;
using EventBus.Events;
using MediatR;

namespace BookRentService.Infrastructure.Messaging.Consumers
{
    public class BookDeletedEventConsumer
    {
        // private readonly IMediator _mediator;
        private readonly IServiceProvider _serviceProvider;

        public BookDeletedEventConsumer(IServiceProvider serviceProvider)
        {
            // _mediator = mediator;
            _serviceProvider=serviceProvider;
        }

        public async Task Handle(BookExemplarDeletedEvent @event)
        {
            //await _mediator.Send(new DeleteRentByBookIdCommand(@event.BookId));
            using (var scope = _serviceProvider.CreateScope())
        {
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            await mediator.Send(new DeleteRentByBookIdCommand(@event.BookId));
        }
        }

    }
}