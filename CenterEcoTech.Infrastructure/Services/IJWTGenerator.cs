using CenterEcoTech.EfData.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Infrastructure.Services
{
    public interface IJWTGenerator
    {
        public string CreateToken(Client user);
    }
}
