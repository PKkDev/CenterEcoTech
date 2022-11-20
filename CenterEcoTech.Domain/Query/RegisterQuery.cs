using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Domain.Query
{
    public class RegisterQuery
    {
        public int Id { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastNme { get; set; }

        public string MidName { get; set; }
        public ClientAdressQuery Adress { get; set; }

        public int СooperativeId { get; set; }
        

    }
    public class ClientAdressQuery
    {
        public int Id { get; set; }

        public string City { get; set; }

        public string Street { get; set; }

        public string House { get; set; }

        public string? Corpus { get; set; }

        public string Room { get; set; }

        public int ClientId { get; set; }
    }
}
