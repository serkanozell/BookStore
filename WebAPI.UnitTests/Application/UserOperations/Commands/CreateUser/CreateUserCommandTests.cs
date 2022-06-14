using AutoMapper;
using BookStore.Application.UserOperations.Commands.CreateUser;
using BookStore.DBOperations;
using BookStore.Entities;
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
    public class CreateUserCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistUserIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange

            var user = new User()
            {
                FirstName = "WhenAlreadyExistUserIsGiven_InvalidOperationException_ShouldBeReturn",
                LastName = "WhenAlreadyExistUserIsGiven_InvalidOperationException_ShouldBeReturn",
                Email = "deneme@gmail.com",
                Password = "Serkan123_."
            };
            _context.Users.Add(user);
            _context.SaveChanges();

            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            command.Model = new CreateUserModel()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Password = user.Password
            };

            //act & assert

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Kullanıcı sistemde kayıtlı");
        }

        [Fact]
        public void WhenValidInputsAreGiven_User_ShouldBeCreated()
        {
            //arrange

            CreateUserCommand command = new CreateUserCommand(_context, _mapper);
            CreateUserModel createUserModel = new CreateUserModel()
            {
                FirstName = "Deeeenemeserkan",
                LastName = "SoyAdidenemeserkan",
                Email = "serkanozel12345@gmail.com",
                Password = "Serkan123Ozel44!"
            };

            command.Model = createUserModel;

            //act

            FluentActions.Invoking(() => command.Handle())
                         .Invoke();

            //assert

            var user = _context.Users.SingleOrDefault(u => u.Email == createUserModel.Email);
            user.Should().NotBeNull();
            user.FirstName.Should().Be(createUserModel.FirstName);
            user.LastName.Should().Be(createUserModel.LastName);
            user.Password.Should().Be(createUserModel.Password);
        }
    }
}
