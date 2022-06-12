using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Queries.GetAuthorDetail
{
    public class GetAuthorDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {
        [Fact]
        public void WhenInvalidInputsAreGiven_Validator_ShouldBeReturnErrors()
        {
            //arrange

            GetAuthorDetailQuery getAuthorDetailQuery = new GetAuthorDetailQuery(null, null);
            getAuthorDetailQuery.AuthorId = 0;

            //act

            GetAuthorDetailQueryValidator validation = new GetAuthorDetailQueryValidator();
            var result = validation.Validate(getAuthorDetailQuery);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            //arrange

            GetAuthorDetailQuery getAuthorDetailQuery = new GetAuthorDetailQuery(null, null);
            getAuthorDetailQuery.AuthorId = 4;

            //act

            GetAuthorDetailQueryValidator validation = new GetAuthorDetailQueryValidator();
            var result = validation.Validate(getAuthorDetailQuery);

            //assert

            result.Errors.Count.Should().Be(0);
        }
    }
}
