using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models
{
    public class Service
    {
        [Key]
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public decimal Quantity { get; set; }
        public double ServicePrice { get; set; }
        public ServiceCategory ServiceCategory { get; set; }
        public ServiceSubCategory ServiceSubCategory { get; set; }
        public DateTime ServiceDateCreated { get; set; }
    }
}