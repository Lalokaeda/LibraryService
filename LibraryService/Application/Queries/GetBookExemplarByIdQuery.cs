using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using MediatR;

namespace LibraryService.Application.Queries
{
    public class GetBookExemplarByIdQuery : IRequest<BookExemplarDto>
    {
        public int Id {get;}
        public GetBookExemplarByIdQuery(int id)
        {
            Id=id;
        }
    }
}