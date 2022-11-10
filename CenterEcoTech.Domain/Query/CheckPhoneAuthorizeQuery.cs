using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CenterEcoTech.Domain.Query
{
    public class CheckPhoneAuthorizeQuery : PhoneAuthorizeQuery
    {
        public string Code { get; set; }
    }
}
