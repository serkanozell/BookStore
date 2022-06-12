using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.TestSetup
{
    public static class Authors
    {
        public static void AddAuthors(this BookStoreDbContext context)
        {
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
                    },
                    new Author
                    {
                        FirstName = "denemeyazari",
                        LastName = "denemeyazari",
                        BirthDate = new DateTime(1950, 09, 09)
                    }
                    );
        }
    }
}
