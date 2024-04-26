using System;
using System.ComponentModel.DataAnnotations;

namespace PowerSell.Models
{
    public class DailyClosingCase
    {
        [Key]
        public int OrderListId { get; set; }
        public DateTime Date { get; set; }
        public string Workers { get; set; }
        public decimal TotalPaid { get; set; }
    }
}
