using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace PowerSell.Models
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public DateTime ClientRegDate { get; set; }
        public string DisplayName => $"{ClientName} - {ClientPhone}";
        public ObservableCollection<Orders> Services { get; set; } = new ObservableCollection<Orders>();
    }
}
