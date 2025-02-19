using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BookRentService.Application.Commands
{
    public class DeleteRentByBookIdCommand : IRequest<bool>
    {
        public int BookId {get; set;}
        
        public DeleteRentByBookIdCommand(int bookId)
        {
            BookId=bookId;
        }
    }
}