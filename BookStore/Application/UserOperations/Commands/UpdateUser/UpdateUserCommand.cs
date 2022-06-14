using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Commands.UpdateUser
{
    public class UpdateUserCommand
    {
        public int UserId { get; set; }
        public UpdateUserModel Model { get; set; }
        private readonly IBookStoreDbContext _context;

        public UpdateUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == UserId);
            if (user is null)
            {
                throw new InvalidOperationException("Böyle bir user bulunmamaktadır");
            }

            user.FirstName = Model.FirstName != default ? Model.FirstName : user.FirstName;
            user.LastName = Model.LastName != default ? Model.LastName : user.LastName;
            user.Email = Model.Email != default ? Model.Email : user.Email;
            user.Password = Model.Password != default ? Model.Password : user.Password;

            _context.Users.Update(user);
            _context.SaveChanges();
        }
    }

    public class UpdateUserModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
