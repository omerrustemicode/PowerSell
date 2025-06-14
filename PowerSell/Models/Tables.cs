﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models
{
    public class Tables
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableId { get; set; }
        public string TableName { get; set; }
        public double? XPosition { get; set; } // Nullable to match database
        public double? YPosition { get; set; } // Nullable to match database
        // Collection of users associated with this service
        public virtual ICollection<User> Workers { get; set; } = new List<User>(); //Need to Get WorkerName when worker add clientservice
        public virtual ICollection<Orders> Orders { get; set; } = new List<Orders>(); //Need to Get WorkerName when worker add clientservice
        public virtual ICollection<OrderList> OrderList { get; set; } = new List<OrderList>(); //Need to Get WorkerName when worker add clientservice



    }
}
