using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0, "", "")]
        [InlineData(0, " ", " ")]
        [InlineData(0, "a", "a")]
        [InlineData(0, "a", "aa")]
        [InlineData(0, "a", "aaa")]
        [InlineData(0, "aa", "a")]
        [InlineData(0, "aaa", "a")]
        [InlineData(0, "aaa", "aa")]
        [InlineData(1, "", "")]
        [InlineData(1, " ", " ")]
        [InlineData(1, "a", "a")]
        [InlineData(1, "a", "aa")]
        [InlineData(1, "a", "aaa")]
        [InlineData(1, "aa", "a")]
        [InlineData(1, "aaa", "a")]
        [InlineData(1, "aa", "aa")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int authorId, string firstName, string lastName)
        {
            //arrance

            UpdateAuthorCommand updateAuthorCommand = new UpdateAuthorCommand(null);
            updateAuthorCommand.AuthorId = authorId;
            updateAuthorCommand.Model = new UpdateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName
            };

            //act

            UpdateAuthorCommandValidator validation = new UpdateAuthorCommandValidator();
            var result = validation.Validate(updateAuthorCommand);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange

            UpdateAuthorCommand updateAuthorCommand = new UpdateAuthorCommand(null);
            updateAuthorCommand.AuthorId = 1;
            updateAuthorCommand.Model = new UpdateAuthorModel()
            {
                FirstName = "serkanozeldeneme",
                LastName = "serkanozeldeneme"
            };

            //act

            UpdateAuthorCommandValidator validation = new UpdateAuthorCommandValidator();
            var result = validation.Validate(updateAuthorCommand);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
