using PowerSell.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell.Views.Admin
{
    public partial class Tables : UserControl
    {
        private readonly PowerSellDbContext _context;

        public Tables()
        {
            InitializeComponent();
            _context = new PowerSellDbContext(); // Initialize your DbContext
            LoadTables(); // Load tables initially
        }

        // Method for adding a table
        private void btnAddTable_Click(object sender, RoutedEventArgs e)
        {
            string tableName = txtTableName.Text.Trim();

            if (!string.IsNullOrEmpty(tableName))
            {
                // Create a new table object
                var newTable = new PowerSell.Models.Tables
                {
                    TableName = tableName
                };

                // Add the new table to the database and save changes
                _context.Tables.Add(newTable);
                _context.SaveChanges();

                // Reload the tables to reflect the change
                LoadTables();
                txtTableName.Clear(); // Clear the input field
            }
            else
            {
                // Show message if the table name is empty
                MessageBox.Show("Please enter a table name.");
            }
        }

        // Method to load the tables from the database
        private void LoadTables()
        {
            // Set the ItemsSource to the list of tables in the database
            dataGridTables.ItemsSource = _context.Tables.ToList();
        }

        // Method for deleting a table
        private void btnDeleteTable_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridTables.SelectedItem != null && dataGridTables.SelectedItem is PowerSell.Models.Tables selectedTable)
            {
                // Remove the selected table from the database
                _context.Tables.Remove(selectedTable);
                _context.SaveChanges();

                // Reload the tables to reflect the change
                LoadTables();
            }
            else
            {
                // Show message if no table is selected
                MessageBox.Show("Please select a table to delete.");
            }
        }
    }
}
