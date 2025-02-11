using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Domain.Entities
{
    public class BookRent
    {
        [Required(ErrorMessage = "Id не заполнено!")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {get; set;}

        [Required(ErrorMessage = "Арендатор не заполнен!")]
        public int RenterId {get; set;}

        [Required(ErrorMessage = "Дата начала аренды не заполнена!")]
        public DateTime StartDate {get; set;}

        [Required(ErrorMessage = "Дата окончания аренды не заполнена!")]
        public DateTime EndDate{get; set;}

        [Required(ErrorMessage = "Статус не заполнен!")]
        public int RentStatusId {get; set;}
        public DateTime? ReturnDate {get; set;}

        public Renter Renter {get; set;} = null!;
        public RentStatus RentStatus {get; set;} = null!;
        public ICollection<BookExemplarRent> BookExemplarRents {get; set;} = null!;
    }
}