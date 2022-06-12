using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenAlreadyDeletedGenreIdIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange

            DeleteGenreCommand command = new DeleteGenreCommand(_context);

            //act & assert

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Böyle bir kitap türü bulunmamaktadır!!");
        }

        [Fact]
        public void WhenValidInputIsGiven_Book_ShouldBeDeleted()
        {
            //arrange

            DeleteGenreCommand command = new DeleteGenreCommand(_context);
            command.GenreId = 1;

            //act

            FluentActions.Invoking(() => command.Handle());
        }
    }
}
