using AutoMapper;
using BookStore.Application.BookOperations.GetBooks;
using BookStore.DBOperations;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.BookOperations.Queries.GetBooks
{
    public class GetBookQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetBookQueryTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenQueryGivenForAll_Books_ShouldBeReturnBookList()
        {
            //arrange

            GetBooksQuery getBooksQuery = new GetBooksQuery(_context, _mapper);
            FluentActions.Invoking(() => getBooksQuery.Handle()).Invoke();

            //act
            //    var a = FluentActions.Invoking(() => getBooksQuery.Handle()).Invoke();
            //    var bookList = _context.Books.Include(b => b.Genre).Include(b => b.Author).OrderBy(b => b.Id).ToList();
        }
    }
}
