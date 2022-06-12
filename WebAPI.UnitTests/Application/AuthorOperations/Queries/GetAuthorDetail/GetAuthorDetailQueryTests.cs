using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.DBOperations;
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
    public class GetAuthorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputAreGiven_Author_ShouldBeDetailed()
        {
            //arrange

            GetAuthorDetailQuery getAuthorDetailQuery = new GetAuthorDetailQuery(_context, _mapper);
            getAuthorDetailQuery.AuthorId = 1;

            //act
            FluentActions.Invoking(() => getAuthorDetailQuery.Handle()).Invoke();

            var a = FluentActions.Invoking(() => getAuthorDetailQuery.Handle()).Invoke();
        }
    }
}
