using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Domain.Entities
{
    public class Renter
    {
        [Required(ErrorMessage = "Id не заполнено!")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [StringLength(200, MinimumLength = 2, ErrorMessage = "минимальная длина 2 символа, максимальная - 200!")]
        [Required(ErrorMessage = "Имя не заполнено!")]
        public string Name {get; set;} = null!;

        [StringLength(200, MinimumLength = 2, ErrorMessage = "минимальная длина 2 символа, максимальная - 200!")]
        [Required(ErrorMessage = "Фамилия не заполнена!")]
        public string LastName {get; set;} = null!;
        public string? MiddleName {get; set;}
        public string Phone {get; set;} = null!;

        public virtual ICollection<BookRent> BookRents {get; set;} = new List<BookRent>();

        public string GetFullName()
        {
            return MiddleName == null? $"{Name} {LastName}" : $"{Name} {LastName} {MiddleName}";
        }

        public string GetShortName()
        {
            return MiddleName == null? $"{Name} {LastName.Substring(0, 1)}." : $"{Name} {LastName.Substring(0, 1)}. {MiddleName.Substring(0, 1)}";
        }
    }
}