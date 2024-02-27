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
        public string ServicesNames => string.Join(", ", Services.Select(service => service.ServiceName));
        public string WorkerNames => string.Join(", ", Services.SelectMany(service => service.Workers.Select(worker => worker.UserName)));

        public static ObservableCollection<Client> GetTestClients()
        {
            return new ObservableCollection<Client>
            {
                new Client
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
                            ServicePrice = 50,
                            ServiceDate = DateTime.Now,
                            Workers = new ObservableCollection<User>
                            {
                                new User { UserId = 1, UserName = "Worker1" }
                            }

                        },
                        new ClientServices
                        {
                            ServiceId = 2,
                            ServiceName = "Service B",
                            ServicePrice = 75,
                            ServiceDate = DateTime.Now,
                               Workers = new ObservableCollection<User>
                            {
                                new User { UserId = 1, UserName = "Worker1" }
                            }
                        },
                        // Add more services as needed
                    }
                },
                new Client
                {
                    ClientId = 2,
                    ClientName = "Stefan William",
                    ClientPhone = "987-654-3210",
                    ClientEmail = "stefan.william@example.com",
                    ClientRegDate = new DateTime(2022, 2, 24, 10, 30, 0),
                    Services = new ObservableCollection<ClientServices>
                    {
                        new ClientServices
                        {
                            ServiceId = 3,
                            ServiceName = "Service C",
                            ServicePrice = 60,
                            ServiceDate = new DateTime(2023, 2, 24, 10, 30, 0)
                        },
                        new ClientServices
                        {
                            ServiceId = 4,
                            ServiceName = "Service D",
                            ServicePrice = 80,
                            ServiceDate = new DateTime(2024, 2, 24, 10, 30, 0)
                        },
                        // Add more services as needed
                    }
                },
                // Add more clients as needed
            };
        }
    }
}
