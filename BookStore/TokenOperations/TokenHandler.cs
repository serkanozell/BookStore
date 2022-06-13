using BookStore.Entities;
using BookStore.TokenOperations.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.TokenOperations
{
    public class TokenHandler
    {
        public IConfiguration Configuration { get; set; }
        public TokenHandler(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public Token CreateAccessToken(User user)
        {
            Token tokenModel = new Token();
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Token:SecurityKey"]));

            SigningCredentials credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            tokenModel.Expiration = DateTime.Now.AddMinutes(15);

            //Token configuration larının ayarlanmaları ve appsettingsteki proplar ile eşleştirilmesi
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: Configuration["Token:Issuer"],
                audience: Configuration["Token:Audience"],
                expires: tokenModel.Expiration,
                notBefore: DateTime.Now,
                signingCredentials: credentials
                );

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            //Token in tam olarak yaratıldığı yer
            tokenModel.AccessToken = tokenHandler.WriteToken(securityToken);
            tokenModel.RefreshToken = CreateRefreshToken();

            return tokenModel;

        }

        public string CreateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
