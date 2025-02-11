using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryService.Domain
{
    public class BookExemplar
    {
        [Required(ErrorMessage = "Id не заполнено!")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Книга не заполнена")]
        [ForeignKey("BookId")]
        public int BookId;

        [Range(1, 100, ErrorMessage ="Номер полки должен быть в диапазоне от 1 до 100!")]
        public int Shelf { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateAdded { get; set; }


        public Book Book {get; set;} = null!;
    }
}