using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryService.Application.BooksDto
{
    public class CreateBookDto
    {
        [StringLength (200, MinimumLength =2, ErrorMessage="минимальная длина 2 символа, максимальная - 200!")]
        [Required(ErrorMessage = "Наименование не заполнено!!")]
        public required string Title;

        [Required(ErrorMessage ="Автор не выбран!")]
        [MinLength(1, ErrorMessage ="Автор не выбран!")]
        public required List<int> AuthorsId;

        public int PublishingYear;

        [Range(1, 100, ErrorMessage ="Номер полки должен быть в диапазоне от 1 до 100!")]
        public int Shelf;

    }
}