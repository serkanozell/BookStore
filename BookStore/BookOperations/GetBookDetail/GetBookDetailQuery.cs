using BookStore.Common;
using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.BookOperations.GetBookDetail
{
    public class GetBookDetailQuery
    {
        public int BookId { get; set; }
        private readonly BookStoreDbContext _context;

        public GetBookDetailQuery(BookStoreDbContext context)
        {
            _context = context;
        }

        public BookDetailViewModel Handle()
        {
            BookDetailViewModel vM = new BookDetailViewModel();
            var book = _context.Books.Where(b => b.Id == BookId).SingleOrDefault();
            if (book is null)
            {
                throw new InvalidOperationException("Kitap Bulunamadı");
            }

            vM.Title = book.Title;
            vM.Genre = ((GenreEnum)book.GenreId).ToString();
            vM.PageCount = book.PageCount;
            vM.PublishDate = book.PublishDate.Date.ToString("dd/MM/yyyy");

            return vM;
        }
    }

    public class BookDetailViewModel
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public int PageCount { get; set; }
        public string PublishDate { get; set; }
    }
}
