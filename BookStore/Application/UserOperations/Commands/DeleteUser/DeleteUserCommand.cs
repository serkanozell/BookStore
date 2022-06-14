using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Commands.DeleteUser
{
    public class DeleteUserCommand
    {
        public int UserId { get; set; }
        private readonly IBookStoreDbContext _context;

        public DeleteUserCommand(IBookStoreDbContext context)
        {
            _context = context;
        }

        public void Handle()
        {
            var user = _context.Users.SingleOrDefault(u => u.UserId == UserId);
            if (user is null)
            {
                throw new InvalidOperationException("Sistemde böyle bir kullanıcı bulunmamaktadır");
            }
            _context.Users.Remove(user);
            _context.SaveChanges();
        }
    }
}
