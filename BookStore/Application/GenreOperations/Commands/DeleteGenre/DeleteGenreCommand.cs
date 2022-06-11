using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Commands.DeleteGenre
{
    public class DeleteGenreCommand
    {
        public int GenreId { get; set; }
        private readonly BookStoreDbContext _context;

        public DeleteGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == GenreId);
            if (genre is not null)
            {
                throw new InvalidOperationException("Böyle bir kitap türü bulunmamaktadır!!");
            }

            _context.Genres.Remove(genre);
        }
    }
}
