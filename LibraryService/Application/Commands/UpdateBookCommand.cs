using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BooksDTO;
using MediatR;

namespace LibraryService.Application.Commands
{
    public class UpdateBookCommand : IRequest<bool>
    {
        public int Id {get;}
        public UpdateBookDto BookDto {get;}

        public UpdateBookCommand(int id, UpdateBookDto bookDto)
        {
            Id=id;
            BookDto=bookDto;
        }
    }
}