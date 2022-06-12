using BookStore.Application.BookOperations.UpdateBook;
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
    public class UpdateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Silmarillon", 0, 0, 0)]
        [InlineData("Silmarillon", 0, 0, 1)]
        [InlineData("Silmarillon", 1, 1, 0)]
        [InlineData("", 1, 0, 0)]
        [InlineData("", 1, 0, 1)]
        [InlineData("", 1, 1, 0)]
        [InlineData(" ", 4, 0, 5)]
        [InlineData(" ", 4, 5, 0)]
        [InlineData("si", 4, 6, 6)]
        [InlineData("sil", 4, 4, 5)]
        [InlineData("sil", 4, 5, 4)]
        [InlineData("silma", 6, 2, 6)]
        [InlineData("silma", 6, 6, 2)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int bookId, int authorId, int genreId)
        {
            //arrance

            UpdateBookCommand updateBookCommand = new UpdateBookCommand(null);
            updateBookCommand.BookId = bookId;
            updateBookCommand.Model = new UpdateBookModel()
            {
                Title = title,
                AuthorId = authorId,
                GenreId = genreId,

            };

            //act

            UpdateBookCommandValidator validation = new UpdateBookCommandValidator();
            var result = validation.Validate(updateBookCommand);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrance

            UpdateBookCommand updateBookCommand = new UpdateBookCommand(null);
            updateBookCommand.BookId = 1;
            updateBookCommand.Model = new UpdateBookModel()
            {
                Title = "Silmarillon",
                AuthorId = 2,
                GenreId = 2
            };

            //act

            UpdateBookCommandValidator validation = new UpdateBookCommandValidator();
            var result = validation.Validate(updateBookCommand);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
