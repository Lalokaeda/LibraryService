using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace BookRentService.Application.Commands
{
    public class DeleteRentCommand : IRequest<bool>
    {
        public int Id {get;} 
        public DeleteRentCommand(int id)
        {
            Id=id;
        }
    }
}