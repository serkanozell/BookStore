using AutoMapper;
using BookStore.Application.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.UpdateBook
{
    public class UpdateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeUpdated()
        {
            //arrange

            UpdateBookCommand command = new UpdateBookCommand(_context);
            command.BookId = 1;
            UpdateBookModel updateBookModel = new UpdateBookModel()
            {
                Title = "Silmarillon",
                AuthorId = 2,
                GenreId = 1
            };
            command.Model = updateBookModel;

            //act
            FluentActions.Invoking(() => command.Handle())
                         .Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(b => b.Title == updateBookModel.Title);
            book.Should().NotBeNull();
            book.GenreId.Should().Be(updateBookModel.GenreId);
            book.AuthorId.Should().Be(updateBookModel.AuthorId);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Book_ShouldNotBeUpdated()
        {
            //arrange

            UpdateBookCommand command = new UpdateBookCommand(_context);
            UpdateBookModel updateBookModel = new UpdateBookModel()
            {
                Title = "Silmarillon",
                AuthorId = 2,
                GenreId = 1
            };
            command.Model = updateBookModel;

            //act
            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Böyle bir kitap bulunmamaktadır");
        }
    }
}
