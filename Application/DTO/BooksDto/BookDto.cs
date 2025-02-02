using LibraryService.Application.DTO.AuthorsDto;
using LibraryService.Application.DTO.BookExemplarsDto;

namespace LibraryService.Application.DTO.BooksDTO
{
    public class BookDto
    {
        public int Id;
        public required string Title;

        public required List<AuthorDto> Authors;

        public int PublishingYear;

        public int BooksCount;

        public List<BookExemplarDto>? bookExemplars;
    }
}