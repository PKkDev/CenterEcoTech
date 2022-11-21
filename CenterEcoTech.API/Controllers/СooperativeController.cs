using CenterEcoTech.Domain.DTO.Сooperative;
using CenterEcoTech.Domain.ServicesContract;
using Microsoft.AspNetCore.Mvc;

namespace CenterEcoTech.API.Controllers
{
    [Route("api/cooperative")]
    [ApiController]
    public class СooperativeController : ControllerBase
    {
        private readonly ICooperativeService _cooperativeService;

        public СooperativeController(ICooperativeService cooperativeService)
        {
            _cooperativeService = cooperativeService;
        }

        /// <summary>
        /// get all cooperatives
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<СooperativeDto>> GetСooperatives(CancellationToken ct = default)
        {
            return await _cooperativeService.GetСooperativesAsync(ct);
        }
    }
}
