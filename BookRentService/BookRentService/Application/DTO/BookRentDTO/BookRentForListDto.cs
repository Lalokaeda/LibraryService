using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Application.DTO.BookRentDTO
{
    public class BookRentForListDto
    {
        public int Id {get; set;}
        public required string Renter {get; set;}

        public DateTime StartDate {get; set;}

        public DateTime EndDate{get; set;}

        public required string RentStatus {get; set;}

        public DateTime? ReturnDate {get; set;}

        public int BooksCount {get; set;}
    }
}