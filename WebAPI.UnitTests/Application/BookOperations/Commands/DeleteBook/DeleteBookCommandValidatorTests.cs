using BookStore.Application.BookOperations.Commands.DeleteBook;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange

            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(null);
            deleteBookCommand.BookId = 1;

            //act

            DeleteBookCommandValidator validation = new DeleteBookCommandValidator();
            var result = validation.Validate(deleteBookCommand);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
