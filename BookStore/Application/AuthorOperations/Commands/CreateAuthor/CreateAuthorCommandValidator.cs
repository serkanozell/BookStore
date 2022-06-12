using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(3).WithMessage("Yazar ismi kesinlikle boş geçilemez ve 3 karakterden kısa olamaz!!");
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2).WithMessage("Yazar soy ismi kesinlikle boş geçilemez ve 2 karakterden kısa olamaz!!");
            RuleFor(command => command.Model.BirthDate.Date).LessThan(DateTime.Now.Date).WithMessage("Yazar doğum günü bugün ile aynı olamaz");
        }
    }
}
