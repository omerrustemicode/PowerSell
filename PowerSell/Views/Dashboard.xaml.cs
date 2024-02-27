using PowerSell.Models;
using PowerSell.Views.ClientView;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell.Views
{
    public partial class Dashboard : Window
    {
        public ObservableCollection<Client> Clients { get; set; } = new ObservableCollection<Client>();

        public Dashboard()
        {
            InitializeComponent();

            // Adding a test client to the Clients collection
            var testClients = Client.GetTestClients();
            Clients = testClients;

            // Set the DataContext to the Clients collection
            DataContext = this;
        }

        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            // Ensure the DataContext is set and the sender is a Button
            if (DataContext is Dashboard dashboard && sender is Button button)
            {
                // Get the client associated with the clicked button
                var selectedClient = dashboard.Clients.FirstOrDefault(client => client.ClientName == button.Content.ToString());

                if (selectedClient != null)
                {
                    // Open the SingleClientWindow and pass the selected client
                    SingleClientWindow singleClientWindow = new SingleClientWindow(selectedClient);
                    singleClientWindow.Show();  // or .ShowDialog() if you want it to be modal
                }
            }
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
