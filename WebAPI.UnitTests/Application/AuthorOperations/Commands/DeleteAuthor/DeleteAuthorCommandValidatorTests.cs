using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //araange
            DeleteAuthorCommand deleteAuthorCommand = new DeleteAuthorCommand(null);
            deleteAuthorCommand.AuthorId = 1;

            //act
            DeleteAuthorCommandValidator validation = new DeleteAuthorCommandValidator();
            var result = validation.Validate(deleteAuthorCommand);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
