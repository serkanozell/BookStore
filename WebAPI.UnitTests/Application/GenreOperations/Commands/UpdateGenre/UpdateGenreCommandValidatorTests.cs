using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Theory]
        [InlineData(0,"")]
        [InlineData(0," ")]
        [InlineData(0,"a")]
        [InlineData(0,"aa")]
        [InlineData(0,"aaa")]
        [InlineData(1,"")]
        [InlineData(1," ")]
        [InlineData(1,"a")]
        [InlineData(1,"aa")]
        [InlineData(1,"aaa")]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors(int genreId,string genreName)
        {
            //arrance

            UpdateGenreCommand updateGenreCommand = new UpdateGenreCommand(null);
            updateGenreCommand.GenreId = genreId;
            updateGenreCommand.Model = new UpdateGenreModel()
            {
                GenreName = genreName
            };

            //act

            UpdateGenreCommandValidator validation = new UpdateGenreCommandValidator();
            var result = validation.Validate(updateGenreCommand);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }
    }
}
