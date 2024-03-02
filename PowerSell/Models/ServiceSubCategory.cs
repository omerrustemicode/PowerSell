using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerSell.Models
{
    public class ServiceSubCategory
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; }
        public ServiceCategory Category { get; set; }
        public int SubNonCategoryId { get; set; } // here need to have realationship with subcategoryid to get same ID when add a subnoncategory
        public string SubNonCategoryName { get; set; }
    }
}
