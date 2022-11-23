using CenterEcoTech.Domain.DTO.Сooperative;
using CenterEcoTech.Domain.ServicesContract;
using CenterEcoTech.EfData.Context;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.Infrastructure.Services
{
    public class CooperativeService : ICooperativeService
    {
        private readonly AppDataBaseContext _context;

        public CooperativeService(AppDataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<СooperativeDto>> GetСooperativesAsync(CancellationToken ct)
        {
            return await _context.Cooperative
                .Select(x => new СooperativeDto(x.Id, x.Name))
                .ToListAsync(ct);
        }
    }
}
