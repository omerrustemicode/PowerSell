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
        public SingleClientWindow SingleClientWindowReference { get; set; }

        // Other code...

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Access SingleClientWindowReference and do something with the data
            // For example: SingleClientWindowReference.UpdateClientData(clientData);

            // Close the AddClient window
            this.Close();
        }
    }

}
