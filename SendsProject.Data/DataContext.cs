


using Microsoft.EntityFrameworkCore;
using SendsProject.Core.Models.Classes;

namespace SendsProject.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Package> Packages { get; set; }
        public DbSet<Recipient> Recipients { get; set; }
        public DbSet<DeliveryPerson> DeliveryPeople { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=DeliveryCompany_db");
        }

    }

}

