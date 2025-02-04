using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.Application.DTO.BooksDTO;

namespace LibraryService.Application.Validators.BookValidators
{
    public class UpdateBookValidator : AbstractValidator<UpdateBookDto>
    {
        public UpdateBookValidator()
        {

            When(x => x.PublishingYear.HasValue, () =>
            {
                RuleFor(x => x.PublishingYear)
                    .Must(y => y > 1400).WithMessage("Год издания должен быть больше 1400")
                    .GreaterThan(1400).WithMessage("Год издания должен быть больше 1400")
                    .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage($"Год издания не может быть больше {DateTime.UtcNow.Year}");
            });
        }
    }
}
