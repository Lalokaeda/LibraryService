using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BooksDTO;
using MediatR;

namespace LibraryService.Application.Queries
{
    public class GetBooksQuery : IRequest<List<BookDto>>
    {
        
    }
}