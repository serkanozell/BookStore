using AutoMapper;
using BookStore.DBOperations;
using BookStore.TokenOperations;
using BookStore.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Application.UserOperations.Commands.RefreshToken
{
    public class RefreshTokenCommand
    {
        public string RefreshToken { get; set; }
        private readonly IBookStoreDbContext _context;
        private readonly IConfiguration _configuration;
        public RefreshTokenCommand(IBookStoreDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public Token Handle()
        {
            var user = _context.Users.FirstOrDefault(u => u.RefreshToken == RefreshToken && u.RefreshTokenExpireDate > DateTime.Now);
            if (user is not null)
            {
                TokenHandler tokenHandler = new TokenHandler(_configuration);
                Token token = tokenHandler.CreateAccessToken(user);

                user.RefreshToken = token.RefreshToken;
                user.RefreshTokenExpireDate = token.Expiration.AddMinutes(5);
                _context.SaveChanges();

                return token;
            }
            else
            {
                throw new InvalidOperationException("Valid bir Refresh Token Bulunamadı");
            }
        }
    }
}
