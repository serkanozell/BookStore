using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommand
    {
        public CreateGenreModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public CreateGenreCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var genre = _context.Genres.SingleOrDefault(g => g.GenreName == Model.GenreName);
            if (genre is not null)
            {
                throw new InvalidOperationException("Kitap türü zaten mevcut");
            }

            genre = _mapper.Map<Genre>(Model);
            //genre.GenreName = Model.GenreName;

            _context.Genres.Add(genre);
            _context.SaveChanges();
        }

    }
    public class CreateGenreModel
    {
        public string GenreName { get; set; }
    }
}
