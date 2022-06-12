using BookStore.Application.BookOperations.Commands.DeleteBook;
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

namespace WebAPI.UnitTests.Application.BookOperations.Commands.DeleteBook
{
    public class DeleteBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenAlreadyDeletedBookIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange

            DeleteBookCommand command = new DeleteBookCommand(_context);

            //act & assert

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Silmek için böyle bir kitap bulunmamaktadır");
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldBeDeleted()
        {
            //arrange

            DeleteBookCommand command = new DeleteBookCommand(_context);
            command.BookId = 3;

            //act

            FluentActions.Invoking(() => command.Handle()).Invoke();
        }


    }
}
