using CenterEcoTech.Domain.Exeptions;
using Microsoft.AspNetCore.Http;

namespace CenterEcoTech.Infrastructure.Extensions
{
    public static class HttpContextExtension
    {
        public static int GetUserId(this HttpContext httpContext)
        {
            var userIdStr = httpContext.User.Claims.ToList().Find(x => x.Type == "UserIdentity");
            if (userIdStr == null)
                throw new ApiException("Wrong User Claims");
            var userId = Convert.ToInt32(userIdStr.Value);
            return userId;
        }
    }
}
