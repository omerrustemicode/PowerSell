using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PowerSell.Models
{
    [Table("ServiceCategories", Schema = "dbo")]
    public class ServiceCategory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }

        [ForeignKey("ParentCategory")]
        public int? CategoryParentId { get; set; }

        public virtual ServiceCategory ParentCategory { get; set; }

        [ForeignKey("Service")]
        public int? ServiceId { get; set; }
        public virtual Service Service { get; set; }
    }
}
