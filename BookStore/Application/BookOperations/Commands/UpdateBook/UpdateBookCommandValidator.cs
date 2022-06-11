using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.BookOperations.UpdateBook
{
    public class UpdateBookCommandValidator : AbstractValidator<UpdateBookCommand>
    {
        public UpdateBookCommandValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0).WithMessage("Book id must be greater than 0");
            RuleFor(command => command.BookId).LessThan(5).WithMessage("Book id must be less than 4");
            RuleFor(command => command.Model.GenreId).ExclusiveBetween(1, 5).WithMessage("GenreId must be between 1 and 5");
            //RuleFor(command => command.Model.GenreId).InclusiveBetween(1, 6).WithMessage("GenreId must be between 1 and 6");
            //RuleFor(command => command.Model.GenreId).IsInEnum().WithMessage("Id is not in an enum list!!!");
            RuleFor(command => command.Model.Title).NotEmpty().MinimumLength(4).WithMessage("Title can't be empty and need to has more than 4 character");
            RuleFor(command => command.Model.Title).NotNull().WithMessage("Title can't be null");

        }
    }
}
