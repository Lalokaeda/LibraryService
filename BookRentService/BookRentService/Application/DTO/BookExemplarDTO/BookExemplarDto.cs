using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Application.DTO.BookExemplarDTO
{
    public class BookExemplarDto
    {
        public int Id;
        public required string Title {get; set;}
        public required List<string> Authors {get; set;}
        public int PublishingYear {get; set;}
        public int Shelf { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }
    }
}