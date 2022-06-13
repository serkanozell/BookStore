using AutoMapper;
using BookStore.DBOperations;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommand
    {
        public CreateTokenModel Model { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        public CreateTokenCommand(IBookStoreDbContext context, IMapper mapper, IConfiguration configuration)
        {
            _context = context;
            _mapper = mapper;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == Model.Email && u.Password == Model.Password);
            if (user != null)
            {
                //Token yarat
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);

                _context.SaveChanges();

                return token;

            }
            else
            {
                throw new InvalidOperationException("Kullanıcı adı veya şifreniz hatalı!!");
            }
        }

    }

    public class CreateTokenModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
