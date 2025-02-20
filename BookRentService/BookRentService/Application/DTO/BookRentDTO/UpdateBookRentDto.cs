using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Application.DTO.BookRentDTO
{
    public class UpdateBookRentDto
    {
        public int? RenterId {get; set;}

        public DateTime? StartDate {get; set;}

        public DateTime? EndDate{get; set;}

        public int? RentStatusId {get; set;}

        public DateTime? ReturnDate {get; set;}

        public required List<int>? BookExemplarsId {get; set;}
    }
}