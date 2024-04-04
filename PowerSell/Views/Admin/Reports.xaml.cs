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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PowerSell.Views.Admin
{
    /// <summary>
    /// Interaction logic for Raports.xaml
    /// </summary>
    public partial class Reports : UserControl
    {
        private PowerSellDbContext _context;

        public Reports()
        {
            InitializeComponent();
            _context = new PowerSellDbContext(); // Initialize your DbContext
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            DateTime startDate = dpStartDate.SelectedDate ?? DateTime.MinValue;
            DateTime endDate = dpEndDate.SelectedDate ?? DateTime.MaxValue;

            var orders = _context.OrdersConfirmed
                .Where(o => o.ServiceDateCreated >= startDate && o.ServiceDateCreated <= endDate)
            .ToList();

            lvOrders.ItemsSource = orders;

            // Calculate total
            decimal total = orders.Sum(o => o.Quantity * o.ServicePrice);
            txtTotal.Text = total.ToString();

            // Calculate best-selling product (example logic)
            var bestSellingProduct = orders.GroupBy(o => o.ServiceId)
                .OrderByDescending(g => g.Sum(o => o.Quantity))
                .FirstOrDefault();

            if (bestSellingProduct != null)
            {
                int bestProductId = bestSellingProduct.Key ?? 0; // Assuming ServiceId is int
                string productName = _context.Service.FirstOrDefault(s => s.ServiceId == bestProductId)?.ServiceName;

                if (!string.IsNullOrEmpty(productName))
                {
                    txtBestSellingProduct.Text = $"{productName} ({bestSellingProduct.Sum(o => o.Quantity)} units)";
                }
            }
            else
            {
                txtBestSellingProduct.Text = "No data";
            }
        }
    }
}
