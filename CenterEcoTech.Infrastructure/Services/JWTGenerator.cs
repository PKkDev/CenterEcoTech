using CenterEcoTech.EfData.Entities;
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

        public JWTGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string CreateToken(Client client)
        {
            var key = _configuration["AuthOptions:TokenKey"];
            var issuer = _configuration["AuthOptions:Issuer"];
            var audience = _configuration["AuthOptions:Audience"];
            var lifeTime = Convert.ToInt32(_configuration["AuthOptions:LifeTime"]);

            SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(key));

            var claims = new List<Claim> {
                new Claim("ClientIdentity", client.Id.ToString()),
                new Claim("ClientIdentityPh", client.Phone),
            };

            if (client.Id != 0)
            {
                if (client.FirstName != null)
                    claims.Add(new Claim(JwtRegisteredClaimNames.GivenName, client.FirstName));
                if (client.Email != null)
                    claims.Add(new Claim(JwtRegisteredClaimNames.Email, client.Email));
            }

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
