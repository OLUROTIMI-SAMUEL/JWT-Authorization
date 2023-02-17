﻿using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Recruits.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Recruits.API.Repository
{
    public class TokenRepository : ITokenRepository
    {
        Dictionary<string, string> UsersRecords = new Dictionary<string, string>
        {
            {"admin", "admin" },
            {"password", "password" }
        };
        private readonly IConfiguration _configuration;

        //public DbSet<Users> Recruit { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //so now we will make use of constructor dependency injection
        public TokenRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public Tokens Authenticate(Users users)
        {
            if(!UsersRecords.Any(x => x.Key == users.Name && x.Value == users.Password))
            {
                return null;
            }
            //So here we have Authenticated
            //Generate Json Web Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, users.Name)
                }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), 
                        SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new Tokens { Token = tokenHandler.WriteToken(token) };

        }
    }
}
   