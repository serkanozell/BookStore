using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).GreaterThan(0).WithMessage("Author id si 0dan büyük olmalıdır");
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(3).WithMessage("Yazar ismi 3 karakterden fazla olmalıdır");
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2).WithMessage("Yazarın soy ismi 2 karakterden fazla olmak zorundadır");
        }
    }
}
