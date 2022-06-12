using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.TestSetup
{
    public static class Genres
    {
        public static void AddGenres(this BookStoreDbContext context)
        {
            context.Genres.AddRange(
                    new Genre
                    {
                        GenreName = "Personel Growth"
                    },
                    new Genre
                    {
                        GenreName = "Science Fiction"
                    },
                    new Genre
                    {
                        GenreName = "Romance"
                    }
                );
        }
    }
}
