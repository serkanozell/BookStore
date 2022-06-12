using BookStore.Application.GenreOperations.Commands.DeleteGenre;
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
    public class DeleteGenreCommandValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenValidInputAreGiven_Validator_ShouldNotBeReturnErrors()
        {
            //arrange

            DeleteGenreCommand deleteGenreCommand = new DeleteGenreCommand(null);
            deleteGenreCommand.GenreId = 1;

            //act

            DeleteGenreCommandValidator validation = new DeleteGenreCommandValidator();
            var result = validation.Validate(deleteGenreCommand);

            //assert
            result.Errors.Count().Should().Be(0);
        }
    }
}
