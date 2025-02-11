using LibraryService.Application.DTO.AuthorsDto;
using LibraryService.Application.DTO.BookExemplarsDto;

namespace LibraryService.Application.DTO.BooksDTO
{
    public class BookDto
    {
        public int Id {get; set;}
        public required string Title {get; set;}

        public required List<string> Authors {get; set;}

        public int PublishingYear {get; set;}

        public int BooksCount {get; set;}

        public List<BookExemplarForBookDto>? bookExemplars {get; set;}
    }
}