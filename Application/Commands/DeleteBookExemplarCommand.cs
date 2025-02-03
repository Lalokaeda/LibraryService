using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace LibraryService.Application.Commands
{
    public class DeleteBookExemplarCommand : IRequest<bool>
    {
        public int Id {get;}
        public DeleteBookExemplarCommand(int id)
        {
            Id=id;
        }
    }
}