using AutoMapper;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.DBOperations;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.UnitTests.TestSetup;
using Xunit;

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeUpdated()
        {
            //arrange

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;
            UpdateGenreModel updateGenreModel = new UpdateGenreModel()
            {
                GenreName = "UpdateGenreDeneme",
                IsActive = true
            };
            command.Model = updateGenreModel;

            //act

            FluentActions.Invoking(() => command.Handle())
                         .Invoke();

            //assert

            var genre = _context.Genres.SingleOrDefault(g => g.GenreName == updateGenreModel.GenreName
                                                             && g.IsActive == updateGenreModel.IsActive);
            genre.Should().NotBeNull();
            genre.GenreName.Should().Be(updateGenreModel.GenreName);
            genre.IsActive.Should().Be(updateGenreModel.IsActive);
        }

        [Fact]
        public void WhenInvalidInputsAreGiven_Genre_ShouldNotBeUpdated()
        {
            //arrange

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            UpdateGenreModel updateGenreModel = new UpdateGenreModel()
            {
                GenreName = "GenreDeneme",
                IsActive = true
            };
            command.Model = updateGenreModel;

            //act

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Güncellemek için böyle bir kitap türü mevcut değil!!");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldNotBeUpdated()
        {
            //arrange

            UpdateGenreCommand command = new UpdateGenreCommand(_context);
            command.GenreId = 1;
            UpdateGenreModel updateGenreModel = new UpdateGenreModel()
            {
                GenreName = "Romance",
                IsActive = true
            };
            command.Model = updateGenreModel;

            //act

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Aynı isimli bir kitap türü zaten mevcut!!");
        }
    }
}
