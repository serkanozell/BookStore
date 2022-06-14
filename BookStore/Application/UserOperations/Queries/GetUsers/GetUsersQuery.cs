using AutoMapper;
using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Queries.GetUsers
{
    public class GetUsersQuery
    {
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetUsersQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<UsersViewModel> Handle()
        {
            var userList = _context.Users.OrderBy(u => u.UserId).ToList();
            List<UsersViewModel> usersViewModels = _mapper.Map<List<UsersViewModel>>(userList);
            return usersViewModels;
        }
    }

    public class UsersViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
