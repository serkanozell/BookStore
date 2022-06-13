using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        public DeleteAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
        }

        [Fact]
        public void WhenValidInputIsGivenAndAuthorHasPublishedBook_Author_ShouldNotBeDeleted()
        {
            //arrange

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 1;

            //act

            //FluentActions.Invoking(() => command.Handle()).Invoke();

            FluentActions.Invoking(() => command.Handle())
             .Should()
             .Throw<InvalidOperationException>().And.Message
             .Should()
             .Be("Yazara ait kitap bulunmaktadır");

            //FluentActions.Invoking(() => command.Handle())
            //             .Should()
            //             .Throw<InvalidOperationException>().And.Message
            //             .Should()
            //             .BeEquivalentTo("Yazara ait kitap bulunmaktadır");
        }

        [Fact]
        public void WhenAlreadyDeletedAuthorIsGiven_InvalidOperationException_ShouldBeReturn()
        {
            //arrange

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);

            //act & assert

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Silmeye çalıştığınız yazar zaten sistemimizde mevcut değil!!");
        }



        [Fact]
        public void WhenValidInpuIsGivenAndAuthorHasNotPuslishedBook_Author_ShouldBeDeleted()
        {
            //arrange

            DeleteAuthorCommand command = new DeleteAuthorCommand(_context);
            command.AuthorId = 4;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();
        }
    }
}
