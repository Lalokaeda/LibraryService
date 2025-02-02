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
            When(x => x.Title is not null, () =>
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название книги обязательно")
                .MaximumLength(200).WithMessage("Название не может быть длиннее 200 символов")
                .MinimumLength(2).WithMessage("Название не может быть короче 2-х символов");
        });

        When(x => x.PublishingYear.HasValue, () =>
        {
            RuleFor(x => x.PublishingYear)
                .GreaterThan(0).WithMessage("Год издания должен быть больше нуля")
                .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage($"Год издания не может быть больше {DateTime.UtcNow.Year}");
        });

        When(x => x.AuthorsId is not null, () =>
        {
            RuleFor(x => x.AuthorsId)
                .NotEmpty().WithMessage("Книга должна содержать хотя бы одного автора");
        });
        }
    }
}