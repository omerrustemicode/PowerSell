using PowerSell.Models;
using PowerSell.Views.ClientView;
using System;
using System.Collections.ObjectModel;
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

        public Dashboard(int userId)
        {
            InitializeComponent();
            LoadTablesFromDatabase();
            StartColorUpdateTimer();
            DataContext = this; // Set the DataContext to allow binding
        }

        private void LoadTablesFromDatabase()
        {
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
                            ServicePrice = order.ServicePrice,
                            IsReady = order.IsReady // Add IsReady property
                            // Add any other properties you need from the Order table
                        });
                    }

                    Tables.Add(viewModelTable);
                }
            }
        }

        private void StartColorUpdateTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateButtonColors();
        }

        private void UpdateButtonColors()
        {
            using (var dbContext = new PowerSellDbContext())
            {
                foreach (var table in Tables)
                {
                    var ordersForTable = dbContext.Orders.Where(o => o.TableId == table.TableId).ToList();

                    bool hasPendingOrders = ordersForTable.Any(order => order.IsReady == 0);

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
                                else if(hasPendingOrders = ordersForTable.Any(order => order.IsReady == 1))
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
    }
}
