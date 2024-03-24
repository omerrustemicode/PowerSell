
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models
{
    public class PowerSellDbContext : DbContext
    {
        public PowerSellDbContext() : base("name=PowerSellDB")
        {
        }

        public DbSet<Tables> Tables { get; set; }
        public DbSet<Orders> Orders { get; set; }
 

        public DbSet<ServiceCategory> ServiceCategory { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OrdersConfirmed> OrdersConfirmed { get; set; }
        public DbSet<Client> Client { get; set; }
        public DbSet<Service> Service { get; set; }
                
        // Add other DbSet properties for your entities

        // Override OnModelCreating if you need to configure entity relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Example:
            // modelBuilder.Entity<YourEntity>().HasMany(x => x.YourRelatedEntity).WithRequired(x => x.YourEntity);
         
        }
    }
}
