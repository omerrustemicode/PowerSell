using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string ServicesNames => string.Join(", ", Services.Select(service => service.ServiceName));

        public static Client GetTestClient()
        {
            return new Client
            {
                ClientId = 1,
                ClientName = "John Doe",
                ClientPhone = "123-456-7890",
                ClientEmail = "john.doe@example.com",
                ClientRegDate = DateTime.Now,
                Services = new ObservableCollection<ClientServices>
                {
                    new ClientServices
                    {
                        ServiceId = 1,
                        ServiceName = "Service A",
                        ServicePrice = 50.0M,
                        ServiceDate = DateTime.Now
                    },
                    new ClientServices
                    {
                        ServiceId = 2,
                        ServiceName = "Service B",
                        ServicePrice = 75.0M,
                        ServiceDate = DateTime.Now
                    },
                    // Add more services as needed
                }
            };
        }

    }
}
