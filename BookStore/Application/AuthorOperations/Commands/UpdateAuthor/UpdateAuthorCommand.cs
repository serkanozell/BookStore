using AutoMapper;
using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommand
    {
        public int AuthorId { get; set; }
        public UpdateAuthorModel Model { get; set; }
        private readonly IBookStoreDbContext _context;

        public UpdateAuthorCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == AuthorId);
            if (author is null)
            {
                throw new InvalidOperationException("Güncellemeye çalıştığınız yazar sistemde mevcut değil. Lütfen geçerli bir yazar id si giriniz");
            }

            author.FirstName = Model.FirstName != default ? Model.FirstName : author.FirstName;
            author.LastName = Model.LastName != default ? Model.LastName : author.LastName;
            //author.BirthDate = Model.LastName != default ? Model.BirthDate : author.BirthDate;

            _context.Authors.Update(author);
            _context.SaveChanges();
        }
    }

    public class UpdateAuthorModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public DateTime BirthDate { get; set; }
    }
}
