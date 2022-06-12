using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandvalidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "")]
        [InlineData("", " ")]
        [InlineData(" ", "")]
        [InlineData(" ", " ")]
        [InlineData("a", "a")]
        [InlineData("a", "ab")]
        [InlineData("a", "aaa")]
        [InlineData("a", "aaaa")]
        [InlineData("aa", "a")]
        [InlineData("aaa", "a")]
        [InlineData("aaaa", "a")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName, string lastName)
        {
            //arrance

            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(null, null);
            createAuthorCommand.Model = new CreateAuthorModel()
            {
                FirstName = firstName,
                LastName = lastName,
                BirthDate = DateTime.Now.Date.AddDays(-30)
            };

            //act

            CreateAuthorCommandValidator validation = new CreateAuthorCommandValidator();
            var result = validation.Validate(createAuthorCommand);

            //assert
            result.Errors.Count()
                         .Should()
                         .BeGreaterThan(0);
        }

        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange
            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(null, null);
            createAuthorCommand.Model = new CreateAuthorModel()
            {
                FirstName = "denemeveri",
                LastName = "denemeveri",
                BirthDate = DateTime.Now.Date
            };

            //act
            CreateAuthorCommandValidator validation = new CreateAuthorCommandValidator();
            var result = validation.Validate(createAuthorCommand);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidDateTimeInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange

            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(null, null);
            createAuthorCommand.Model = new CreateAuthorModel()
            {
                FirstName = "yazar1",
                LastName = "yazar1",
                BirthDate = DateTime.Now.Date.AddYears(-30)
            };

            //act

            CreateAuthorCommandValidator validation = new CreateAuthorCommandValidator();
            var result = validation.Validate(createAuthorCommand);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
