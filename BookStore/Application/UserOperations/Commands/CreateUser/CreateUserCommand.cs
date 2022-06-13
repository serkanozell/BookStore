using AutoMapper;
using BookStore.DBOperations;
using BookStore.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommand
    {
        public CreateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public CreateUserCommand(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(u => u.Email == Model.Email);
            if (user is not null)
            {
                throw new InvalidOperationException("Kullanıcı sistemde kayıtlı");
            }

            user = _mapper.Map<User>(Model);
            _context.Users.Add(user);
            _context.SaveChanges();
        }


    }

    public class CreateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
