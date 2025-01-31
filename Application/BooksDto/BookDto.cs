using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.AuthorsDto;

namespace LibraryService.Application
{
    public class BookDto
    {
        public int Id;
        public required string Title;

        public required List<AuthorDto> Authors;

        public int PublishingYear;

        public int Shelf;

        public DateOnly DateAdded;


    }
}