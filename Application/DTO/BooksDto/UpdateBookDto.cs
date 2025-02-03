using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryService.Application.DTO.BooksDTO
{
    public class UpdateBookDto
    {
        [StringLength (200, MinimumLength =2, ErrorMessage="минимальная длина 2 символа, максимальная - 200!")]
        public string? Title {get; set;}

        [MinLength(1, ErrorMessage ="Автор не выбран!")]
        public List<int>? AuthorsId {get; set;}

        public int? PublishingYear {get; set;}

    }
}