using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using MediatR;

namespace LibraryService.Application.Commands
{
    public class UpdateBookExemplarCommand : IRequest<bool>
    {
        public int Id {get;}
        public UpdateBookExemplarDto ExemplarDto {get;}

        public UpdateBookExemplarCommand(int id, UpdateBookExemplarDto exemplarDto)
        {
            Id = id;
            ExemplarDto=exemplarDto;
        }
        
    }
}