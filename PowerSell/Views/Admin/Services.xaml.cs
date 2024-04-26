using PowerSell.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PowerSell.Services;
using System.Collections.Generic;

namespace PowerSell.Views.Admin
{
    public partial class Services : UserControl
    {
        private readonly PowerSellDbContext _dbContext;
        private List<ServiceCategory> _categories;

        public Services()
        {
            InitializeComponent();
            _dbContext = new PowerSellDbContext();
            LoadCategories();
            LoadServices();
        }

        private void LoadCategories()
        {
            _categories = _dbContext.ServiceCategory.ToList();
            cmbCategory.DisplayMemberPath = "CategoryName";
            cmbCategory.SelectedValuePath = "CategoryId";
            cmbCategory.ItemsSource = _categories;
        }
        private void LoadServices()
        {
            var services = _dbContext.Service
                .Join(
                    _dbContext.ServiceCategory, // Inner join with ServiceCategory
                    service => service.CategoryId, // Join on CategoryId in Service
                    category => category.CategoryId, // Join on CategoryId in ServiceCategory
                    (service, category) => new // Selecting data from both tables
                    {
                        ServiceId = service.ServiceId,
                        ServiceName = service.ServiceName,
                        Quantity = service.Quantity,
                        ServicePrice = service.ServicePrice,
                        CategoryName = category.CategoryName
                    })
                .ToList();

            dataGridServices.ItemsSource = services;
        }



        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            string serviceName = txtServiceName.Text.Trim();
            if (string.IsNullOrEmpty(serviceName))
            {
                MessageBox.Show("Service Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!(cmbCategory.SelectedItem is ServiceCategory selectedCategory))
            {
                MessageBox.Show("Please select a Category.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtQuantity.Text, out int quantity))
            {
                MessageBox.Show("Invalid Quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!decimal.TryParse(txtServicePrice.Text, out decimal price))
            {
                MessageBox.Show("Invalid Service Price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Service newService = new Service
            {
                ServiceName = serviceName,
                Quantity = quantity,
                ServicePrice = price,
                ServiceDateCreated = DateTime.UtcNow,
                CategoryId = selectedCategory.CategoryId
            };

            _dbContext.Service.Add(newService);
            _dbContext.SaveChanges();
            RefreshDataGrid();
        }

        private void EditService_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridServices.SelectedItem is Service selectedService)
            {
                bool isNameChanged = !string.IsNullOrEmpty(txtServiceName.Text.Trim()) && txtServiceName.Text.Trim() != "Enter Service Name";
                int quantity = 0;
                decimal price = 0;

                bool isQuantityChanged = !string.IsNullOrEmpty(txtQuantity.Text.Trim()) && int.TryParse(txtQuantity.Text.Trim(), out quantity);
                bool isPriceChanged = !string.IsNullOrEmpty(txtServicePrice.Text.Trim()) && decimal.TryParse(txtServicePrice.Text.Trim(), out price);

                // Check if any of the properties are changed and ServiceName is not the placeholder
                if ((isNameChanged || isQuantityChanged || isPriceChanged) && isNameChanged)
                {
                    if (isNameChanged)
                    {
                        selectedService.ServiceName = txtServiceName.Text.Trim();
                    }

                    if (isQuantityChanged)
                    {
                        selectedService.Quantity = quantity;
                    }

                    if (isPriceChanged)
                    {
                        selectedService.ServicePrice = price;
                    }

                    // Update selectedService in the database
                    _dbContext.SaveChanges();
                    RefreshDataGrid();

                    // Reset the placeholder text if ServiceName was changed
                    if (isNameChanged)
                    {
                        txtServiceName.Text = string.Empty; // Clear the text
                        txtServiceName.Text = "Enter Service Name"; // Set the placeholder text
                    }
                }
            }
        }

        private void DeleteService_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridServices.SelectedItem is Service selectedService)
            {
                _dbContext.Service.Remove(selectedService);
                _dbContext.SaveChanges();
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            LoadServices();
        }

        private void dataGridServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle the selection changed event here
        }

        private void txtServiceName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
