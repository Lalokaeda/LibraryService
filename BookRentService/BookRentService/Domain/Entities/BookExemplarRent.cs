using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Domain.Entities
{
    public class BookExemplarRent
    {
        [Required(ErrorMessage = "Id не заполнено!")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required(ErrorMessage = "Аренда не заполнена!")]
        public int BookRentId {get; set;}

        [Required(ErrorMessage = "Книга не заполнена!")]
        public int BookExemplarId {get; set;}

        public BookRent BookRent {get; set;} = null!;
    }
}