using AutoMapper;
using BookStore.Application.UserOperations.Queries.GetUserDetail;
using BookStore.DBOperations;
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
    public class GetUserDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetUserDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputAreGiven_User_ShouldBeDetailed()
        {
            //arrange

            GetUserDetailQuery getUserDetailQuery = new GetUserDetailQuery(_context, _mapper);
            getUserDetailQuery.UserId = 1;

            //act
            FluentActions.Invoking(() => getUserDetailQuery.Handle()).Invoke();

            //var a= FluentActions.Invoking(() => getUserDetailQuery.Handle()).Invoke();
        }

        [Fact]
        public void WhenInvalidInputAreGiven_User_ShouldNotBeDetailed()
        {
            //arrange
            GetUserDetailQuery getUserDetailQuery = new GetUserDetailQuery(_context, _mapper);
            //getUserDetailQuery.UserId = 0;

            //act
            FluentActions.Invoking(() => getUserDetailQuery.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("User bulunamadı");
        }
    }
}
