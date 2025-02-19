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
        private readonly IMediator _mediator;

        public BookDeletedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Handle(BookExemplarDeletedEvent @event)
        {
            await _mediator.Send(new DeleteRentByBookIdCommand(@event.BookId));
        }
    }
}