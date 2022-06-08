using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookDetailQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0).WithMessage("Book id has to bigger than 0!!");
            RuleFor(command => command.BookId).LessThan(4).WithMessage("Book id has to less than 4!!");
        }
    }
}
