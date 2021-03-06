using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BookStore.Application.BookOperations.Commands.CreateBook.CreateBookCommand;

namespace BookStore.Application.BookOperations.UpdateBook
{
    public class UpdateBookCommand
    {
        public int BookId { get; set; }
        public UpdateBookModel Model { get; set; }
        private readonly IBookStoreDbContext _context;

        public UpdateBookCommand(IBookStoreDbContext context)
        {
            _context = context;
        }


        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Böyle bir kitap bulunmamaktadır");
            }

            book.Title = Model.Title != default ? Model.Title : book.Title;
            book.GenreId = Model.GenreId != default ? Model.GenreId : book.GenreId;
            book.AuthorId = Model.AuthorId != default ? Model.AuthorId : book.AuthorId;

            _context.Books.Update(book);
            _context.SaveChanges();

        }


    }
    public class UpdateBookModel
    {
        public string Title { get; set; }
        public int GenreId { get; set; }
        public int AuthorId { get; set; }
    }
}
