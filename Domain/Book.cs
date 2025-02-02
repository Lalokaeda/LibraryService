using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryService.Domain
{
    public class Book
    {
        [Required(ErrorMessage = "Id не заполнено!")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength (200, MinimumLength =2, ErrorMessage="минимальная длина 2 символа, максимальная - 200!")]
        [Required(ErrorMessage = "Наименование не заполнено!!")]
        public string Title { get; set; } = null!;

        public int PublishingYear { get; set; }

        public virtual ICollection<Author> Authors { get;} = new List<Author>();

        public virtual ICollection<BookExemplar> BookExemplars { get;} = new List<BookExemplar>();

        public int GetBooksCount(){
            return BookExemplars.Count();
        }
    }
}
