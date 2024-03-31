using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models
{
    public class OrderList
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrderListId { get; set; }
        public decimal Total { get; set; }
        public string Message { get; set; }
        public string Transport { get; set; }

        public string ClientName { get; set; }
        public int? IsReady { get; set; } // Added nullable type indicator (?)
    }
}
