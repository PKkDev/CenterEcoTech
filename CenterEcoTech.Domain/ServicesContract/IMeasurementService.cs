using CenterEcoTech.Domain.DTO.MeasurementRequest;
using CenterEcoTech.Domain.Query.MeasurementRequest;

namespace CenterEcoTech.Domain.ServicesContract
{
    public interface IMeasurementService
    {

        /// <summary>
        /// get mesuarement history
        /// </summary>
        /// <param name="query"></param>
        /// <param name="userId"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<MeasurementRequestDto>> GetHistoryMeasurement(GetMeasurementHistoryQuery query, int userId, CancellationToken ct);
    }
}
