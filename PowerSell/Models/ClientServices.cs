using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models
{
    public class ClientServices
    {
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public double ServicePrice { get; set; }
        public string ServiceCategory { get; set; }
        public DateTime ServiceDate { get; set; }
    }
}
