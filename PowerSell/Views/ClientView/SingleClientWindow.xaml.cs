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
            // Assuming you have a method to retrieve active orders by TableId
            List<Orders> orders = GetActiveOrdersByTableId(TableId);

            // Bind the orders to the DataGrid
            dataGridOrders.ItemsSource = orders;
        }

        // Replace this with your actual method to retrieve orders from the database
        private List<Orders> GetActiveOrdersByTableId(int tableId)
        {
            // Implement your logic to retrieve orders from the database based on the tableId
            // For example:
            // return dbContext.Orders.Where(o => o.TableId == tableId && o.IsPaid == false).ToList();
            //return new List<Orders>(); // Replace this with your actual logic
            return dbContext.Orders.Where(o => o.TableId == tableId).ToList();
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
    }
}
