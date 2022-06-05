using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.DeleteBook
{
    public class DeleteBookCommand
    {
        public int BookId { get; set; }

        private readonly BookStoreDbContext _context;

        public DeleteBookCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == BookId);
            if (book is null)
            {
                throw new InvalidOperationException("Silmek için böyle bir kitap bulunmamaktadır");
            }

            _context.Books.Remove(book);
            _context.SaveChanges();
        }
    }
}
