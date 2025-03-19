using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models.SP
{
    [Table("vw_TableOrderDetails")] // Specify the view name
    public class TableOrderDetailView
    {
       
        public int TableId { get; set; }
        public string TableName { get; set; }
        public decimal? TotalSumForTable { get; set; } = null;
        public int? IsReady { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public double? XPosition { get; set; }
        public double? YPosition { get; set; }
    }

}
