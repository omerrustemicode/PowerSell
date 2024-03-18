using PowerSell.Models;
using PowerSell.Views.ClientView;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell.Views
{
    // Dashboard.xaml.cs
    public partial class Dashboard : Window
    {
        public ObservableCollection<Tables> Tables { get; set; } = new ObservableCollection<Tables>();

        public Dashboard(int userId)
        {
            InitializeComponent();
            using (var dbContext = new PowerSellDbContext())
            {
                var tablesFromDb = dbContext.Tables.ToList();

                // Clear existing items
                Tables.Clear();

                // Map tables from the database to your view model
                foreach (var table in tablesFromDb)
                {
                    var viewModelTable = new Tables { TableId = table.TableId, TableName = table.TableName };

                    // Fetch orders for the current table from the Orders table
                    var ordersForTable = dbContext.Orders.Where(o => o.TableId == table.TableId).ToList();

                    // Add order information to the view model table
                    foreach (var order in ordersForTable)
                    {
                        viewModelTable.Orders.Add(new Orders
                        {
                            ServicePrice = order.ServicePrice
                            // Add any other properties you need from the Order table
                        });
                    }

                    Tables.Add(viewModelTable);
                }
            }


            DataContext = this; // Set the DataContext to allow binding
        }



        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            // Assuming Tables is a collection of objects with a property TableId
            if (sender is Button button && button.DataContext is Tables tableModel)
            {
                // Navigate to SingleClientWindow with TableId parameter
                SingleClientWindow singleClientWindow = new SingleClientWindow(tableModel.TableId);
                singleClientWindow.Show();
            }
        }



        private void ToGoButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle To Go button click event
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            // Handle Reports button click event
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }

}
