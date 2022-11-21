using CenterEcoTech.Domain.DTO.Сooperative;

namespace CenterEcoTech.Domain.ServicesContract
{
    public interface ICooperativeService
    {
        public Task<IEnumerable<СooperativeDto>> GetСooperativesAsync(CancellationToken ct);
    }
}
