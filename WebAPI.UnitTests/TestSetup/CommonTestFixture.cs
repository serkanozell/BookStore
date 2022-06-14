using AutoMapper;
using BookStore.Common;
using BookStore.DBOperations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.TestSetup
{
    public class CommonTestFixture
    {
        public BookStoreDbContext Context { get; set; }
        public IMapper Mapper { get; set; }

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<BookStoreDbContext>().UseInMemoryDatabase("BookStoreTestDb").Options;
            Context = new BookStoreDbContext(options);

            Context.Database.EnsureCreated();
            Context.AddAuthors();
            Context.AddBooks();
            Context.AddGenres();
            Context.AddUsers();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(config => config.AddProfile<MappingProfile>()).CreateMapper();
            //Mapper = new MapperConfiguration(config => { config.AddProfile<MappingProfile>(); }).CreateMapper();
        }
    }
}
