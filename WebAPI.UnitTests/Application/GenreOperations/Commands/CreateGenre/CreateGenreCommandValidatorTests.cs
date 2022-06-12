using BookStore.Application.GenreOperations.Commands.CreateGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("a")]
        [InlineData("aa")]
        [InlineData("aaa")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(string genreName)
        {
            //arrange

            CreateGenreCommand createGenreCommand = new CreateGenreCommand(null, null);
            createGenreCommand.Model = new CreateGenreModel()
            {
                GenreName = genreName
            };

            //act

            CreateGenreCommandValidator validation = new CreateGenreCommandValidator();
            var result = validation.Validate(createGenreCommand);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldNotBeReturnError()
        {
            //arrange

            CreateGenreCommand createGenreCommand = new CreateGenreCommand(null, null);
            createGenreCommand.Model = new CreateGenreModel()
            {
                GenreName = "Genredenemetitle"
            };

            //act

            CreateGenreCommandValidator validation = new CreateGenreCommandValidator();
            var result = validation.Validate(createGenreCommand);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
