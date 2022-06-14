using BookStore.Application.UserOperations.Commands.DeleteUser;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange

            DeleteUserCommand deleteUserCommand = new DeleteUserCommand(null);
            deleteUserCommand.UserId = 1;

            //act

            DeleteUserCommandValidator validation = new DeleteUserCommandValidator();
            var result = validation.Validate(deleteUserCommand);

            //assert
            result.Errors.Count.Should().Be(0);
        }
    }
}
