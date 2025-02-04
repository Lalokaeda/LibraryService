using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BooksDTO;
using MediatR;

namespace LibraryService.Application.Queries
{
    public class GetBooksBySearchQuery : IRequest<List<BookDto>>
    {
        public string SearchQuery {get; set;}
        public GetBooksBySearchQuery(string searchQuery)
        {
            SearchQuery=searchQuery;
        }
        
    }
}