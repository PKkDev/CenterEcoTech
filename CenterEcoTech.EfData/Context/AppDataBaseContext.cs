using CenterEcoTech.EfData.Entities;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.EfData.Context
{
    public class AppDataBaseContext : DbContext
    {
        public DbSet<Test> Test { get; set; }

        public AppDataBaseContext(DbContextOptions<AppDataBaseContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        public static void SeedInitilData(AppDataBaseContext context)
        { }
    }
}
