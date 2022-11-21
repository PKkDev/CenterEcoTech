﻿using CenterEcoTech.EfData.Entities;
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

        public static async void SeedInitilData(AppDataBaseContext context)
        {
            var newСooperative1 = new Сooperative()
            {
                Adress = "Adress1",
                Name = "Name1",
                Phone = "Phone1"
            };
            context.Сooperative.Add(newСooperative1);
            context.SaveChanges();

            var newСooperative2 = new Сooperative()
            {
                Adress = "Adress2",
                Name = "Name2",
                Phone = "Phone2"
            };
            context.Сooperative.Add(newСooperative2);
            context.SaveChanges();

            var newСooperative3 = new Сooperative()
            {
                Adress = "Adress3",
                Name = "Name3",
                Phone = "Phone3"
            };
            context.Сooperative.Add(newСooperative3);
            context.SaveChanges();
        }
    }
}
