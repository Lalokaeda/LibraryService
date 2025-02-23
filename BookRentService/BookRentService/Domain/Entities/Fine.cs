using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookRentService.Domain.Entities
{
    public class Fine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int BookRentId { get; set; }
        public decimal Amount { get; set; } 
        public bool IsPaid { get; set; } = false;
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;

        public BookRent BookRent { get; set; } = null!;
    }
}