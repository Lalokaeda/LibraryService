using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.Application.Commands;

namespace LibraryService.Application.Validators.BookExemplarValidators
{
    public class UpdateBookExemplarValidator : AbstractValidator<UpdateBookExemplarCommand>
    {
        public UpdateBookExemplarValidator()
        {
            When(x=> x.ExemplarDto.BookId.HasValue, () =>
            {
               RuleFor(x=>x.ExemplarDto.BookId)
                .NotEqual(0).WithMessage("Id книги не может быть 0");
            });
        }
    }
}