using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
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

namespace WebAPI.UnitTests.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly BookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateGenreCommandTests(CommonTestFixture commonTestFixture)
        {
            _context = commonTestFixture.Context;
            _mapper = commonTestFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistGenreNameIsGiven_InvaliOperationException_ShouldBeReturn()
        {
            //arrange

            var genre = new Genre()
            {
                GenreName = "WhenAlreadyExistGenreNameIsGiven_InvaliOperationException_ShouldBeReturn",
                IsActive = true
            };

            _context.Genres.Add(genre);
            _context.SaveChanges();

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            command.Model = new CreateGenreModel() { GenreName = genre.GenreName };

            //act & assert 

            FluentActions.Invoking(() => command.Handle())
                         .Should()
                         .Throw<InvalidOperationException>().And.Message
                         .Should()
                         .Be("Kitap türü zaten mevcut");
        }

        [Fact]
        public void WhenValidInputsAreGiven_Genre_ShouldBeCreated()
        {
            //arrange

            CreateGenreCommand command = new CreateGenreCommand(_context, _mapper);
            CreateGenreModel createGenreModel = new CreateGenreModel()
            {
                GenreName = "GenredenemeTitle"
            };
            command.Model = createGenreModel;

            //act

            FluentActions.Invoking(() => command.Handle())
                         .Invoke();

            //assert

            var genre = _context.Genres.SingleOrDefault(g => g.GenreName == createGenreModel.GenreName);
            genre.Should().NotBeNull();
            genre.GenreName.Should().Be(createGenreModel.GenreName);
        }
    }


}
