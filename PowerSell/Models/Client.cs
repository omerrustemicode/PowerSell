using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace PowerSell.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientPhone { get; set; }
        public string ClientEmail { get; set; }
        public DateTime ClientRegDate { get; set; }
        public ObservableCollection<ClientServices> Services { get; set; } = new ObservableCollection<ClientServices>();
    }
}
