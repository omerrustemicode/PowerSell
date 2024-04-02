using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PowerSell.Models;

namespace PowerSell.Views.Admin
{
    public partial class Home : UserControl
    {
        public ObservableCollection<OrdersConfirmed> TodayOrders { get; set; }
        private readonly PowerSellDbContext _context;

        public Home()
        {
            InitializeComponent();
            // Initialize the DbContext
            _context = new PowerSellDbContext();
            TodayOrders = new ObservableCollection<OrdersConfirmed>();
            CountORders();
        }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            CountORders();
      
        }
        public void CountORders()
        {
            // Query and populate TodayOrders collection
            // Ensure _context and query logic is correct
            // Example:
            TodayOrders = new ObservableCollection<OrdersConfirmed>(_context.OrdersConfirmed
                .Where(o => o.ServiceDateCreated == DateTime.Today)
                .ToList());

            // Display the count of TodayOrders in a textbox or button
            btnCountOrders.Content = TodayOrders.Count.ToString(); // Assuming txtTodayOrdersCount is a TextBox
                                                                   // OR
            btnCountOrders.Content = $"Денешните Нарацки ({TodayOrders.Count})"; // Assuming btnTodayOrdersCount is a Button
        }

        private void btnCountOrders_Click(object sender, RoutedEventArgs e)
        {
            CountORders();
        }
 
    }
}
