using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidator : AbstractValidator<DeleteAuthorCommand>
    {
        public DeleteAuthorCommandValidator()
        {
            RuleFor(command => command.AuthorId).NotEmpty()
                                                .GreaterThanOrEqualTo(1)
                                                .WithMessage("Silmeye çalıştığınız yazarın id si boş olamaz, 0dan küçük olamaz. En az 1 yada büyük bir sayı giriniz");
        }
    }
}
