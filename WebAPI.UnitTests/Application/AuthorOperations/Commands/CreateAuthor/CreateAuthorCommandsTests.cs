using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
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

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandsTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateAuthorCommandsTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistAuthorIsGiven_InvaliOperationException_ShouldBeReturn()
        {
            //arrange

            var author = new Author()
            {
                FirstName = "deneme",
                LastName = "deneme",
                BirthDate = new DateTime(2000, 01, 01)
            };
            _context.Authors.Add(author);
            _context.SaveChanges();

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            command.Model = new CreateAuthorModel()
            {
                FirstName = author.FirstName,
                LastName = author.LastName,
                BirthDate = author.BirthDate
            };

            //act & assert 

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Sistemde zaten böyle bir yazar mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeCreated()
        {
            //arrange

            CreateAuthorCommand command = new CreateAuthorCommand(_context, _mapper);
            CreateAuthorModel createAuthorModel = new CreateAuthorModel()
            {
                FirstName = "denemeisim",
                LastName = "denemeSoyisim",
                BirthDate = new DateTime(2000, 01, 01)
            };
            command.Model = createAuthorModel;

            //act
            FluentActions.Invoking(() => command.Handle())
                         .Invoke();

            //Assert
            var author = _context.Authors.SingleOrDefault(a => a.FirstName == createAuthorModel.FirstName
                                                               && a.LastName == createAuthorModel.LastName
                                                               && a.BirthDate == createAuthorModel.BirthDate);
            author.Should().NotBeNull();
            author.FirstName.Should().Be(createAuthorModel.FirstName);
            author.LastName.Should().Be(createAuthorModel.LastName);
        }
    }
}
