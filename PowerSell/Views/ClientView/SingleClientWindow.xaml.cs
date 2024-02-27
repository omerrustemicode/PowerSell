using PowerSell.Localization;
using PowerSell.Models;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell.Views.ClientView
{
    public partial class SingleClientWindow : Window
    {
        public ObservableCollection<Service> Services { get; set; } = new ObservableCollection<Service>();
        private LocalizationManager _localizationManager;

        public SingleClientWindow(Client client)
        {
            // Initialize the LocalizationManager without specifying a language folder
            _localizationManager = new LocalizationManager();
            InitializeComponent();

            // Set the Services property based on the client's services
            Services = new ObservableCollection<Service>(client.Services.Select(clientService => new Service
            {
                Name = clientService.ServiceName,
                Price = clientService.ServicePrice,
                Quantity = 1,
                TotalPrice = clientService.ServicePrice,
                DateOrderPlaced = clientService.ServiceDate,
                Worker = string.Join(", ", clientService.Workers.Select(worker => worker.UserName))
            }));

            serviceDataGrid.ItemsSource = Services;
        }

        // Event handler for button clicks
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Your existing button click logic
        }

        private void Transport_Btn(object sender, RoutedEventArgs e)
        {
            // Open the KeyboardWindow
            var keyboardWindow = new KeyboardWindow();
            keyboardWindow.ShowDialog();
        }
    }
}
