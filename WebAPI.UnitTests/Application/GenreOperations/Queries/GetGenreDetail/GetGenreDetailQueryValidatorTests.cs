using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Queries.GetGenreDetail
{
    public class GetGenreDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange

            GetGenreDetailQuery getGenreDetailQuery = new GetGenreDetailQuery(null, null);
            getGenreDetailQuery.GenreId = 0;

            //act

            GetGenreDetailQueryValidator validation = new GetGenreDetailQueryValidator();
            var result = validation.Validate(getGenreDetailQuery);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            //arrange

            GetGenreDetailQuery getGenreDetailQuery = new GetGenreDetailQuery(null, null);
            getGenreDetailQuery.GenreId = 1;

            //act

            GetGenreDetailQueryValidator validation = new GetGenreDetailQueryValidator();
            var result = validation.Validate(getGenreDetailQuery);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
