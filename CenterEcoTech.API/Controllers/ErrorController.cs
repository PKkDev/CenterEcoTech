using CenterEcoTech.Domain.DTO;
using CenterEcoTech.Domain.Exeptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CenterEcoTech.API.Controllers
{
    /// <summary>
    /// accept service error on prod
    /// </summary>
    [AllowAnonymous]
    [ApiExplorerSettings(IgnoreApi = true)]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        /// <summary>
        /// generate error response from service
        /// </summary>
        /// <returns></returns>
        [Route("error")]
        public HttpErrorMessage AcceptAPIError()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;

            var code = 500;
            // var message = exception.Message;
            var message = "Exception occurred";

            if (exception is ApiException httpException)
            {
                code = (int)httpException.Code;
                message = httpException.Error;
            }

            Response.StatusCode = code;
            var response = new HttpErrorMessage(message);
            return response;
        }
    }
}
