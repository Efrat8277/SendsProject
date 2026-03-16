


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
            base.OnModelCreating(modelBuilder);

            // קשר בין User ל-DeliveryPerson
            modelBuilder.Entity<DeliveryPerson>()
                .HasOne(dp => dp.User)
                .WithMany() // אין צורך בקולקשן מצד ה-User, רק User אחד ל-DeliveryPerson
                .HasForeignKey(dp => dp.UserId)
                .OnDelete(DeleteBehavior.SetNull);  // במקום Cascade, נשאיר את השדה כ-NULL במקרה של מחיקה

            // קשר בין User ל-Recipient
            modelBuilder.Entity<Recipient>()
                .HasOne(r => r.User)
                .WithMany() // אין צורך בקולקשן מצד ה-User, רק User אחד ל-Recipient
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.SetNull);  // נשאיר את השדה כ-NULL במקרה של מחיקה

            // קשר בין Package ל-DeliveryPerson
            modelBuilder.Entity<Package>()
                .HasOne(p => p.DeliveryPerson)
                .WithMany(dp => dp.Packages) // DeliveryPerson מקבל הרבה חבילות
                .HasForeignKey(p => p.DeliveryPersonId)
                .OnDelete(DeleteBehavior.SetNull);  // נשאיר את השדה כ-NULL במקרה של מחיקה

            // קשר בין Package ל-Recipient
            modelBuilder.Entity<Package>()
                .HasOne(p => p.Recipient)
                .WithMany(r => r.Packages) // Recipient מקבל הרבה חבילות
                .HasForeignKey(p => p.RecipientId)
                .OnDelete(DeleteBehavior.SetNull);  // נשאיר את השדה כ-NULL במקרה של מחיקה
        }

    }

}

