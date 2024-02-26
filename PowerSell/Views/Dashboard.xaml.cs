using PowerSell.Models;
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

            // Adding a test client to the Clients collection
            var testClient = Client.GetTestClient();
            Clients.Add(testClient);

            // Set the DataContext to the Clients collection
            DataContext = this;
        }


        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle button click if needed
        }

        private void ToGoButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle To Go button click if needed
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Reports button click if needed
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Logout button click
            Close();
        }
    }
}
