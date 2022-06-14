using BookStore.Application.UserOperations.Commands.DeleteUser;
using BookStore.DBOperations;
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
    public class DeleteUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteUserCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenAlreadyDeletedUserIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange

            DeleteUserCommand command = new DeleteUserCommand(_context);

            //act & assert

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Sistemde böyle bir kullanıcı bulunmamaktadır");


        }

        [Fact]
        public void WhenValidInputIsGiven_User_ShouldBeDeleted()
        {
            //arrange

            DeleteUserCommand command = new DeleteUserCommand(_context);
            command.UserId = 3;

            //act

            FluentActions.Invoking(() => command.Handle()).Invoke();
        }
    }
}
