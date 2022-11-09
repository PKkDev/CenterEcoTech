using CenterEcoTech.EfData.Entities;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.EfData.Context
{
    public class AppDataBaseContext : DbContext
    {
        public DbSet<Сooperative> Сooperative { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<ClientAdress> ClientAdress { get; set; }

        public DbSet<Request> Request { get; set; }

        public DbSet<Measurement> Measurement { get; set; }

        public AppDataBaseContext(DbContextOptions<AppDataBaseContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public static void SeedInitilData(AppDataBaseContext context)
        { }
    }
}
