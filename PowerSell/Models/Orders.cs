using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerSell.Models
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrdersId { get; set; }

        public decimal Quantity { get; set; } // Added nullable type indicator (?)

        public decimal ServicePrice { get; set; } // Added nullable type indicator (?)

        public decimal Total { get; set; } // Added nullable type indicator (?)

        public decimal? ServiceDIscount { get; set; } // Added nullable type indicator (?)

        public bool? IsPaid { get; set; } // Added nullable type indicator (?)

        public int? IsReady { get; set; } // Added nullable type indicator (?)

        public bool? ClientGetService { get; set; } // Added nullable type indicator (?)

        public DateTime? ServiceDateCreated { get; set; } // Added nullable type indicator (?)

        public DateTime? ClientGetServiceDate { get; set; } // Added nullable type indicator (?)

        public DateTime? ServiceDateIsReady { get; set; } // Added nullable type indicator (?)

        public int? TableId { get; set; } // Added nullable type indicator (?)
        public virtual Tables Tables { get; set; }
        public int? ServiceId { get; set; } // Added nullable type indicator (?)
        public virtual Service Service { get; set; }
        public int? ClientId { get; set; } // Foreign key for Client table
        public virtual Client Client { get; set; } // Navigation property for Client

        // Collection of users associated with this service
        public virtual ICollection<User> Workers { get; set; } = new List<User>();
    }
    public class OrderDTO
    {
        public int OrdersId { get; set; }
        public DateTime ServiceDateCreated { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal Quantity { get; set; }
        public string ServiceName { get; set; }
        // Add other properties as needed
    }
}
