using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Commands.DeleteAuthor
{
    public class DeleteAuthorCommand
    {
        public int AuthorId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == AuthorId);
            var book = _context.Books.SingleOrDefault(a => a.AuthorId == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Silmeye çalıştığınız yazar zaten sistemimizde mevcut değil!!");
            }
            if (book is not null)
            {
                throw new InvalidOperationException("Yazara ait kitap bulunmaktadır");
            }

            _context.Authors.Remove(author);
            _context.SaveChanges();
        }
    }
}
