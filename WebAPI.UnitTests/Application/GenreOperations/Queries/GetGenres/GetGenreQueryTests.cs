using AutoMapper;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenreQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetGenreQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenQueryGivenForAll_Genres_ShouldBeReturnAuthor()
        {
            //arrange

            GetGenresQuery getGenresQuery = new GetGenresQuery(_context, _mapper);
            FluentActions.Invoking(() => getGenresQuery.Handle()).Invoke();
        }
    }
}
