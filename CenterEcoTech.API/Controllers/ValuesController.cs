using CenterEcoTech.EfData.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CenterEcoTech.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly AppDataBaseContext _context;

        public ValuesController(AppDataBaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public Task Do()
        {
            return Task.CompletedTask;
        }
    }
}
