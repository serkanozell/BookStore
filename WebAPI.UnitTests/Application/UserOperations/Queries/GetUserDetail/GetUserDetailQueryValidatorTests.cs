using BookStore.Application.UserOperations.Queries.GetUserDetail;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQueryValidatorTests : IClassFixture<CommonTestFixture>
    {

        [Fact]
        public void WhenInvalidInputsAreGiven_ValidatorShouldBeReturnErrors()
        {
            //arrange

            GetUserDetailQuery getUserDetailQuery = new GetUserDetailQuery(null, null);
            //getUserDetailQuery.UserId = 0;

            //bazı örneklerde id verilerek yapılmasının
            //amacı iki şekilde de çalıştığını göstermek


            //act

            GetUserDetailQueryValidator validation = new GetUserDetailQueryValidator();
            var result = validation.Validate(getUserDetailQuery);

            //assert

            result.Errors.Count.Should().BeGreaterThan(0);
        }

        [Fact]
        public void WhenValidInputsAreGiven_Validator_ShouldBeReturnSuccess()
        {
            //arrange

            GetUserDetailQuery getUserDetailQuery = new GetUserDetailQuery(null, null);
            getUserDetailQuery.UserId = 1;

            //act

            GetUserDetailQueryValidator validation = new GetUserDetailQueryValidator();
            var result = validation.Validate(getUserDetailQuery);

            //assert

            result.Errors.Count.Should().Be(0);
        }

    }
}
