using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookRentService.Application.DTO.BookRentDTO;
using MediatR;

namespace BookRentService.Application.Commands
{
    public class UpdateRentCommand : IRequest<bool>
    {
        public int Id {get;}
        public UpdateBookRentDto UpdateRentDto {get;}
        public UpdateRentCommand(int id, UpdateBookRentDto updateRentDto)
        {
            Id=id;
            UpdateRentDto=updateRentDto;
        }
    }
}