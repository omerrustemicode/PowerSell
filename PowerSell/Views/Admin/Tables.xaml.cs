using PowerSell.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell.Views.Admin
{
    public partial class Tables : UserControl
    {
        private PowerSellDbContext _context;

        public Tables()
        {
            InitializeComponent();
            _context = new PowerSellDbContext(); // Initialize your DbContext
            LoadTables(); // Load tables initially
        }

        private void btnAddTable_Click(object sender, RoutedEventArgs e)
        {
            string tableName = txtTableName.Text.Trim();

            if (!string.IsNullOrEmpty(tableName))
            {
                PowerSell.Models.Tables newTable = new PowerSell.Models.Tables { TableName = tableName };
                _context.Tables.Add(newTable);
                _context.SaveChanges();

                LoadTables(); // Reload tables after adding a new one
            }
            else
            {
                MessageBox.Show("Please enter a table name.");
            }
        }

        private void LoadTables()
        {
            lstTables.ItemsSource = _context.Tables.ToList();
        }

        private void btnDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            if (lstTables.SelectedItem != null && lstTables.SelectedItem is PowerSell.Models.Tables selectedTable)
            {
                _context.Tables.Remove(selectedTable);
                _context.SaveChanges();

                LoadTables(); // Reload tables after deletion
            }
            else
            {
                MessageBox.Show("Please select a table to delete.");
            }
        }
    }
}
