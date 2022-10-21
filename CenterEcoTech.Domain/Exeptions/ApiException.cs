using System.Net;

namespace CenterEcoTech.Domain.Exeptions
{
    public class ApiException : Exception
    {
        public HttpStatusCode Code { get; }
        public string Error { get; set; }

        public ApiException(string error)
        {
            Code = HttpStatusCode.BadRequest;
            Error = error;
        }

        public ApiException(HttpStatusCode code, string error)
            : this(error)
        {
            Code = code;
        }
    }
}
