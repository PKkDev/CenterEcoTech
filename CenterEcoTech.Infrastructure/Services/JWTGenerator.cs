using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using CenterEcoTech.EfData.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Infrastructure.Services
{
    public class JWTGenerator : IJWTGenerator
    {
        private readonly IConfiguration _configuration;
        private AppDataBaseContext _context;
        public JWTGenerator(IConfiguration configuration, AppDataBaseContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> CreateTokenAsync(int id)
        {
            var key = _configuration["AuthOptions:TokenKey"];
            var issuer = _configuration["AuthOptions:Issuer"];
            var audience = _configuration["AuthOptions:Audience"];
            var lifeTime = Convert.ToInt32(_configuration["AuthOptions:LifeTime"]);

            SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(key));
            var user = await _context.Client
               .FirstOrDefaultAsync(x => x.Id == id);

            var claims = new List<Claim> {
                new Claim("ClientIdentity", id.ToString()),
                new Claim("ClientIdentityPh", user.Phone),
            };

            if (user.FirstName != null)
                claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName));
            if (user.Email != null)
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Audience = audience,
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.Add(TimeSpan.FromMinutes(lifeTime)),
                SigningCredentials = credentials
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
