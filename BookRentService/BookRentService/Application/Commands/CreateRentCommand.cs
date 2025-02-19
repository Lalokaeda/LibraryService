using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.DTO.BookRentDTO;
using MediatR;

namespace BookRentService.Application.Commands
{
    public class CreateRentCommand : IRequest<int>
    {
        public CreateBookRentDto RentDto{get;}
        
        public CreateRentCommand(CreateBookRentDto rentDto)
        {
            RentDto=rentDto;
        }
    }
}