using AutoMapper;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Queries.GetAuthors
{
    public class GetAuthorQueryTest : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetAuthorQueryTest(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenQueryGivenForAll_Books_ShouldBeReturnBookList()
        {
            //arrange

            GetAuthorsQuery getAuthorsQuery = new GetAuthorsQuery(_context, _mapper);

            //act
            FluentActions.Invoking(() => getAuthorsQuery.Handle()).Invoke();
        }
    }
}
