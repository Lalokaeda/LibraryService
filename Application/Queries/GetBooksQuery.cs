using LibraryService.Application.DTO.BooksDTO;
using MediatR;

namespace LibraryService.Application.Queries
{
    public class GetBooksQuery : IRequest<List<BookDto>>
    {
        
    }
}