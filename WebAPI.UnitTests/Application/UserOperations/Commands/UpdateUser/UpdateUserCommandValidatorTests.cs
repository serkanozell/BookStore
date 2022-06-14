using BookStore.Application.UserOperations.Commands.UpdateUser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("", "", "", "")]
        [InlineData(" ", " ", " ", " ")]
        [InlineData("a", "", "", "")]
        [InlineData("aa", "", "", "")]
        [InlineData("", "a", "", "")]
        [InlineData("", "aa", "", "")]
        [InlineData("aaa", "aaa", "", "")]
        [InlineData("", "", " ", "")]
        [InlineData("", "", "asdasdasdasd", "")]
        [InlineData("a", "aaa", "asdasdasdasd", " ")]
        [InlineData("aaa", "aaa", "denememaili12", " ")]
        [InlineData("aaa", "aaa", "asdasd", "Password123")]
        [InlineData("aaa", "aaa", "gdfh", "Password123? ")]
        [InlineData("aaaa", "a", "asdafghgfsdasdasd", "asdasdASddd23.")]

        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string firstName,
    string lastName, string email, string password)
        {
            //arrange

            UpdateUserCommand updateUserCommand = new UpdateUserCommand(null);
            updateUserCommand.Model = new UpdateUserModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            //act
            UpdateUserCommandValidator validation = new UpdateUserCommandValidator();
            var result = validation.Validate(updateUserCommand);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange

            UpdateUserCommand updateUserCommand = new UpdateUserCommand(null);
            updateUserCommand.UserId = 1;
            updateUserCommand.Model = new UpdateUserModel()
            {
                FirstName = "serkanltestfirstname",
                LastName = "serkaneltestlastname",
                Email = "serkanstme@mail.com",
                Password = "seakasttname!1X"
            };

            //act

            UpdateUserCommandValidator validation = new UpdateUserCommandValidator();
            var result = validation.Validate(updateUserCommand);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
