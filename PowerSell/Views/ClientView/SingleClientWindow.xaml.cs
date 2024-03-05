using PowerSell.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System.Windows;
using System.Collections.Generic;

namespace PowerSell.Views.ClientView
{
    public partial class SingleClientWindow : Window
    {
        // Declare dbContext at the class level
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();

        public int TableId { get; private set; }

        public SingleClientWindow(int tableId)
        {
            InitializeComponent();
            TableId = tableId;

            // Check for active orders and populate the DataGrid
            LoadOrdersData();
        }

        private void LoadOrdersData()
        {
            List<OrderDTO> orders = GetActiveOrdersByTableId(TableId);
            dataGridOrders.ItemsSource = orders;
        }


        public class OrderDTO
        {
            public int OrdersId { get; set; }
            public DateTime ServiceDateCreated { get; set; }
            public decimal ServicePrice { get; set; }
            public decimal Quantity { get; set; }
            public string ServiceName { get; set; }
            // Add other properties as needed
        }

        private List<OrderDTO> GetActiveOrdersByTableId(int tableId)
        {
            return dbContext.Orders
                .Include(o => o.Service)  // Include the related Service
                .Where(o => o.TableId == tableId)
                .Select(o => new OrderDTO
                {
                    OrdersId = o.OrdersId,
                    ServiceDateCreated = o.ServiceDateCreated,
                    ServicePrice = o.ServicePrice,
                    Quantity = o.Quantity,
                    ServiceName = o.Service.ServiceName,  // Access ServiceName from related Service
                                                          // Map other properties as needed
                })
                .ToList();
        }



        // Event handler for button clicks
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Your existing button click logic
        }

        private void Transport_Btn(object sender, RoutedEventArgs e)
        {
            // Open the KeyboardWindow
            var keyboardWindow = new KeyboardWindow();
            keyboardWindow.ShowDialog();
        }

        private void dataGridOrders_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {

        }
    }
}
