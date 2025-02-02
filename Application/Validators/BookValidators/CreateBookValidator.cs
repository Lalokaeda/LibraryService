using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.Application.DTO.BooksDTO;

namespace LibraryService.Application.Validators.BookValidators
{
    public class CreateBookValidator : AbstractValidator<CreateBookDto>
    {
        public CreateBookValidator()
        {
            RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Название книги обязательно")
            .MaximumLength(200).WithMessage("Название не может быть длиннее 200 символов")
            .MinimumLength(2).WithMessage("Название не может быть короче 2-х символов");

        RuleFor(x => x.PublishingYear)
            .GreaterThan(1400).WithMessage("Год издания должен быть больше 1400")
            .LessThanOrEqualTo(DateTime.UtcNow.Year).WithMessage($"Год издания не может быть больше {DateTime.UtcNow.Year}");

        RuleFor(x => x.AuthorsId)
            .NotEmpty().WithMessage("Книга должна содержать хотя бы одного автора");
        }
    }
}