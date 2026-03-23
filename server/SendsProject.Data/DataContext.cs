


using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SendsProject.Core.Models.Classes;
using System.Collections.Generic;
using System.Threading.Channels;

namespace SendsProject.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Package> Packages { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<DeliveryPerson> DeliveryPeople { get; set; }

        public DbSet<User> User { get; set; }
        private readonly IConfiguration _configuration;
        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration["ConnectionStrings"]);
            optionsBuilder.LogTo(massage => Console.WriteLine(massage));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. טיפול בחבילה מול הנמען
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Recipient)
                .WithMany(r => r.Packages)
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.ClientSetNull); // מניעת מחיקה בשרשרת כאן

            // 2. טיפול בחבילה מול השליח
            modelBuilder.Entity<Package>()
                .HasOne(p => p.DeliveryPerson)
                .WithMany(d => d.Packages)
                .HasForeignKey(p => p.DeliveryPersonId)
                .OnDelete(DeleteBehavior.ClientSetNull); // מניעת מחיקה בשרשרת כאן

            // 3. ליתר ביטחון - הקשרים של ה-User
            modelBuilder.Entity<Recipient>()
                .HasOne(r => r.User)
                .WithMany()
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<DeliveryPerson>()
                .HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            base.OnModelCreating(modelBuilder);
        }

    }

}

