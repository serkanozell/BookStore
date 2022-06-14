using BookStore.Application.UserOperations.Commands.CreateUser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandValidatorTests : IClassFixture<CommonTestFixture>
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

            CreateUserCommand createUserCommand = new CreateUserCommand(null, null);
            createUserCommand.Model = new CreateUserModel()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password
            };

            //act
            CreateUserCommandValidator validation = new CreateUserCommandValidator();
            var result = validation.Validate(createUserCommand);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange

            CreateUserCommand createUserCommand = new CreateUserCommand(null, null);
            createUserCommand.Model = new CreateUserModel()
            {
                FirstName = "SerkanOzeldeneme",
                LastName = "SerkanOzelSoyIsımDeneme",
                Email = "serkanozell@github.com",
                Password = "Serkan1234!!"
            };

            //act

            CreateUserCommandValidator validation = new CreateUserCommandValidator();
            var result = validation.Validate(createUserCommand);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
