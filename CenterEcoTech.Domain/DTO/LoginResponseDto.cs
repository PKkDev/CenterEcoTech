using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Domain.DTO
{
    public class LoginResponseDto
    {
        public string Token { get; set; }

        public LoginResponseDto(string token)
        {
            Token = token;
        }
    }
}
