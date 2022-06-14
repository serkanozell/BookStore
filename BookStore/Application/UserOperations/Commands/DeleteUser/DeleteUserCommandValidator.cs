using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
    {
        public DeleteUserCommandValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0).WithMessage("Please enter a bigger than 0");
        }
    }
}
