using PowerSell.Models;
using System;
using System.Linq;
using System.Windows;

namespace PowerSell.Views.ClientView
{
    public partial class ReturnProduct : Window
    {
        private int orderId;

        public ReturnProduct()
        {
            InitializeComponent();
        }

        private void btnSearchOrder_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(txtOrderListId.Text, out int orderListId))
            {
                LoadProducts(orderListId);
            }
            else
            {
                MessageBox.Show("Please enter a valid OrderListId.");
            }
        }

        private void LoadProducts(int orderListId)
        {
            using (var dbContext = new PowerSellDbContext())
            {
                var orderDetails = dbContext.Orders
                    .Where(od => od.OrderListId == orderListId)
                    .Select(od => new
                    {
                        od.Service.ServiceId,
                        od.Service.ServiceName,
                        od.Quantity
                    }).ToList();

                if (orderDetails.Any())
                {
                    dataGridProducts.ItemsSource = orderDetails;
                    orderId = orderListId;
                }
                else
                {
                    MessageBox.Show("No products found for the given OrderListId.");
                }
            }
        }

        private void btnReturnProduct_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridProducts.SelectedItem != null && int.TryParse(txtQuantityToReturn.Text, out int quantityToReturn))
            {
                var selectedProduct = dataGridProducts.SelectedItem as dynamic;

                if (selectedProduct == null)
                {
                    MessageBox.Show("Please select a valid product.");
                    return;
                }

                int? serviceIdNullable = selectedProduct.ServiceId;
                if (!serviceIdNullable.HasValue)
                {
                    MessageBox.Show("Invalid Service ID.");
                    return;
                }

                int serviceId = serviceIdNullable.Value;
                decimal originalQuantity = selectedProduct.Quantity;

                if (quantityToReturn > originalQuantity)
                {
                    MessageBox.Show("Return quantity cannot be greater than the original quantity.");
                    return;
                }

                try
                {
                    using (var dbContext = new PowerSellDbContext())
                    {
                        var service = dbContext.Service.SingleOrDefault(p => p.ServiceId == serviceId);
                        var orderDetail = dbContext.Orders.SingleOrDefault(od => od.OrderListId == orderId && od.ServiceId == serviceId);

                        if (service != null && orderDetail != null)
                        {
                            // Update product quantity in stock
                            service.Quantity += quantityToReturn;

                            // Update order detail quantity
                            orderDetail.Quantity -= quantityToReturn;

                            if (orderDetail.Quantity == 0)
                            {
                                dbContext.Orders.Remove(orderDetail);
                            }

                            dbContext.SaveChanges();
                            MessageBox.Show("Product quantity updated successfully.");
                            LoadProducts(orderId); // Refresh the DataGrid to show the updated quantity
                        }
                        else
                        {
                            MessageBox.Show("Product or order detail not found.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error updating product quantity: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Please select a product and enter a valid quantity.");
            }
        }
    }
}
