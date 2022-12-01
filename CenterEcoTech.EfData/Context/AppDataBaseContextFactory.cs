using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CenterEcoTech.EfData.Context
{
    public class AppDataBaseContextFactory : IDesignTimeDbContextFactory<AppDataBaseContext>
    {
        public AppDataBaseContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDataBaseContext>();

             string connectionString = "Server=(localdb)\\mssqllocaldb;Database=CenterEcoTech;Trusted_Connection=True;MultipleActiveResultSets=true;Encrypt=False";
            //string connectionString = "Server=37.140.192.97,1433;Database=u0901873_CenterEcoTech;User Id=u0901873_CenterEcoTech_admin;Password=Oj9_2uy01;Encrypt=False";

            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new AppDataBaseContext(optionsBuilder.Options);
        }
    }
}
