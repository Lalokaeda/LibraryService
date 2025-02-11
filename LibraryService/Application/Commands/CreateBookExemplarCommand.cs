using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using LibraryService.Application.DTO.BooksDTO;
using MediatR;

namespace LibraryService.Application.Commands
{
    public class CreateBookExemplarCommand : IRequest<int>
    {
        public CreateBookExemplarDto ExemplarDto {get;}
        public CreateBookExemplarCommand(CreateBookExemplarDto exemplarDto)
        {
            ExemplarDto=exemplarDto;
        }
    }
}