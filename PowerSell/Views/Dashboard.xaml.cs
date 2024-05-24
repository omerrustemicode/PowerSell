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
using PowerSell.Services;
using MahApps.Metro.Controls;

namespace PowerSell.Views
{
    public partial class Dashboard : MetroWindow
    {
        public ObservableCollection<Tables> Tables { get; set; } = new ObservableCollection<Tables>();
        public ObservableCollection<OrderList> OrderLists { get; set; } = new ObservableCollection<OrderList>();
        private DispatcherTimer timer;
        private readonly DataService _dataService;
        public Dashboard(DataService dataService)
        {
            InitializeComponent();
            this.Loaded += Dashboard_Loaded;
            _dataService = dataService;
            DataContext = this; // Set the DataContext to allow binding
        }
        private void Dashboard_Loaded(object sender, RoutedEventArgs e)
        {
            LoadTablesFromDatabase();
            UpdateButtonColors();
            StartColorUpdateTimer();
            StartTableUpdateTimer();
        }


        public void LoadTablesFromDatabase()
        {
            var tableDetails = _dataService.GetTableOrderDetails();

            Tables.Clear();

            foreach (var detail in tableDetails)
            {

                if (detail.TotalSumForTable > 0)
                {
                    Tables.Add(new Tables
                    {
                        TableId = detail.TableId,
                        TableName = detail.TableName,
                        OrderList = new ObservableCollection<OrderList>
                {
                    new OrderList
                    {
                        Total = detail.TotalSumForTable ?? 0,
                        IsReady = detail.IsReady,
                        ClientName = $"{detail.ClientName}\n{detail.ClientPhone}"
                    }
                }
                    });
                }
                else
                {
                    Tables.Add(new Tables
                    {
                        TableId = detail.TableId,
                        TableName = detail.TableName
                    });
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
            timer.Stop();
        }
        private void StartTableUpdateTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += TableTimer_Tick;
            timer.Start();
        }
        private void TableTimer_Tick(object sender, EventArgs e)
        {
            LoadTablesFromDatabase();
            timer.Stop();
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

                    // bool hasPendingOrders = orderListForTable.Any(ol => ol.IsReady == 0);
                   
                    foreach (var item in tablesListBox.Items)
                    {
                        if (item is Tables tableModel && tableModel.TableId == table.TableId)
                        {
                            var container = tablesListBox.ItemContainerGenerator.ContainerFromItem(tableModel) as ListBoxItem;
                            var button = FindVisualChild<Button>(container);
                           
                            if (button != null)
                            {
                                if (orderListForTable.Any(ol => ol.IsReady == 0 && ol.ClientGetService == null && ol.IsPaid == null))
                                {
                                    button.Background = Brushes.Red; // Set button color to Red if there are pending orders
                                }
                                else if (orderListForTable.Any(ol => ol.IsReady == 1 && ol.ClientGetService == null && ol.IsPaid == null))
                                {
                                    button.Background = Brushes.Green; // Set button color to Green if ready
                                }
                                else if (orderListForTable.Any(ol => ol.IsReady == 0 && ol.ClientGetService == null && ol.IsPaid == true))
                                {
                                    button.Background = CustomColorHelper.CreateRedGreenGradientBrush(); // Set button color to YellowGreen
                                }
                                else if (orderListForTable.Any(ol => ol.IsReady == 1 && ol.ClientGetService == null && ol.IsPaid == true))
                                {
                                    button.Background = CustomColorHelper.CreateYellowGreenGradientBrush(); // Set button color to the middle color between red and green

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
                singleClientWindow.Closed += SingleClientWindow_Closed; // Attach event handler for Closed
            }
        }
        private void SingleClientWindow_Closed(object sender, EventArgs e)
        {
            LoadTablesFromDatabase(); // Reload the data from the database
            UpdateButtonColors();     // Update the button colors
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
        public void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            ReturnProduct returnprod = new ReturnProduct();
            returnprod.ShowDialog();
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
