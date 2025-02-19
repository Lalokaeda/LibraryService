using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using MediatR;

namespace LibraryService.Application.Queries
{
    public class GetBookExemplarsByIdQuery : IRequest<List<BookExemplarDto>>
    {
        public ICollection<int> Ids {get; set;}

        public GetBookExemplarsByIdQuery( ICollection<int> ids)
        {
            Ids=ids;
        }
    }
}