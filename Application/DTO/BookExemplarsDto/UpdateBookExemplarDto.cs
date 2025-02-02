using System.ComponentModel.DataAnnotations;


namespace LibraryService.Application.DTO.BookExemplarsDto
{
    public class UpdateBookExemplarDto
    {
        [MinLength(1, ErrorMessage ="Книга не выбрана!")]
        public int? BookId;

        [Range(1, 100, ErrorMessage ="Номер полки должен быть в диапазоне от 1 до 100!")]
        public int? Shelf;
    }
}