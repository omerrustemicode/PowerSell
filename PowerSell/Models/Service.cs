using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Windows.Controls;

namespace PowerSell.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public decimal Quantity { get; set; }

        public double ServicePrice { get; set; }

        public DateTime ServiceDateCreated { get; set; } = DateTime.UtcNow; // Set a default value or initialize in the constructor

        // Navigation properties
        public ICollection<Orders> Orders { get; set; } // Assuming one-to-many relationship
        public ICollection<ServiceCategory> ServiceCategories { get; set; }
    }
}
