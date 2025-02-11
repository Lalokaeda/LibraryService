using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace LibraryService.Application.Commands
{
    public class DeleteBookCommand : IRequest<bool>
    {
        public int Id {get;} 
        public DeleteBookCommand(int id)
        {
            Id=id;
        }  
    }
}