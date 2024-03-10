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

        public decimal Quantity { get; set; }

        public decimal ServicePrice { get; set; }

        public decimal ServiceDIscount { get; set; }

        public bool IsPaid { get; set; }

        public int IsReady { get; set; }

        public bool ClientGetService { get; set; }

        public DateTime ServiceDateCreated { get; set; } //Need to add date when add Service for client

        public DateTime ClientGetServiceDate { get; set; } // when clietn get their service need to add datetime when ClientGetService is True

        public DateTime ServiceDateIsReady { get; set; } //Need to set date when IsReady update to 1

        public int TableId { get; set; }
        public virtual Tables Tables { get; set; }
        public int ServiceId { get; set; }
        public virtual Service Service { get; set; }
        // Collection of users associated with this service
        public virtual ICollection<User> Workers { get; set; } = new List<User>(); //Need to Get WorkerName when create a order
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
