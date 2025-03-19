using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models
{
    public class ReturnedProducts
    {
        [Key]
        public int RetProdId { get; set; }
        public int OrdersId { get; set; }
        public int OrderListId { get; set; }
        public int Quantity { get; set; }
        public int ServiceId { get; set; }
        public int ServicePrice { get; set; }
        public string ServiceName { get; set; }
        public string WorkerName { get; set; }
        public int WorkerId { get; set; }
        public DateTime ReturnDate { get; set; }
    }
}
