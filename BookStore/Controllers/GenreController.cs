using AutoMapper;
using BookStore.Application.GenreOperations.Commands.CreateGenre;
using BookStore.Application.GenreOperations.Commands.DeleteGenre;
using BookStore.Application.GenreOperations.Commands.UpdateGenre;
using BookStore.Application.GenreOperations.Queries.GetGenreDetail;
using BookStore.Application.GenreOperations.Queries.GetGenres;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GenreController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetGenres()
        {
            GetGenresQuery query = new GetGenresQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            GenreDetailViewModel result;

            GetGenreDetailQuery getGenreDetailQuery = new GetGenreDetailQuery(_context, _mapper);
            getGenreDetailQuery.GenreId = id;

            GetGenreDetailQueryValidator validation = new GetGenreDetailQueryValidator();
            validation.ValidateAndThrow(getGenreDetailQuery);

            result = getGenreDetailQuery.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddGenre([FromBody] CreateGenreModel newGenre)
        {
            CreateGenreCommand createGenre = new CreateGenreCommand(_context);
            createGenre.Model = newGenre;

            CreateGenreCommandValidator validation = new CreateGenreCommandValidator();
            validation.ValidateAndThrow(createGenre);

            createGenre.Handle();

            return Ok("Ekleme Başarılı");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateGenre(int id, [FromBody] UpdateGenreModel updateGenre)
        {
            UpdateGenreCommand updateGenreCommand = new UpdateGenreCommand(_context);
            updateGenreCommand.GenreId = id;
            updateGenreCommand.Model = updateGenre;

            UpdateGenreCommandValidator validation = new UpdateGenreCommandValidator();
            validation.ValidateAndThrow(updateGenreCommand);

            updateGenreCommand.Handle();

            return Ok("Güncelleme başarılı");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteGenre(int id)
        {
            DeleteGenreCommand deleteGenreCommand = new DeleteGenreCommand(_context);
            deleteGenreCommand.GenreId = id;

            DeleteGenreCommandValidator validation = new DeleteGenreCommandValidator();
            validation.ValidateAndThrow(deleteGenreCommand);

            deleteGenreCommand.Handle();

            return Ok("Kitap türü başarıyla silindi");
        }
    }
}
