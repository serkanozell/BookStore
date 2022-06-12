using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuthorCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Author_ShouldBeUpdated()
        {
            //arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            command.AuthorId = 1;
            UpdateAuthorModel updateAuthorModel = new UpdateAuthorModel()
            {
                FirstName = "denemeveri12",
                LastName = "denemeveri12"
            };
            command.Model = updateAuthorModel;

            //act
            FluentActions.Invoking(() => command.Handle()).Invoke();

            //assert

            var author = _context.Authors.SingleOrDefault(a => a.FirstName == updateAuthorModel.FirstName
                                                               && a.LastName == updateAuthorModel.LastName);

            author.Should().NotBeNull();
            author.FirstName.Should().Be(updateAuthorModel.FirstName);
            author.LastName.Should().Be(updateAuthorModel.LastName);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Author_ShouldNotBeUpdated()
        {
            //arrange

            UpdateAuthorCommand command = new UpdateAuthorCommand(_context);
            UpdateAuthorModel updateAuthorModel = new UpdateAuthorModel()
            {
                FirstName = "denemeyazaradi",
                LastName = "denemeyazarsoyadi"
            };
            command.Model = updateAuthorModel;

            //act

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Güncellemeye çalıştığınız yazar sistemde mevcut değil. Lütfen geçerli bir yazar id si giriniz");
        }
    }
}
