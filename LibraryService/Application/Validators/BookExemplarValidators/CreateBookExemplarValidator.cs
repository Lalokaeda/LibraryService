using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using LibraryService.Application.Commands;

namespace LibraryService.Application.Validators.BookExemplarValidators
{
    public class CreateBookExemplarValidator : AbstractValidator<CreateBookExemplarCommand>
    {
        public CreateBookExemplarValidator()
        {
            RuleFor(x=>x.ExemplarDto.BookId)
                .NotEqual(0).WithMessage("Id книги не может быть 0");

        }

        
    }
}