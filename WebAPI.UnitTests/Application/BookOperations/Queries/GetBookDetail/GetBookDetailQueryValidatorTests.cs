using BookStore.Application.BookOperations.GetBookDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange

            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(null, null);
            getBookDetailQuery.BookId = 0;

            //act

            GetBookDetailQueryValidator validation = new GetBookDetailQueryValidator();
            var result = validation.Validate(getBookDetailQuery);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            //arrange

            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(null, null);
            getBookDetailQuery.BookId = 1;

            //act

            GetBookDetailQueryValidator validation = new GetBookDetailQueryValidator();
            var result = validation.Validate(getBookDetailQuery);

            //assert

            result.Errors.Count.Should().Be(0);
        }

    }
}
