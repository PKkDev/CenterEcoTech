using CenterEcoTech.Domain.DTO.Сooperative;
using CenterEcoTech.Domain.ServicesContract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace CenterEcoTech.API.Controllers
{
    [Route("api/cooperative")]
    [ApiController]
    public class СooperativeController : ControllerBase
    {
        private readonly ICooperativeService _cooperativeService;
        private readonly IMemoryCache _memoryCache;

        public СooperativeController(
            ICooperativeService cooperativeService, IMemoryCache memoryCache)
        {
            _cooperativeService = cooperativeService;
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// get all cooperatives
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<СooperativeDto>> GetСooperatives(CancellationToken ct = default)
        {
            var cachedValue = await _memoryCache.GetOrCreate(
                "GetСooperatives",
                async cacheEntry =>
                {
                    cacheEntry.SlidingExpiration = TimeSpan.FromSeconds(3);
                    cacheEntry.AbsoluteExpirationRelativeToNow = TimeSpan.FromSeconds(20);
                    return await _cooperativeService.GetСooperativesAsync(ct);
                });

            return cachedValue;
        }
    }
}
