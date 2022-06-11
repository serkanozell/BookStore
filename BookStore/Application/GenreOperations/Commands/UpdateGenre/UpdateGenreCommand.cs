using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Commands.UpdateGenre
{
    public class UpdateGenreCommand
    {
        public int GenreId { get; set; }
        public UpdateGenreModel Model { get; set; }
        private readonly BookStoreDbContext _context;

        public UpdateGenreCommand(BookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g => g.Id == GenreId);
            if (genre is null)
            {
                throw new InvalidOperationException("Güncellemek için böyle bir kitap türü mevcut değil!!");
            }

            if (_context.Genres.Any(g => g.GenreName.ToLower() == Model.GenreName.ToLower() && g.Id != GenreId))
            {
                throw new InvalidOperationException("Aynı isimli bir kitap türü zaten mevcut!!");
            }

            genre.GenreName = string.IsNullOrEmpty(Model.GenreName.Trim()) ? genre.GenreName : Model.GenreName;
            //genre.GenreName = Model.GenreName != default ? Model.GenreName : genre.GenreName; //farklı yazılış türü
            genre.IsActive = Model.IsActive;
            _context.SaveChanges();
        }
    }

    public class UpdateGenreModel
    {
        //public int GenreId { get; set; }
        public string GenreName { get; set; }
        public bool IsActive { get; set; }
    }
}
