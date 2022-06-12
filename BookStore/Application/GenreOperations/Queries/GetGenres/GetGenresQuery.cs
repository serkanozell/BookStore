using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.GenreOperations.Queries.GetGenres
{
    public class GetGenresQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetGenresQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<GenresViewModel> Handle()
        {
            var genreList = _context.Genres.Where(g => g.IsActive).OrderBy(g => g.Id);
            List<GenresViewModel> genresViewModel = _mapper.Map<List<GenresViewModel>>(genreList);
            return genresViewModel;
        }
    }

    public class GenresViewModel
    {
        public int Id { get; set; }
        public string GenreName { get; set; }
    }
}
