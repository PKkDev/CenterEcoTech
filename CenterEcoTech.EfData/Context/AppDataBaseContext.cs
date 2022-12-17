using CenterEcoTech.EfData.Entities;
using Microsoft.EntityFrameworkCore;

namespace CenterEcoTech.EfData.Context
{
    public class AppDataBaseContext : DbContext
    {
        public DbSet<Cooperative> Cooperative { get; set; }

        public DbSet<Client> Client { get; set; }

        public DbSet<ClientAdress> ClientAdress { get; set; }

        public DbSet<Request> Request { get; set; }

        public DbSet<Measurement> Measurement { get; set; }

        public AppDataBaseContext(DbContextOptions<AppDataBaseContext> options)
           : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { }

        public static async void SeedInitilData(AppDataBaseContext context)
        {
            #region Сooperative

            var newСooperative1 = new Cooperative()
            {
                Adress = "Cooperative1Adress",
                Name = "Cooperative1",
                Phone = "86874587458"
            };
            context.Cooperative.Add(newСooperative1);
            context.SaveChanges();

            var newСooperative2 = new Cooperative()
            {
                Adress = "Cooperative2Adress",
                Name = "Cooperative2",
                Phone = "85632145985"
            };
            context.Cooperative.Add(newСooperative2);
            context.SaveChanges();

            var newСooperative3 = new Cooperative()
            {
                Adress = "Cooperative3Adress",
                Name = "Cooperative3",
                Phone = "87452369856"
            };
            context.Cooperative.Add(newСooperative3);
            context.SaveChanges();

            #endregion Сooperative

            #region client

            var newClient = new Client()
            {
                Email = "Email@mail.ru",
                LastNme = "Вовкиннн",
                FirstName = "Вова",
                MidName = "Вович",
                Phone = "81234567896",
                Adress = new ClientAdress()
                {
                    City = "Самара",
                    Corpus = null,
                    House = "55",
                    Room = "44",
                    Street = "Уличная"
                },
                Cooperative = newСooperative1,
            };

            context.Client.Add(newClient);
            context.SaveChanges();

            #endregion client
        }
    }
}
