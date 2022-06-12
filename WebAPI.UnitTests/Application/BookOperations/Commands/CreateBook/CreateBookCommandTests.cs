using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.DBOperations;
using BookStore.Entities;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;
using static BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace WebAPI.UnitTests.Application.BookOperations.Commands.CreateBook
{
    public class CreateBookCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateBookCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistBookTitleIsGiven_InvaliOperationException_ShouldBeReturn()
        {
            //arrange =>(Hazırlık)

            var book = new Book()
            {
                Title = "WhenAlreadyExistBookTitleIsGiven_InvaliOperationException_ShouldBeReturn",
                PageCount = 100,
                PublishDate = new DateTime(1990, 01, 10),
                GenreId = 1
            };
            _context.Books.Add(book);
            _context.SaveChanges();

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            command.Model = new CreateBookModel() { Title = book.Title };


            //act & assert => (Çalıştırma - Doğrulama)

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Kitap zaten mevcut!!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Book_ShouldBeCreated()
        {
            //arrange

            CreateBookCommand command = new CreateBookCommand(_context, _mapper);
            CreateBookModel createBookModel = new CreateBookModel()
            {
                Title = "Hobbit",
                PageCount = 1000,
                PublishDate = DateTime.Now.Date.AddYears(-10),
                GenreId = 1,
                AuthorId = 1
            };
            command.Model = createBookModel;

            //act
            FluentActions.Invoking(() => command.Handle())
                         .Invoke();

            //assert
            var book = _context.Books.SingleOrDefault(b => b.Title == createBookModel.Title);
            book.Should().NotBeNull();
            book.PageCount.Should().Be(createBookModel.PageCount);
            book.PublishDate.Should().Be(createBookModel.PublishDate);
            //book.Title.Should().Be(createBookModel.Title); zaten ilk kontrolde yapıldığı için gerekli değil
            book.GenreId.Should().Be(createBookModel.GenreId);
            book.AuthorId.Should().Be(createBookModel.AuthorId);
        }
    }
}
