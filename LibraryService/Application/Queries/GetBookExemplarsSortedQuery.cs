using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BookExemplarsDto;
using MediatR;

namespace LibraryService.Application.Queries
{
    public class GetBookExemplarsSortedQuery : IRequest<List<BookExemplarDto>>
    {
        public string SortBy { get; }
        public bool Descending { get; }
        public GetBookExemplarsSortedQuery(string sortBy = "Shelf", bool descendig = false)
        {
            SortBy=sortBy;
            Descending=descendig;
        }

        
        
    }
}