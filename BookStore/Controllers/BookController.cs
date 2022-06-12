using AutoMapper;
using BookStore.Application.BookOperations.Commands.CreateBook;
using BookStore.Application.BookOperations.Commands.DeleteBook;
using BookStore.Application.BookOperations.GetBookDetail;
using BookStore.Application.BookOperations.GetBooks;
using BookStore.Application.BookOperations.UpdateBook;
using BookStore.DBOperations;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using static BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public BookController(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            GetBooksQuery query = new GetBooksQuery(_context, _mapper);
            var result = query.Handle();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            BookDetailViewModel result;

            GetBookDetailQuery getBookDetailQuery = new GetBookDetailQuery(_context, _mapper);
            getBookDetailQuery.BookId = id;

            GetBookDetailQueryValidator validation = new GetBookDetailQueryValidator();
            validation.ValidateAndThrow(getBookDetailQuery);

            result = getBookDetailQuery.Handle();


            return Ok(result);
        }

        //[HttpGet]
        //public Book Get([FromQuery] string id)
        //{
        //    var book = BookList.Where(b => b.Id == int.Parse(id)).SingleOrDefault();
        //    return book;
        //}

        [HttpPost]
        public IActionResult AddBook([FromBody] CreateBookModel newBook)
        {

            CreateBookCommand createBookCommand = new CreateBookCommand(_context, _mapper);
            createBookCommand.Model = newBook;

            CreateBookCommandValidator validation = new CreateBookCommandValidator();
            validation.ValidateAndThrow(createBookCommand);

            createBookCommand.Handle();

            return Ok("Ekleme Başarılı");
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBook(int id, [FromBody] UpdateBookModel updatedBook)
        {

            UpdateBookCommand updateBookCommand = new UpdateBookCommand(_context);
            updateBookCommand.BookId = id;
            updateBookCommand.Model = updatedBook;

            UpdateBookCommandValidator validation = new UpdateBookCommandValidator();
            validation.ValidateAndThrow(updateBookCommand);

            updateBookCommand.Handle();

            return Ok("Guncelleme Basarili");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            DeleteBookCommand deleteBookCommand = new DeleteBookCommand(_context);
            deleteBookCommand.BookId = id;
            DeleteBookCommandValidator validation = new DeleteBookCommandValidator();
            validation.ValidateAndThrow(deleteBookCommand);

            deleteBookCommand.Handle();

            return Ok("kitap silindi");
        }

    }
}
