using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidator : AbstractValidator<GetUserDetailQuery>
    {
        public GetUserDetailQueryValidator()
        {
            RuleFor(command => command.UserId).GreaterThan(0).WithMessage("User id must be greater than 0");
        }
    }
}
