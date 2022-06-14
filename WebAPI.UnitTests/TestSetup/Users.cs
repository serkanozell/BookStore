using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.UnitTests.TestSetup
{
    public static class Users
    {
        public static void AddUsers(this BookStoreDbContext context)
        {
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
        }
    }
}
