using System;

namespace PowerSell.Models
{
    // Example Service class
    public class Service
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateOrderPlaced { get; set; }
        public string Worker { get; set; }
    }

}
