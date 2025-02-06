using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.Application.Commands;
using LibraryService.Application.DTO.BooksDTO;

namespace LibraryService.Application.Validators.BookValidators
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookValidator()
        {

            When(x => x.BookDto.PublishingYear.HasValue, () =>
            {
                RuleFor(x => x.BookDto.PublishingYear)
                    .Must(y => y > 1400).WithMessage("Год издания должен быть больше 1400")
                    .GreaterThan(1400).WithMessage("Год издания должен быть больше 1400")
                    .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage($"Год издания не может быть больше {DateTime.UtcNow.Year}");

            });
            When(x => x.BookDto.AuthorsId !=null && x.BookDto.AuthorsId.Count>0, () =>
            {
                RuleForEach(x => x.BookDto.AuthorsId)
                    .NotEqual(0).WithMessage("Id автора не может быть 0");
        });
        }
    }
}
