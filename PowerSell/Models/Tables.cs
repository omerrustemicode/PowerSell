using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models
{
    public class Tables
    {
        [Key]
        public int TableId { get; set; }
        public string TableName { get; set; }

        // Collection of users associated with this service
        public virtual ICollection<User> Workers { get; set; } = new List<User>(); //Need to Get WorkerName when worker add clientservice



    }
}
