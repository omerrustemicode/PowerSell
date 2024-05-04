using System;
using System.Linq;
using System.Windows;
using PowerSell.Models;
using PowerSell.Services;

namespace PowerSell.Views.Reports
{
    public partial class Reports : Window
    {
        private readonly PowerSellDbContext _dbContext;

        public Reports()
        {
            InitializeComponent();
            _dbContext = new PowerSellDbContext();
            LoadOrdersAndCalculateSales();
        }

        private void LoadOrdersAndCalculateSales()
        {
            // Get today's date
            DateTime today = DateTime.Today;

            // Calculate the date range starting 30 days ago
            DateTime startDate = DateTime.Today.AddDays(-30); // Start of the day 30 days ago
            DateTime endDate = DateTime.Today; // End of today (current date)

            // Adjust the time component to the end of the day for endDate
            endDate = endDate.Date.AddDays(1).AddTicks(-1);

            // Load orders where IsPaid is true and ClientGetServiceDate is within today's date range
            var ordersToday = _dbContext.OrderList
                .Where(o => o.IsPaid == true && o.ClientGetServiceDate != null &&
                            o.ClientGetServiceDate >= startDate && o.ClientGetServiceDate <= endDate && o.IsClosedCase == false)
                .ToList();

            // Display orders in the DataGrid
            OrdersDataGrid.ItemsSource = ordersToday;

            // Calculate total sales for today
            decimal totalSales = ordersToday.Sum(o => o.Total);

            // Display total sales in the label
            TotalSalesLabel.Content = $"Тотал: {totalSales:C}";
        }

        private async void CalculateSalesButton_Click(object sender, RoutedEventArgs e)
        {
            LoadOrdersAndCalculateSales();
        }

        private void CloseCaseButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if there are items in the OrdersDataGrid
            if (OrdersDataGrid.Items.Count > 0)
            {
                // Calculate total paid based on all orders in the DataGrid
                decimal totalPaid = 0;
                foreach (var item in OrdersDataGrid.Items)
                {
                    if (item is OrderList order)
                    {
                        totalPaid += order.Total;

                        // Update IsClosedCase to true for each order being closed
                        order.IsClosedCase = true;
                    }
                }

                // Create a new DailyClosingCase instance
                var dailyClosingCase = new DailyClosingCase
                {
                    OrderListId = 0, // Set to 0 or any default value since we are adding all orders under one OrderListId
                    Date = DateTime.Now,
                    Workers = SessionManager.Instance.UserName, // Replace "Worker Name" with the actual worker's name
                    TotalPaid = totalPaid
                };

                // Add the dailyClosingCase to the DbContext and save changes
                _dbContext.DailyClosingCase.Add(dailyClosingCase);
                _dbContext.SaveChanges();

                // Optionally, display a message or perform any other actions after closing the case
                MessageBox.Show("Case closed successfully.");
            }
            else
            {
                MessageBox.Show("No orders to close the case.");
            }
        }



    }
}
