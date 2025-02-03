using LibraryService.Application.DTO.BooksDTO;
using MediatR;

namespace LibraryService.Application.Commands
{
    public class CreateBookCommand : IRequest<int>
    {
        public CreateBookDto BookDto { get; }

        public CreateBookCommand(CreateBookDto bookDto)
        {
            BookDto = bookDto;
        }
    }
}