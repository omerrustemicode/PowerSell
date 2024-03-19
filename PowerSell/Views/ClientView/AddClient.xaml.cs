using MahApps.Metro.Controls;
using PowerSell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PowerSell.Views.ClientView
{
    /// <summary>
    /// Interaction logic for AddClient.xaml
    /// </summary>
    public partial class AddClient : MetroWindow
    {
        public Client NewClient { get; private set; }
        private SingleClientWindow singleClientWindow;

        public AddClient(SingleClientWindow window)
        {
            InitializeComponent();
            singleClientWindow = window;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Retrieve values from TextBoxes
            string name = NameTextBox.Text;
            string phone = PhoneTextBox.Text;
            string email = EmailTextBox.Text;

            // Validate input (you may want to add more validation logic)
            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(email))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            try
            {
                // Assuming you have a database context (dbContext) to work with
                using (var dbContext = new PowerSellDbContext())
                {
                    // Create a new Client object and add it to the Clients table
                    var newClient = new Client
                    {
                        ClientName = name,
                        ClientPhone = phone,
                        ClientEmail = email,
                        ClientRegDate = DateTime.Now
                        // Add any other properties as needed
                    };

                    dbContext.Client.Add(newClient);
                    dbContext.SaveChanges();

                    // Set the NewClient property to the newly added client
                    NewClient = newClient;

                    // Close the AddClientWindow
                    this.Close();

                    // Get the instance of SingleClientWindow
                    SingleClientWindow singleClientWindow = Application.Current.Windows.OfType<SingleClientWindow>().FirstOrDefault();

                    // Update the labels in SingleClientWindow with the new client information
                    if (singleClientWindow != null)
                    {
                        singleClientWindow.NameLabel.Content = newClient.ClientName;
                        singleClientWindow.PhoneLabel.Content = newClient.ClientPhone;
                        singleClientWindow.EmailLabel.Content = newClient.ClientEmail;
                    }
                }

                MessageBox.Show("Client added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }

}
