using AutoMapper;
using BookStore.Application.AuthorOperations.Commands.CreateAuthor;
using BookStore.Application.AuthorOperations.Commands.DeleteAuthor;
using BookStore.Application.AuthorOperations.Commands.UpdateAuthor;
using BookStore.Application.AuthorOperations.Queries.GetAuthorDetail;
using BookStore.Application.AuthorOperations.Queries.GetAuthors;
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
    public class AuthorController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public AuthorController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAuthors()
        {
            GetAuthorsQuery getAuthorsQuery = new GetAuthorsQuery(_context, _mapper);
            var result = getAuthorsQuery.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            AuthorDetailViewModel result;

            GetAuthorDetailQuery getAuthorDetailQuery = new GetAuthorDetailQuery(_context, _mapper);
            getAuthorDetailQuery.AuthorId = id;

            GetAuthorDetailQueryValidator validation = new GetAuthorDetailQueryValidator();
            validation.ValidateAndThrow(getAuthorDetailQuery);

            result = getAuthorDetailQuery.Handle();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult AddAuthor([FromBody] CreateAuthorModel createAuthor)
        {
            CreateAuthorCommand createAuthorCommand = new CreateAuthorCommand(_context, _mapper);
            createAuthorCommand.Model = createAuthor;

            CreateAuthorCommandValidator validation = new CreateAuthorCommandValidator();
            validation.ValidateAndThrow(createAuthorCommand);

            createAuthorCommand.Handle();

            return Ok("Yazar ekleme başarıyla gerçekleştirildi");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateAuthor(int id, [FromBody] UpdateAuthorModel updateAuthor)
        {
            UpdateAuthorCommand updateAuthorCommand = new UpdateAuthorCommand(_context);
            updateAuthorCommand.AuthorId = id;
            updateAuthorCommand.Model = updateAuthor;

            UpdateAuthorCommandValidator validation = new UpdateAuthorCommandValidator();
            validation.ValidateAndThrow(updateAuthorCommand);

            updateAuthorCommand.Handle();

            return Ok("Yazar bilgileri başarıyla güncellenmiştir");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteAuthor(int id)
        {
            DeleteAuthorCommand deleteAuthorCommand = new DeleteAuthorCommand(_context);
            deleteAuthorCommand.AuthorId = id;

            DeleteAuthorCommandValidator validation = new DeleteAuthorCommandValidator();
            validation.ValidateAndThrow(deleteAuthorCommand);

            deleteAuthorCommand.Handle();

            return Ok("Yazar sistemden başarıyla kaldırılmıştır. Tebrikler");
        }
    }
}
