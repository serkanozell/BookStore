using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.DBOperations;
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
    public class GetGenreDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreDetailQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputAreGiven_Genre_ShouldBeDetailed()
        {
            //arrange

            GetGenreDetailQuery getGenreDetailQuery = new GetGenreDetailQuery(_context, _mapper);
            getGenreDetailQuery.GenreId = 1;

            //act
            FluentActions.Invoking(() => getGenreDetailQuery.Handle()).Invoke();

            var a = FluentActions.Invoking(() => getGenreDetailQuery.Handle()).Invoke();
        }

        [Fact]
        public void WhenInvalidInputAreGiven_Genre_ShouldNotBeDetailed()
        {
            //arrange

            GetGenreDetailQuery getGenreDetailQuery = new GetGenreDetailQuery(_context, _mapper);
            getGenreDetailQuery.GenreId = 0;

            //act
            FluentActions.Invoking(() => getGenreDetailQuery.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Kitap türü bulunamadı");
        }
    }
}
