﻿
using PowerSell.Models.SP;
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
        public DbSet<OrderList> OrderList { get; set; }
        public DbSet<DailyClosingCase> DailyClosingCase { get; set; }
        public DbSet<ReturnedProducts> ReturnedProducts { get; set; }
       // public virtual DbSet<TableOrderDetailView> TableOrderDetailsView { get; set; }

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
        
        }
    }
}
