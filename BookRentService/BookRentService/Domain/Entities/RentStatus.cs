using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Domain.Entities
{
    public class RentStatus
    {
        [Required(ErrorMessage = "Id не заполнено!")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required(ErrorMessage ="Наименование не заполнено!")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "минимальная длина 2 символа, максимальная - 200!")]
        public string Name {get; set;} = null!;
    }
}