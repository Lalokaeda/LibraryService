using System.ComponentModel.DataAnnotations;


namespace LibraryService.Application.DTO.BookExemplarsDto
{
    public class UpdateBookExemplarDto
    {
        public int? BookId {get; set;}

        [Range(1, 100, ErrorMessage ="Номер полки должен быть в диапазоне от 1 до 100!")]
        public int? Shelf {get; set;}
    }
}