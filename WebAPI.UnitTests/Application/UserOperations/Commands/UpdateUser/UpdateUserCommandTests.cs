using AutoMapper;
using BookStore.Application.UserOperations.Commands.UpdateUser;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Linq;
using WebAPI.UnitTests.TestSetup;
using Xunit;

;

namespace WebAPI.UnitTests.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;

        public UpdateUserCommandTests(CommonTestFixture commontestFixture)
        {
            _context = commontestFixture.Context;
            _mapper = commontestFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeUpdated()
        {
            //arrange

            UpdateUserCommand command = new UpdateUserCommand(_context);
            command.UserId = 1;
            UpdateUserModel updateUserModel = new UpdateUserModel()
            {
                Email = "testdeneme123@gmail.com",
                FirstName = "testismidenemeserkan",
                LastName = "testsoyisimdenemeozel",
                Password = "Serkan12312300.!"
            };
            command.Model = updateUserModel;

            //act

            FluentActions.Invoking(() => command.Handle()).Invoke();

            //asser

            var user = _context.Users.SingleOrDefault(u => u.Email == updateUserModel.Email);
            user.Should().NotBeNull();
            user.FirstName.Should().Be(updateUserModel.FirstName);
            user.LastName.Should().Be(updateUserModel.LastName);
            user.Password.Should().Be(updateUserModel.Password);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_User_ShouldNotBeUpdated()
        {
            //arrange

            UpdateUserCommand command = new UpdateUserCommand(_context);
            UpdateUserModel updateUserModel = new UpdateUserModel()
            {
                FirstName = "serkanserrkanserkanserkan",
                LastName = "ozelozel",
                Email = "serkanozel@ail.comserk",
                Password = "Serkanozelsifre!_"
            };

            //act

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Böyle bir user bulunmamaktadır");
        }
    }
}
