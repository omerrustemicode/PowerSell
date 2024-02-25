using System.Collections.ObjectModel;
using System.Windows;

namespace PowerSell.Views
{
    public partial class Dashboard : Window
    {
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        public Dashboard()
        {
            InitializeComponent();
            // Initialize your clients and services, and set the DataContext
            DataContext = this;
        }

        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle service button click
        }

        private void ToGoButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle To Go button click
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Reports button click
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Logout button click
            Close();
        }
    }

    public class Client
    {
        public string ClientName { get; set; }
        public ObservableCollection<Service> Services { get; set; } = new ObservableCollection<Service>();
    }

    public class Service
    {
        public string ServiceName { get; set; }
        public string ServiceStatusColor { get; set; }
    }
}
