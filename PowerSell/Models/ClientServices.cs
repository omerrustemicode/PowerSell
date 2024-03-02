using System.Collections.Generic;
using System;

namespace PowerSell.Models
{
    public class ClientServices
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Quantity { get; set; }
        public decimal ServicePrice { get; set; }
        public decimal ServiceDIscount { get; set; }
        public bool IsPaid { get; set; }
        public int IsReady { get; set; }
        public bool ClientGetService { get; set; }
        public string ServiceCategory { get; set; }
        public DateTime ServiceDateCreated { get; set; } //Need to add date when add Service for client
        public DateTime ClientGetServiceDate { get; set; } // when clietn get their service need to add datetime when ClientGetService is True
        public DateTime ServiceDateIsReady { get; set; } //Need to set date when IsReady update to 1
        public Tables Tables { get; set; } //when add a client any service need to get and table number from Tables
    // Collection of users associated with this service
    public virtual ICollection<User> Workers { get; set; } = new List<User>(); //Need to Get WorkerName when create a order
    }
}
