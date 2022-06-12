using BookStore.Application.BookOperations.Commands.CreateBook;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;
using static BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("Lord Of The Rings", 0, 0)]
        [InlineData("Lord Of The Rings", 0, 1)]
        [InlineData("Lord Of The Rings", 100, 0)]
        [InlineData("", 0, 0)]
        [InlineData("", 100, 1)]
        [InlineData("", 0, 1)]
        [InlineData("Lor", 100, 1)]
        [InlineData("Lord", 100, 0)]
        [InlineData("Lord", 0, 1)]
        [InlineData(" ", 100, 1)]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string title, int pageCount, int genreId)
        {
            //arrance

            CreateBookCommand createBookCommand = new CreateBookCommand(null, null);
            createBookCommand.Model = new CreateBookModel()
            {
                Title = title,
                PageCount = pageCount,
                PublishDate = DateTime.Now.Date.AddYears(-1),
                GenreId = genreId
            };

            //act

            CreateBookCommandValidator validation = new CreateBookCommandValidator();
            var result = validation.Validate(createBookCommand);

            //assert
            result.Errors.Count.Should().BeGreaterThan(0);
        }


        [Fact]
        public void WhenDateTimeEqualNowIsGiven_Validator_ShouldBeReturnError()
        {
            //arrange

            CreateBookCommand createBookCommand = new CreateBookCommand(null, null);
            createBookCommand.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date,
                GenreId = 1
            };

            //act

            CreateBookCommandValidator validation = new CreateBookCommandValidator();
            var result = validation.Validate(createBookCommand);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange

            CreateBookCommand createBookCommand = new CreateBookCommand(null, null);
            createBookCommand.Model = new CreateBookModel()
            {
                Title = "Lord Of The Rings",
                PageCount = 100,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 1,
                AuthorId = 1
            };

            //act

            CreateBookCommandValidator validation = new CreateBookCommandValidator();
            var result = validation.Validate(createBookCommand);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
