using AutoMapper;
using BookStore.DBOperations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Queries.GetUserDetail
{
    public class GetUserDetailQuery
    {
        public int UserId { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        public GetUserDetailQuery(IBookStoreDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public UserDetailViewModel Handle()
        {
            var user = _context.Users.FirstOrDefault(u => u.UserId == UserId);
            if (user is null)
            {
                throw new InvalidOperationException("User bulunamadı");
            }

            UserDetailViewModel userDetailViewModel = _mapper.Map<UserDetailViewModel>(user);
            return userDetailViewModel;
        }
    }

    public class UserDetailViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
