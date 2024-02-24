using PowerSell.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell
{
    public partial class MainWindow : Window
    {
        public ObservableCollection<Service> Services { get; set; } = new ObservableCollection<Service>();

        public MainWindow()
        {
            InitializeComponent();
            serviceDataGrid.ItemsSource = Services;
        }

        // Event handler for button clicks
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            string serviceName = clickedButton.Content.ToString();

            // Check if the service with the same name already exists
            Service existingService = Services.FirstOrDefault(service => service.Name == serviceName);

            if (existingService != null)
            {
                // Update the quantity if the service exists
                existingService.Quantity++;
                existingService.TotalPrice = existingService.Price * existingService.Quantity;

                // Refresh the DataGrid to reflect changes
                serviceDataGrid.Items.Refresh();
            }
            else
            {
                // Add a new service if it doesn't exist
                Services.Add(new Service
                {
                    Name = serviceName,
                    Price = 10,
                    Quantity = 1,
                    TotalPrice = 10,
                    DateOrderPlaced = DateTime.Now,
                    Worker = "Omer"
                });
            }
        }
    }
}
