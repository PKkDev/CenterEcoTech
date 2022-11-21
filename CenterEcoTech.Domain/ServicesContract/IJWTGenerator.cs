namespace CenterEcoTech.Domain.ServicesContract
{
    public interface IJWTGenerator
    {
        public Task<string> CreateTokenAsync(int id, CancellationToken ct);
    }
}
