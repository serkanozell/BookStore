using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(command => command.Model.FirstName).NotEmpty().MinimumLength(2).WithMessage("User First Name can not be empty and needs to be longer than 2 character");
            RuleFor(command => command.Model.FirstName).NotNull().WithMessage("First name can not be null");
            RuleFor(command => command.Model.LastName).NotEmpty().MinimumLength(2).WithMessage("User last Name can not be empty and needs to be longer than 2 character");
            RuleFor(command => command.Model.LastName).NotNull().WithMessage("Last name can not be null");
            RuleFor(command => command.Model.Email).NotEmpty().MinimumLength(6).WithMessage("User e-mail Name can not be empty and needs to be longer than 6 character");
            RuleFor(command => command.Model.Email).NotNull().WithMessage("E-mail name can not be null");
            RuleFor(command => command.Model.Password).NotEmpty().WithMessage("Your password cannot be empty")
                    .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                    .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                    .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                    .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                    .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
                    .Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");

        }
    }
}
