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
    public partial class AddClient : Window
    {
        public AddClient()
        {
            InitializeComponent();
        }
        // Other code...

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
                }

                MessageBox.Show("Client added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                // Close the AddClientWindow
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }

}
