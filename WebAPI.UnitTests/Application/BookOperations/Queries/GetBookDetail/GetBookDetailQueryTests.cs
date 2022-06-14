using AutoMapper;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.DBOperations;
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
    public class GetBookDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputAreGiven_Book_ShouldBeDetailed()
        {
            //arrange

            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);
            getBookDetailQuery.BookId = 1;

            //act
            FluentActions.Invoking(() => getBookDetailQuery.Handle()).Invoke();

            //var a = FluentActions.Invoking(() => getBookDetailQuery.Handle()).Invoke();
        }

        [Fact]
        public void WhenInvalidInputAreGiven_Book_ShouldNotBeDetailed()
        {
            //arrange

            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);
            getBookDetailQuery.BookId = 0;

            //act
            FluentActions.Invoking(() => getBookDetailQuery.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Kitap Bulunamadı");
        }
    }
}
