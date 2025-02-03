using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryService.Application.DTO.BookExemplarsDto
{
    public class CreateBookExemplarDto
    {
        [Required(ErrorMessage ="Книга не выбрана!")]
        [MinLength(1, ErrorMessage ="Книга не выбрана!")]
        public required int BookId {get; set;}

        [Range(1, 100, ErrorMessage ="Номер полки должен быть в диапазоне от 1 до 100!")]
        public required int Shelf {get; set;}
    }
}