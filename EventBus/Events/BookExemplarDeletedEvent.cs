using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus.Abstractions;

namespace EventBus.Events
{
    public class BookExemplarDeletedEvent : IntegrationEvent
    {
        public int BookId { get; }

        public BookExemplarDeletedEvent(int bookId)
        {
            BookId = bookId;
        }
    }
}