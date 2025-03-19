using PowerSell.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using PowerSell.Services;
using System.Linq;
using System.Windows.Media;
using MaterialDesignThemes.Wpf;
using MaterialDesignColors;
using Aspose.Slides.Theme;
using System;
using MahApps.Metro.Controls;
using System.Xml;
using System.Drawing.Printing;
using System.Threading.Tasks;
using System.Windows.Data;
using DataGridTextColumn = MaterialDesignThemes.Wpf.DataGridTextColumn;
using System.Data.SqlClient;
using System.Data;
using System.Xml.Serialization;
using System.IO;

namespace PowerSell.Views.ClientView
{
    // PrintSettings for XML-based print formatting
    [XmlRoot("PrintSettings")]
    public class PrintSettings
    {
        [XmlElement("Width")]
        public double Width { get; set; }

        [XmlElement("Height")]
        public double Height { get; set; }

        [XmlElement("Separator")]
        public string Separator { get; set; }

        [XmlElement("ServiceName")]
        public string ServiceName { get; set; }

        [XmlElement("ServicePrice")]
        public string ServicePrice { get; set; }

        [XmlElement("Quantity")]
        public string Quantity { get; set; }

        [XmlElement("Total")]
        public string Total { get; set; }

        [XmlElement("Employee")]
        public string Employee { get; set; }
    }

    public static class PrintSettingsLoader
    {
        public static PrintSettings LoadPrintSettings(string filePath)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PrintSettings));
                using (FileStream fs = new FileStream(filePath, FileMode.Open))
                {
                    return (PrintSettings)serializer.Deserialize(fs);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading print settings from {filePath}: {ex.Message}");
                return new PrintSettings { Width = 72.1, Height = 99999, Separator = "-----------------------------------" };
            }
        }
    }

    // PrinterSettingsModel for printer assignments (renamed to avoid conflict)
    [XmlRoot("PrinterSettings")]
    public class PrinterSettingsModel
    {
        [XmlElement("PrintOrderPrinter")]
        public string PrintOrderPrinter { get; set; }

        [XmlElement("PrintFiscPrinter")]
        public string PrintFiscPrinter { get; set; }
    }

    public static class PrinterSettingsManager
    {
        private static readonly string SettingsPath = "../Services/PrinterSettings.xml";

        public static PrinterSettingsModel LoadSettings()
        {
            try
            {
                if (File.Exists(SettingsPath))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PrinterSettingsModel));
                    using (FileStream fs = new FileStream(SettingsPath, FileMode.Open))
                    {
                        return (PrinterSettingsModel)serializer.Deserialize(fs);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading printer settings: {ex.Message}");
            }
            return new PrinterSettingsModel { PrintOrderPrinter = "", PrintFiscPrinter = "" };
        }

        public static void SaveSettings(PrinterSettingsModel settings)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PrinterSettingsModel));
                using (FileStream fs = new FileStream(SettingsPath, FileMode.Create))
                {
                    serializer.Serialize(fs, settings);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving printer settings: {ex.Message}");
            }
        }
    }

    public partial class SingleClientWindow : MetroWindow
    {
        public ObservableCollection<ServiceCategory> YourServiceCategoriesCollection { get; set; }
        public ICommand YourCommandForButtonClick { get; set; }
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();
        private Stack<int> navigationStack = new Stack<int>();
        public int TableId { get; private set; }
        string userName = SessionManager.Instance.UserName;
        int userid = SessionManager.Instance.UserId;
        private PrinterSettingsModel printerSettings;

        public SingleClientWindow(int tableId)
        {
            InitializeComponent();
            TableId = tableId;
            YourServiceCategoriesCollection = new ObservableCollection<ServiceCategory>();
            LoadCategories();
            LoadOrdersData(dataGridOrders);
            YourCommandForButtonClick = new RelayCommand(ExecuteYourCommandForButtonClick);
            TotalToOrders();
            UpdateTotalOrders();
            printerSettings = PrinterSettingsManager.LoadSettings();
        }

        private void LoadOrdersData(DataGrid dataGrid)
        {
            List<OrderDTO> orders = GetActiveOrdersByTableId(TableId);
            dataGrid.Columns.Clear();
            dataGrid.AutoGenerateColumns = false;

            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Order ID", Binding = new Binding("OrdersId") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Service Name", Binding = new Binding("ServiceName") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Quantity", Binding = new Binding("Quantity") });
            dataGrid.Columns.Add(new DataGridTextColumn { Header = "Service Price", Binding = new Binding("ServicePrice") });

            dataGrid.ItemsSource = orders;
        }

        private List<OrderDTO> GetActiveOrdersByTableId(int tableId)
        {
            using (var dbContext = new PowerSellDbContext())
            {
                return dbContext.Database.SqlQuery<OrderDTO>("EXEC GetActiveOrdersByTableId @TableId", new SqlParameter("@TableId", tableId)).ToList();
            }
        }

        private void LoadCategories()
        {
            ExecuteYourCommandForButtonClick(0);
        }

        private void ExecuteYourCommandForButtonClick(object parameter)
        {
            if (parameter is int categoryId)
            {
                if (categoryId == 0)
                {
                    List<ServiceCategory> categoriesToDisplay = GetAllCategories();
                    DisplayCategories(categoriesToDisplay);
                }
                else
                {
                    List<ServiceCategory> subcategoriesToDisplay = GetSubcategories(categoryId);
                    DisplaySubcategories(subcategoriesToDisplay);
                    List<Service> servicesToDisplay = GetServicesByCategory(categoryId);
                    DisplayServices(servicesToDisplay);
                }
            }
        }

        private List<ServiceCategory> GetSubcategories(int parentCategoryId)
        {
            return dbContext.ServiceCategory.Where(category => category.CategoryParentId == parentCategoryId).ToList();
        }

        private List<Service> GetServicesByCategory(int categoryId)
        {
            return dbContext.Service.Where(s => s.CategoryId == categoryId).ToList();
        }

        private List<ServiceCategory> GetAllCategories()
        {
            return dbContext.ServiceCategory.Where(category => category.CategoryParentId == null).ToList();
        }

        private void DisplayCategories(List<ServiceCategory> categories)
        {
            CategoryServiceWrap.Children.Clear();
            foreach (ServiceCategory category in categories)
            {
                Button categoryButton = CreateCategoryButton(category);
                CategoryServiceWrap.Children.Add(categoryButton);
            }
        }

        private void DisplaySubcategories(List<ServiceCategory> subcategories)
        {
            SubcategoryServiceWrap.Children.Clear();
            foreach (ServiceCategory subcategory in subcategories)
            {
                Button subcategoryButton = CreateSubcategoryButton(subcategory);
                SubcategoryServiceWrap.Children.Add(subcategoryButton);
            }
        }

        private Button CreateCategoryButton(ServiceCategory category)
        {
            Button categoryButton = new Button
            {
                Content = category.CategoryName,
                Width = 200,
                Height = 90,
                FontSize = 20,
                Margin = new Thickness(5),
                Tag = category.CategoryId,
                Background = (Brush)new BrushConverter().ConvertFrom("#125535"),
                Foreground = Brushes.White,
                Style = (Style)App.Current.Resources["MaterialDesignFlatButton"]
            };
            categoryButton.Click += CategoryButton_Click;
            return categoryButton;
        }

        private Button CreateSubcategoryButton(ServiceCategory subcategory)
        {
            Button subcategoryButton = new Button
            {
                Content = subcategory.CategoryName,
                Width = 200,
                Height = 90,
                FontSize = 20,
                Margin = new Thickness(5),
                Tag = subcategory.CategoryId,
                Background = (Brush)new BrushConverter().ConvertFrom("#305500"),
                Foreground = Brushes.White,
                Style = (Style)App.Current.Resources["MaterialDesignFlatButton"]
            };
            subcategoryButton.Click += SubcategoryButton_Click;
            return subcategoryButton;
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is int categoryId && categoryId != -1)
            {
                navigationStack.Push(categoryId);
                ExecuteYourCommandForButtonClick(categoryId);
            }
        }

        private void SubcategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is int categoryId && categoryId != -1)
            {
                navigationStack.Push(categoryId);
                ExecuteYourCommandForButtonClick(categoryId);
            }
        }

        private void DisplayServices(List<Service> services)
        {
            ServiceWrap.Children.Clear();
            foreach (Service service in services)
            {
                Button serviceButton = CreateServiceButton(service);
                ServiceWrap.Children.Add(serviceButton);
            }
        }

        private Button CreateServiceButton(Service service)
        {
            Button serviceButton = new Button
            {
                Content = service.ServiceName + "\n" + service.ServicePrice,
                Width = 200,
                Height = 90,
                FontSize = 20,
                Margin = new Thickness(5),
                DataContext = service,
                Background = (Brush)new BrushConverter().ConvertFrom("#00bcd4"),
                Foreground = Brushes.White,
                Style = (Style)App.Current.Resources["MaterialDesignFlatButton"]
            };
            serviceButton.Click += ServiceButton_Click;
            return serviceButton;
        }

        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.DataContext is Service service)
            {
                var existingService = dataGridOrdersNew.Items.Cast<Service>().FirstOrDefault(s => s.ServiceId == service.ServiceId);
                if (existingService != null)
                {
                    existingService.Quantity++;
                    dataGridOrdersNew.Items.Refresh();
                    TotalToOrders();
                }
                else
                {
                    Service newServiceOrder = new Service
                    {
                        ServiceId = service.ServiceId,
                        ServiceName = service.ServiceName,
                        ServicePrice = service.ServicePrice,
                        Quantity = 1
                    };
                    dataGridOrdersNew.Items.Add(newServiceOrder);
                    TotalToOrders();
                }
            }
        }

        private void Transport_Btn(object sender, RoutedEventArgs e) { }

        private void dataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalToOrders();
        }

        private void dataGridOrdersNew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalToOrders();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClient = new AddClient(this);
            addClient.ShowDialog();
        }

        private decimal CalculateTotal(IEnumerable<Service> services)
        {
            decimal total = 0;
            foreach (var service in services)
            {
                total += service.Quantity * service.ServicePrice;
            }
            return total;
        }

        private void PrintService_Click(object sender, RoutedEventArgs e)
        {
            if (dataGridOrdersNew.Items.Count > 0)
            {
                try
                {
                    using (var dbContext = new PowerSellDbContext())
                    {
                        string clientName = NameLabel.Content?.ToString();
                        var orderManager = new OrderManager(dbContext);
                        int tableId = TableId;
                        int userId = userid;
                        var services = dataGridOrdersNew.Items.Cast<Service>();

                        var orderList = new OrderList
                        {
                            Total = CalculateTotal(services),
                            Message = MessageLabel.Content?.ToString(),
                            Transport = TransportLabel.Content?.ToString(),
                            ClientName = tableId.ToString(),
                            IsReady = 0,
                            ServiceDateCreated = DateTime.Now,
                            TableId = tableId
                        };

                        dbContext.OrderList.Add(orderList);
                        dbContext.SaveChanges();

                        foreach (var service in services)
                        {
                            var product = dbContext.Service.SingleOrDefault(p => p.ServiceId == service.ServiceId);
                            if (product != null)
                            {
                                product.Quantity -= service.Quantity;
                            }
                        }
                        dbContext.SaveChanges();

                        orderManager.PrintServiceClick(tableId, userId, services, orderList.OrderListId);
                    }
                    PrintButton_Click(sender, e);
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error saving orders: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Нема Нарацки");
                this.Close();
            }
        }

        private void ReadyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var dbContext = new PowerSellDbContext())
                {
                    int tableId = TableId;
                    if (tableId > 0)
                    {
                        var ordersForTable = dbContext.Orders.Where(o => o.TableId == TableId).ToList();
                        if (ordersForTable.Any())
                        {
                            var orderListForTable = dbContext.OrderList.Where(ol => ol.IsReady == 0 && ol.TableId == TableId).ToList();
                            if (orderListForTable.Count > 0)
                            {
                                foreach (var orderList in orderListForTable)
                                {
                                    if (orderList.IsReady == 0) orderList.IsReady = 1;
                                }
                                dbContext.SaveChanges();
                                MessageBox.Show("Orders are marked as ready.");
                            }
                            else
                            {
                                MessageBox.Show("No orders found for the specified TableId.");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No orders found for the specified TableId.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid TableId. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating IsReady status: " + ex.Message);
            }
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            if (!string.IsNullOrEmpty(printerSettings.PrintOrderPrinter))
            {
                printDocument.PrinterSettings.PrinterName = printerSettings.PrintOrderPrinter;
            }

            printDocument.Print();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            string xmlFile = "PrintOrder.xml";
            PrintSettings printSettings = PrintSettingsLoader.LoadPrintSettings(xmlFile);

            double printWidth = printSettings.Width * 10;
            double printHeight = printSettings.Height * 10;

            StackPanel stackPanel = new StackPanel();

            foreach (var item in dataGridOrdersNew.Items) // Line ~486
            {
                if (item is Service service)
                {
                    TextBlock textBlock = new TextBlock
                    {
                        Text = $"\n" +
                               $"{printSettings.Separator}\n" + // Potential null here
                               $"{printSettings.ServiceName.Replace("{ServiceName}", service.ServiceName)}\n" + // Line ~496 (likely culprit)
                               $"{printSettings.ServicePrice.Replace("{ServicePrice}", service.ServicePrice.ToString())}\n" +
                               $"{printSettings.Quantity.Replace("{Quantity}", service.Quantity.ToString())}\n" +
                               $"{printSettings.Total.Replace("{Total}", (service.Quantity * service.ServicePrice).ToString())}\n" +
                               $"{printSettings.Employee.Replace("{UserName}", userName)}\n" + // Potential null here
                               $"{printSettings.Separator}"
                    };
                    stackPanel.Children.Add(textBlock);
                }
            }

            Size visualSize = new Size(printWidth, printHeight);
            stackPanel.Measure(visualSize);
            stackPanel.Arrange(new Rect(new Point(0, 0), visualSize));
            stackPanel.UpdateLayout();

            var printDialog = new PrintDialog();
            printDialog.PrintVisual(stackPanel, "Print Document");

            e.Cancel = true;
        }

        private async Task<bool> SaveOrdersToConfirmed()
        {
            try
            {
                var ordersToConfirm = dbContext.Orders.Where(o => o.TableId == TableId).ToList();
                var orderListsToConfirm = dbContext.OrderList.Where(o => o.TableId == TableId).ToList();
                bool allPaid = false;

                while (!allPaid)
                {
                    allPaid = true;
                    var result = MessageBox.Show($"Is order paid?", "Is Paid", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    foreach (var order in ordersToConfirm)
                    {
                        var confirmedOrder = dbContext.OrdersConfirmed.FirstOrDefault(o => o.OrdersId == order.OrdersId);
                        if (confirmedOrder == null)
                        {
                            var orderToUpdate = orderListsToConfirm.FirstOrDefault(o => o.OrderListId == order.OrderListId);
                            if (orderToUpdate != null)
                            {
                                orderToUpdate.ClientGetServiceDate = DateTime.Now;
                                orderToUpdate.IsPaid = result == MessageBoxResult.Yes;
                            }

                            if (result != MessageBoxResult.Yes)
                            {
                                allPaid = false;
                                continue;
                            }

                            dbContext.OrdersConfirmed.Add(new OrdersConfirmed
                            {
                                OrdersId = order.OrdersId,
                                ServiceId = order.ServiceId,
                                Quantity = order.Quantity,
                                ServicePrice = order.ServicePrice,
                                TableId = order.TableId,
                                ClientGetServiceDate = DateTime.Now,
                                ServiceDateIsReady = DateTime.Now,
                                IsPaid = true,
                                ClientGetService = true,
                                Total = order.Total,
                                UserId = userid,
                                OrderListId = order.OrderListId
                            });
                        }
                    }

                    if (!allPaid)
                    {
                        MessageBox.Show("Not all orders are paid. Please confirm payment for all orders before proceeding.", "Payment Confirmation", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }

                PrintOrdersToPaper(ordersToConfirm);
                await dbContext.SaveChangesAsync();
                dbContext.Orders.RemoveRange(ordersToConfirm);
                await dbContext.SaveChangesAsync();
                this.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing orders: " + ex.Message);
                return false;
            }
        }

        private async void BillButton_Click(object sender, RoutedEventArgs e)
        {
            await SaveOrdersToConfirmed();
            TotalToOrders();
        }

        private void PrintOrdersToPaper(List<Orders> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                MessageBox.Show("No orders to print.");
                return;
            }

            PrintDocument printDocument = new PrintDocument();
            printDocument.PrintPage += (sender, e) =>
            {
                string xmlFile = "PrintFisc.xml";
                PrintSettings printSettings = PrintSettingsLoader.LoadPrintSettings(xmlFile);

                double printWidth = printSettings.Width * 10;
                double printHeight = printSettings.Height * 10;

                StackPanel stackPanel = new StackPanel { HorizontalAlignment = HorizontalAlignment.Center };

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Paid",
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 10)
                });

                stackPanel.Children.Add(new TextBlock
                {
                    Text = "Artikal - Kolicina - Cena",
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 10)
                });
                stackPanel.Children.Add(new TextBlock { Text = printSettings.Separator });

                foreach (var order in orders)
                {
                    if (order?.Service != null)
                    {
                        stackPanel.Children.Add(new TextBlock
                        {
                            Text = $"{order.Service.ServiceName} - {order.Quantity} {order.ServicePrice}",
                            HorizontalAlignment = HorizontalAlignment.Center,
                            Margin = new Thickness(0, 20, 0, 10)
                        });
                    }
                }

                stackPanel.Children.Add(new TextBlock { Text = printSettings.Separator });

                var total = orders.Sum(t => t.ServicePrice * t.Quantity);
                stackPanel.Children.Add(new TextBlock
                {
                    Text = $"Total: {total}",
                    FontSize = 18,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 10)
                });

                Size visualSize = new Size(printWidth, printHeight);
                stackPanel.Measure(visualSize);
                stackPanel.Arrange(new Rect(new Point(0, 0), visualSize));
                stackPanel.UpdateLayout();

                var printDialog = new PrintDialog();
                printDialog.PrintVisual(stackPanel, "Print Fiscal Document");

                e.Cancel = true;
            };

            if (!string.IsNullOrEmpty(printerSettings.PrintFiscPrinter))
            {
                printDocument.PrinterSettings.PrinterName = printerSettings.PrintFiscPrinter;
            }

            printDocument.Print();
        }

        private void DeleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Service> selectedServices = dataGridOrdersNew.SelectedItems.Cast<Service>().ToList();
                foreach (Service service in selectedServices)
                {
                    dataGridOrdersNew.Items.Remove(service);
                    TotalToOrders();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting selected services: " + ex.Message);
            }
        }

        public void UpdateMessageLabel(string message)
        {
            MessageLabel.Content = message;
            MessageLabel.Visibility = Visibility.Visible;
        }

        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            var keyboardWindow = new KeyboardWindow();
            keyboardWindow.ShowDialog();
        }

        private void PaidButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (var dbContext = new PowerSellDbContext())
                {
                    int tableId = TableId;
                    if (tableId > 0)
                    {
                        var ordersForTable = dbContext.Orders.Where(o => o.TableId == TableId).ToList();
                        if (ordersForTable.Any())
                        {
                            var orderListForTable = dbContext.OrderList.Where(ol => ol.IsPaid == null && ol.TableId == TableId).ToList();
                            if (orderListForTable.Count > 0)
                            {
                                foreach (var orderList in orderListForTable)
                                {
                                    if (orderList.IsPaid == null) orderList.IsPaid = true;
                                }
                                dbContext.SaveChanges();
                                MessageBox.Show("SITE NARACKI E PLATENO.");
                            }
                            else
                            {
                                MessageBox.Show("NEMA NARACKI DA PLATI");
                            }
                        }
                        else
                        {
                            MessageBox.Show("No orders found for the specified TableId.");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Invalid TableId. Please try again.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating IsReady status: " + ex.Message);
            }
        }

        public void TotalToOrders()
        {
            double totalSum = 0;
            foreach (var item in dataGridOrdersNew.Items)
            {
                var totalProperty = item.GetType().GetProperty("Total");
                if (totalProperty != null)
                {
                    var cellValue = totalProperty.GetValue(item)?.ToString();
                    if (double.TryParse(cellValue, out double value))
                    {
                        totalSum += value;
                    }
                }
            }
            TotalToOrder.Content = "Total: " + totalSum.ToString("N2");
        }

        private void UpdateTotalOrders()
        {
            var orders = dataGridOrders.Items.Cast<OrderDTO>();
            decimal total = orders.Sum(order => order.Quantity * order.ServicePrice);
            TotalOrders.Content = $"Total: {total:C}";
        }

        // Method to open PrinterSettings UserControl in a dialog
        private void OpenPrinterSettings()
        {
                MetroWindow dialog = new MetroWindow
                {
                    Title = "Printer Settings",
                    Width = 400,
                    Height = 300,
                    Content = new PrinterSettings(),
                    Owner = this,
                    WindowStartupLocation = WindowStartupLocation.CenterOwner
                };
                dialog.ShowDialog();
                printerSettings = PrinterSettingsManager.LoadSettings(); // Reload settings after changes
        }

        // Example button handler to open PrinterSettings (add to XAML)
        private void PrinterSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            OpenPrinterSettings();
        }
    }
}