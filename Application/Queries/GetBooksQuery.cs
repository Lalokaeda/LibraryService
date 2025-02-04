using LibraryService.Application.DTO.BooksDTO;
using MediatR;

namespace LibraryService.Application.Queries
{
    public class GetBooksQuery : IRequest<List<BookDto>>
    {
        public string? SearchQuery {get; set;}
        public GetBooksQuery(string? searchQuery=null)
        {
            SearchQuery=searchQuery;
        }
    }
}