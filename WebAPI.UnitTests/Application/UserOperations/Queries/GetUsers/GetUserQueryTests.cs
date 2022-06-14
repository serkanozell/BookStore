using AutoMapper;
using BookStore.Application.UserOperations.Queries.GetUsers;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.UserOperations.Queries.GetUsers
{
    public class GetUserQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetUserQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenQueryGivenForAll_Users_ShouldBeReturnUser()
        {
            //arrange

            GetUsersQuery getUserQuery = new GetUsersQuery(_context, _mapper);
            FluentActions.Invoking(() => getUserQuery.Handle()).Invoke();
        }
    }
}
