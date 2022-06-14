using BookStore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.DBOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new BookStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookStoreDbContext>>()))
            {
                if (context.Books.Any())
                {
                    return;
                }
                context.Authors.AddRange(
                    new Author
                    {
                        FirstName = "Eric",
                        LastName = "Ries",
                        BirthDate = new DateTime(1978, 09, 22)
                    },
                    new Author
                    {
                        FirstName = "Serkan",
                        LastName = "Ozel",
                        BirthDate = new DateTime(1994, 09, 05)
                    },
                    new Author
                    {
                        FirstName = "Berkan",
                        LastName = "OZEL",
                        BirthDate = new DateTime(1990, 12, 21)
                    }
                    );
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
                context.Books.AddRange(
                  new Book
                  {
                      //Id = 1,
                      Title = "Lean StartUp",
                      GenreId = 1,//Personel Growth
                      AuthorId = 1,
                      PageCount = 200,
                      PublishDate = new DateTime(2001, 06, 12)
                  },
                  new Book
                  {
                      //Id = 2,
                      Title = "Herland",
                      GenreId = 2,//Science Fiction 
                      AuthorId = 2,
                      PageCount = 250,
                      PublishDate = new DateTime(2010, 05, 23)
                  },
                  new Book
                  {
                      //Id = 3,
                      Title = "Dune",
                      GenreId = 2,//Since Fiction 
                      AuthorId = 3,
                      PageCount = 540,
                      PublishDate = new DateTime(2002, 12, 21)
                  });

                context.Users.AddRange(
                  new User
                  {
                      FirstName = "serkan",
                      LastName = "ozel",
                      Email = "serkanozel@gmail.com",
                      Password = "Serkanozelsifre!_"
                  },
                   new User
                   {
                       FirstName = "berkan",
                       LastName = "ozel",
                       Email = "berkannozel@gmail.com",
                       Password = "Berkanozelsifre!_"
                   },
                  new User
                  {
                      FirstName = "gulsen",
                      LastName = "ozel",
                      Email = "gulsenozel@gmail.com",
                      Password = "Gulsenozelsifre!_"
                  },
                  new User
                  {
                      FirstName = "ibrahim",
                      LastName = "ozel",
                      Email = "ibrahimozel@gmail.com",
                      Password = "HalilIbrahimozelsifre!_"
                  });

                context.SaveChanges();
            }
        }
    }
}
