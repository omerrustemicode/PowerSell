using PowerSell.Models;
using PowerSell.Views.ClientView;
using PowerSell.Views.ToGo;
using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace PowerSell.Views
{
    public partial class Dashboard : Window
    {
        public ObservableCollection<Tables> Tables { get; set; } = new ObservableCollection<Tables>();
        private DispatcherTimer timer;

        public Dashboard()
        {
            InitializeComponent();
            LoadTablesFromDatabase();
            StartColorUpdateTimer();
            StartTableUpdateTimer();
            UpdateButtonColors();
            DataContext = this; // Set the DataContext to allow binding
        }
        private void LoadTablesFromDatabase()
{
    using (var dbContext = new PowerSellDbContext())
    {
        var tablesFromDb = dbContext.Tables.OrderByDescending(t => t.TableName).ToList();

        // Clear existing items
        Tables.Clear();

        // Map tables from the database to your view model
        foreach (var table in tablesFromDb)
        {
            var viewModelTable = new Tables { TableId = table.TableId, TableName = table.TableName };
            var ordersForTable = dbContext.Orders.Where(o => o.TableId == table.TableId).ToList();

            // Calculate the total sum for the table outside of the inner loop
            decimal totalSumForTable = ordersForTable.Sum(ol => ol.Total);

            // Check if there are orders for the table
            if (ordersForTable.Any())
            {
                // Collect OrderListId values for all orders related to the table
                var orderListIds = ordersForTable.Select(o => o.OrderListId).ToList();

                // Check if any order in the table is ready (assuming IsReady is an integer representing readiness)
                bool anyOrderReady = orderListIds.Any(orderListId =>
                    dbContext.OrderList.Any(ol => ol.OrderListId == orderListId && ol.IsReady != null && ol.IsReady != 0));

                // Assign 1 if any order is ready, else assign 0
                int? isReadyValue = anyOrderReady ? 1 : 0;

                // Add a single row representing the total sum to viewModelTable.OrderList
                viewModelTable.OrderList.Add(new OrderList
                {
                    Total = totalSumForTable,
                    IsReady = isReadyValue
                    // Add any other properties you need from the OrderList table
                });
            }
            else
            {
                // Handle case where there are no orders for the table
                // You can add default or placeholder data if needed
            }

            Tables.Add(viewModelTable);
            UpdateButtonColors();
        }
    }
}







        private void StartColorUpdateTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += ColorTimer_Tick;
            timer.Start();
        }
        private void ColorTimer_Tick(object sender, EventArgs e)
        {
            UpdateButtonColors();

        }
        private void StartTableUpdateTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);
            timer.Tick += TableTimer_Tick;
            timer.Start();
        }
        private void TableTimer_Tick(object sender, EventArgs e)
        {

            LoadTablesFromDatabase();
        }


        private void UpdateButtonColors()
        {
            using (var dbContext = new PowerSellDbContext())
            {
                foreach (var table in Tables)
                {
                    var ordersForTable = dbContext.Orders.Where(o => o.TableId == table.TableId).ToList();

                    // Get OrderListIds for the orders of the current table
                    var orderListIds = ordersForTable.Select(o => o.OrderListId).ToList();

                    // Fetch OrderList items based on OrderListIds
                    var orderListForTable = dbContext.OrderList
                        .Where(ol => orderListIds.Contains(ol.OrderListId))
                        .ToList();

                    bool hasPendingOrders = orderListForTable.Any(ol => ol.IsReady == 0);

                    foreach (var item in tablesListBox.Items)
                    {
                        if (item is Tables tableModel && tableModel.TableId == table.TableId)
                        {
                            var container = tablesListBox.ItemContainerGenerator.ContainerFromItem(tableModel) as ListBoxItem;
                            var button = FindVisualChild<Button>(container);

                            if (button != null)
                            {
                                if (hasPendingOrders)
                                {
                                    button.Background = Brushes.Red; // Set button color to Red
                                }
                                else if (orderListForTable.Any(ol => ol.IsReady == 1 && ol.ClientGetService == true && ol.IsPaid == null))
                                {
                                    button.Background = Brushes.YellowGreen; // Set button color to Yellow
                                }
                                else if (orderListForTable.Any(ol => ol.IsReady == 1))
                                {
                                    button.Background = Brushes.Green; // Set button color to Green
                                }
                            }

                            break; // Exit the loop once the button is found
                        }
                    }
                }
            }
        }


        private void TableButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button && button.DataContext is Tables tableModel)
            {
                // Navigate to SingleClientWindow with TableId parameter
                SingleClientWindow singleClientWindow = new SingleClientWindow(tableModel.TableId);
                singleClientWindow.Show();
            }
        }

        private void ToGoButton_Click(object sender, RoutedEventArgs e)
        {
            var ToGOWIndow = new ToGoWindow();
            ToGOWIndow.ShowDialog();
        }

        private void ReportsButton_Click(object sender, RoutedEventArgs e)
        {
            Reports.Reports reportsDialog = new Reports.Reports();
            reportsDialog.ShowDialog(); // Show the ReportsDialog as a dialog
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private T FindVisualChild<T>(DependencyObject obj) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }

                T childItem = FindVisualChild<T>(child);
                if (childItem != null)
                {
                    return childItem;
                }
            }
            return null;
        }
        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower(); // Convert search text to lowercase for case-insensitive search
            var filteredTables = Tables.Where(table => table.TableName.ToLower().Contains(searchText)).ToList();
            tablesListBox.ItemsSource = filteredTables;
        }

    }
}
