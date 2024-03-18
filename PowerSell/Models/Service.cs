using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Controls;

namespace PowerSell.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServiceId { get; set; }

        public string ServiceName { get; set; }

        public int Quantity { get; set; }

        public decimal ServicePrice { get; set; }

        public DateTime ServiceDateCreated { get; set; } = DateTime.UtcNow; // Set a default value or initialize in the constructor

        // Foreign key to CategoryId
        public int CategoryId { get; set; }

        // Navigation properties
        public ICollection<Orders> Orders { get; set; } // Assuming one-to-many relationship
        public ICollection<ServiceCategory> ServiceCategories { get; set; }

        // Total property to calculate the total price
        [NotMapped]
        public decimal Total => (decimal)Quantity * ServicePrice;
    }
}
