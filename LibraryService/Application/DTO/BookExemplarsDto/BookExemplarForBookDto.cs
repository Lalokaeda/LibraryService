using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using LibraryService.Application.DTO.BooksDTO;

namespace LibraryService.Application.DTO.BookExemplarsDto
{
    public class BookExemplarForBookDto
    {
        public int Id;
        
        public int Shelf { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}