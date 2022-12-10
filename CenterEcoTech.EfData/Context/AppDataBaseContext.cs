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
                Adress = "Adress1",
                Name = "Name1",
                Phone = "Phone1"
            };
            context.Cooperative.Add(newСooperative1);
            context.SaveChanges();

            var newСooperative2 = new Cooperative()
            {
                Adress = "Adress2",
                Name = "Name2",
                Phone = "Phone2"
            };
            context.Cooperative.Add(newСooperative2);
            context.SaveChanges();

            var newСooperative3 = new Cooperative()
            {
                Adress = "Adress3",
                Name = "Name3",
                Phone = "Phone3"
            };
            context.Cooperative.Add(newСooperative3);
            context.SaveChanges();

            #endregion Сooperative

            #region client

            var newClient = new Client()
            {
                Email = "Email@mail.ru",
                LastNme = "LastNme",
                FirstName = "FirstName",
                MidName = "MidName",
                Phone = "Phone",
                Adress = new ClientAdress()
                {
                    City = "City",
                    Corpus = null,
                    House = "House",
                    Room = "Room",
                    Street = "Street"
                },
                Cooperative = newСooperative1,
            };

            context.Client.Add(newClient);
            context.SaveChanges();

            #endregion client
        }
    }
}
