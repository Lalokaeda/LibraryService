using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryService.Application.DTO.AuthorsDto
{
    public class AuthorDto
    {
        public int Id {get; set;}

        public required string FullName {get; set;}
    }
}