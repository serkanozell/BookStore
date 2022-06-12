using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidator : AbstractValidator<UpdateGenreCommand>
    {
        public UpdateGenreCommandValidator()
        {
            RuleFor(command => command.GenreId).GreaterThan(0).WithMessage("Genre Id Must be greater than 0");
            RuleFor(command => command.Model.GenreName).MinimumLength(4).When(command => command.Model.GenreName != string.Empty);
            RuleFor(command => command.Model.GenreName).NotEmpty().NotNull();
        }
    }
}
