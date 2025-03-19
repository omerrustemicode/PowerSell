using PowerSell.Models;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;

namespace PowerSell.Views.Admin
{
    public partial class Reports : UserControl
    {
        private PowerSellDbContext _context;

        public Reports()
        {
            InitializeComponent();
            _context = new PowerSellDbContext();

            LoadCategories();
            LoadProducts();
        }

        private void LoadCategories()
        {
            var categories = _context.ServiceCategory.ToList();
            cbCategory.ItemsSource = categories;
            cbCategory.DisplayMemberPath = "CategoryName";
            cbCategory.SelectedValuePath = "CategoryId";
        }

        private void LoadProducts()
        {
            var products = _context.Service.ToList();
            cbProduct.ItemsSource = products;
            cbProduct.DisplayMemberPath = "ServiceName";
            cbProduct.SelectedValuePath = "ServiceId";
        }

        private void btnGenerateReport_Click(object sender, RoutedEventArgs e)
        {
            if (dpStartDate.SelectedDate.HasValue && dpEndDate.SelectedDate.HasValue)
            {
                DateTime startDate = dpStartDate.SelectedDate.Value.Date;
                DateTime endDate = dpEndDate.SelectedDate.Value.Date.AddDays(1).AddSeconds(-1);

                var query = _context.OrdersConfirmed.AsQueryable();
                query = query.Where(o => o.ClientGetServiceDate >= startDate && o.ClientGetServiceDate <= endDate);

                if (cbCategory.SelectedItem != null)
                {
                    int selectedCategoryId = (int)cbCategory.SelectedValue;
                    query = query.Where(o => o.Service.CategoryId == selectedCategoryId);
                }

                if (cbProduct.SelectedItem != null)
                {
                    int selectedProductId = (int)cbProduct.SelectedValue;
                    query = query.Where(o => o.ServiceId == selectedProductId);
                }

                var orders = query.ToList();
                var ordersWithService = orders.Select(o => new
                {
                    o.OrdersId,
                    o.Quantity,
                    o.ServicePrice,
                    ServiceName = o.Service?.ServiceName ?? "N/A"
                }).ToList();

                lvOrders.ItemsSource = ordersWithService;
                txtTotal.Text = orders.Sum(o => o.Quantity * o.ServicePrice).ToString("C");

                var bestSellingProduct = orders.GroupBy(o => o.ServiceId)
                    .OrderByDescending(g => g.Sum(o => o.Quantity))
                    .FirstOrDefault();

                txtBestSellingProduct.Text = bestSellingProduct != null
                    ? $"{_context.Service.FirstOrDefault(s => s.ServiceId == bestSellingProduct.Key)?.ServiceName} ({bestSellingProduct.Sum(o => o.Quantity)} units)"
                    : "No data";
            }
            else
            {
                MessageBox.Show("Please select valid start and end dates.");
            }
        }

        private void btnExportPDF_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            FlowDocument doc = new FlowDocument();
            doc.Blocks.Add(new Paragraph(new Run("Sales Report")));
            doc.Blocks.Add(new Paragraph(new Run($"Date Range: {dpStartDate.SelectedDate?.ToShortDateString()} - {dpEndDate.SelectedDate?.ToShortDateString()}")));

            foreach (var item in lvOrders.Items)
            {
                dynamic order = item;
                doc.Blocks.Add(new Paragraph(new Run($"{order.OrdersId}: {order.ServiceName} - {order.Quantity} units, {order.ServicePrice:C}")));
            }

            doc.Blocks.Add(new Paragraph(new Run($"Total: {txtTotal.Text}")));
            doc.Blocks.Add(new Paragraph(new Run($"Best-Selling Product: {txtBestSellingProduct.Text}")));

            IDocumentPaginatorSource idpSource = doc;
            printDialog.PrintDocument(idpSource.DocumentPaginator, "Sales Report");
        }

        private void btnPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(lvOrders, "Printing Orders Report");
            }
        }
    }
}
