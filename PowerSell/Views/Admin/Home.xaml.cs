using System;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PowerSell.Models;

namespace PowerSell.Views.Admin
{
    public partial class Home : UserControl
    {
        public ObservableCollection<OrderList> TodayOrders { get; set; }
        private readonly PowerSellDbContext _context;

        public Home()
        {
            InitializeComponent();
            // Initialize the DbContext
            _context = new PowerSellDbContext();
            TodayOrders = new ObservableCollection<OrderList>();
            CountOrders();
            CountPaid();
            CountNotPaid();
        }

        private void Home_Loaded(object sender, RoutedEventArgs e)
        {
            CountOrders();
            CountPaid();
            CountNotPaid();

        }
        public void CountOrders()
        {
            // Query and populate TodayOrders collection
            // Ensure _context and query logic is correct
            // Example:
            TodayOrders = new ObservableCollection<OrderList>(_context.OrderList
              .Where(o => DbFunctions.TruncateTime(o.ServiceDateCreated) == DateTime.Today)
              .ToList());

            // Display the count of TodayOrders in a textbox or button
            btnCountOrders.Content = TodayOrders.Count.ToString(); // Assuming btnCountOrders is a Button
                                                                   // OR
            btnCountOrders.Content = $"Денешните Нарачки\n({TodayOrders.Count})"; // Assuming btnCountOrders is a Button
        }

        private void btnCountOrders_Click(object sender, RoutedEventArgs e)
        {
            CountOrders();
        }
        public void CountPaid()
        {
            TodayOrders = new ObservableCollection<OrderList>(_context.OrderList
                   .Where(o => DbFunctions.TruncateTime(o.ClientGetServiceDate) == DateTime.Today &&
                   o.IsPaid == true && o.ClientGetService == true)
                   .ToList());

            // Calculate the sum of Total where IsPaid and ClientGetService are true for today's date
            decimal sum = TodayOrders.Sum(o => o.Total);

            // Display the sum in a textbox or button
            btnCountPaid.Content = sum.ToString(); // Assuming btnSumOrders is a Button
                                                   // OR
            btnCountPaid.Content = $"Денешни Платени Услуги\n({sum}) Ден"; // Assuming btnSumOrders is a Button
        }
        public void CountNotPaid()
        {
            // Query and populate TodayOrders collection
            // Ensure _context and query logic is correct
            TodayOrders = new ObservableCollection<OrderList>(_context.OrderList
                .Where(o => DbFunctions.TruncateTime(o.ClientGetServiceDate) == DateTime.Today &&
                             o.IsPaid == false && o.ClientGetService == true)
                .ToList());

            // Calculate the sum of Total where IsPaid is false and ClientGetService is true for today's date
            decimal sum = TodayOrders.Sum(o => o.Total);

            // Display the sum in a textbox or button
            btnCountNotPaid.Content = sum.ToString(); // Assuming btnCountPaid is a Button
                                                      // OR
            btnCountNotPaid.Content = $"Денешни Неплатени Услуги\n({sum}) Ден"; // Assuming btnCountPaid is a Button
        }
    }
}
