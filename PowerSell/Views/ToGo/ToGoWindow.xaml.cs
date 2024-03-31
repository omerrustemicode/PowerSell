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
using PowerSell.Views.ClientView;

namespace PowerSell.Views.ToGo
{
    public partial class ToGoWindow : MetroWindow
    {
        public ObservableCollection<ServiceCategory> YourServiceCategoriesCollection { get; set; }
        public ICommand YourCommandForButtonClick { get; set; }
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();
        private Stack<int> navigationStack = new Stack<int>();
        public int TableId { get; private set; }
        string userName = SessionManager.Instance.UserName;
        int userid = SessionManager.Instance.UserId;
        public ToGoWindow()
        {
            InitializeComponent();
            YourServiceCategoriesCollection = new ObservableCollection<ServiceCategory>();
            LoadCategories();
            LoadOrdersData(dataGridOrders);

            YourCommandForButtonClick = new RelayCommand(ExecuteYourCommandForButtonClick);
        }

        private void LoadOrdersData(DataGrid dataGrid)
        {
            List<OrderDTO> orders = GetActiveOrdersByTableId(TableId);

            // Clear existing columns
            dataGrid.Columns.Clear();

            // Set AutoGenerateColumns to false
            dataGrid.AutoGenerateColumns = false;

            // Define columns you want to show
            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Order ID",
                Binding = new Binding("OrderId")
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Service Name",
                Binding = new Binding("ServiceName")
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Quantity",
                Binding = new Binding("Quantity")
            });

            dataGrid.Columns.Add(new DataGridTextColumn
            {
                Header = "Service Price",
                Binding = new Binding("ServicePrice")
            });

            // Set ItemsSource to display the selected columns
            dataGrid.ItemsSource = orders;
        }



        private List<OrderDTO> GetActiveOrdersByTableId(int tableId)
        {
            return dbContext.Orders
                .Include(o => o.Service)
                .Where(o => o.TableId == tableId)
                .Select(o => new OrderDTO
                {
                    OrdersId = o.OrdersId,
                    ServiceName = o.Service.ServiceName,
                    Quantity = o.Quantity,
                    ServicePrice = o.ServicePrice,
                })
                .ToList();
        }

        private void LoadCategories()
        {
            ExecuteYourCommandForButtonClick(0); // Display top-level categories initially
        }

        private void ExecuteYourCommandForButtonClick(object parameter)
        {
            if (parameter is int categoryId)
            {
                if (categoryId == 0)
                {
                    // Display top-level categories
                    List<ServiceCategory> categoriesToDisplay = GetAllCategories();
                    DisplayCategories(categoriesToDisplay);
                }
                else
                {
                    // Display subcategories of the clicked category if they exist
                    List<ServiceCategory> subcategoriesToDisplay = GetSubcategories(categoryId);
                    DisplaySubcategories(subcategoriesToDisplay);

                    // Display services associated with the selected category
                    List<Service> servicesToDisplay = GetServicesByCategory(categoryId);
                    DisplayServices(servicesToDisplay);
                }
            }
        }

        private List<ServiceCategory> GetSubcategories(int parentCategoryId)
        {
            return dbContext.ServiceCategory
                .Where(category => category.CategoryParentId == parentCategoryId)
                .ToList();
        }

        private List<Service> GetServicesByCategory(int categoryId)
        {
            return dbContext.Service
                .Where(s => s.CategoryId == categoryId)
                .ToList();
        }

        private List<ServiceCategory> GetAllCategories()
        {
            return dbContext.ServiceCategory
                            .Where(category => category.CategoryParentId == null)
                            .ToList();
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
                Width = 150,
                Height = 60,
                Margin = new Thickness(5),
                Tag = category.CategoryId
            };

            categoryButton.Click += CategoryButton_Click;

            return categoryButton;
        }

        private Button CreateSubcategoryButton(ServiceCategory subcategory)
        {
            Button subcategoryButton = new Button
            {
                Content = subcategory.CategoryName,
                Width = 150,
                Height = 60,
                Margin = new Thickness(5),
                Tag = subcategory.CategoryId
            };

            subcategoryButton.Click += SubcategoryButton_Click;

            return subcategoryButton;
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is int categoryId && categoryId != -1) // Update condition to prevent clicking on the same category
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
                Content = service.ServiceName,
                Width = 130,
                Height = 50,
                Margin = new Thickness(5),
                DataContext = service // Set the DataContext to the Service object
            };

            // Apply Material Design styles
            serviceButton.Background = (Brush)new BrushConverter().ConvertFrom("#00bcd4"); // Aqua color
            serviceButton.Foreground = Brushes.White; // White text color
            serviceButton.Style = (Style)App.Current.Resources["MaterialDesignFlatButton"];

            serviceButton.Click += ServiceButton_Click;

            return serviceButton;
        }

        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.DataContext is Service service)
            {
                // Check if the service already exists in the data grid
                var existingService = dataGridOrdersNew.Items.Cast<Service>()
                                            .FirstOrDefault(s => s.ServiceId == service.ServiceId);

                if (existingService != null)
                {
                    // If the service exists, increase the quantity
                    existingService.Quantity++;

                    // Refresh the data grid to reflect the changes
                    dataGridOrdersNew.Items.Refresh();
                }
                else
                {
                    // If the service doesn't exist, create a new Service object
                    Service newServiceOrder = new Service
                    {
                        ServiceId = service.ServiceId,
                        ServiceName = service.ServiceName,
                        ServicePrice = service.ServicePrice,
                        Quantity = 1, // Set the default quantity here
                    };

                    // Add the new ServiceOrder to the data grid
                    dataGridOrdersNew.Items.Add(newServiceOrder);
                }
            }
        }

        private void Transport_Btn(object sender, RoutedEventArgs e)
        {
          
        }

        private void dataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changes if needed
        }
        private void dataGridOrdersNew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changes if needed
        }


        private void ShowClientDetails_Click(object sender, RoutedEventArgs e)
        {
            string name = NameLabel.Content?.ToString(); // Get the client's name from the label
            string phone = PhoneLabel.Content?.ToString(); // Get the client's phone from the label
            string email = EmailLabel.Content?.ToString(); // Get the client's email from the label

            // Display the client details in a message box
            MessageBox.Show($"Name: {name}\nPhone: {phone}\nEmail: {email}", "Client Details", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Method to update the UI with the newly added client
        public void UpdateClient(Client newClient)
        {
            // Update the labels with the new client information
            NameLabel.Content = newClient.ClientName;
            PhoneLabel.Content = newClient.ClientPhone;
            EmailLabel.Content = newClient.ClientEmail;

            // Reload the data grid to remove the added items

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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Handle button click event here
            // This method is currently empty, you can add your logic as needed
        }
        private async Task<bool> SaveOrdersToConfirmed()
        {
            try
            {
                // Select all orders with the matching TableId
                var ordersToConfirm = dbContext.Orders
                    .Where(o => o.TableId == TableId)
                    .ToList();

                // Add selected orders to OrdersConfirmed table
                foreach (var order in ordersToConfirm)
                {
                    dbContext.OrdersConfirmed.Add(new OrdersConfirmed
                    {
                        OrdersId = order.OrdersId,
                        ServiceId = order.ServiceId,
                        Quantity = order.Quantity,
                        ServicePrice = order.ServicePrice,
                        TableId = order.TableId,
                        ServiceDateCreated = order.ServiceDateCreated,
                        ClientGetServiceDate = DateTime.Now,
                        ServiceDateIsReady = DateTime.Now,
                        IsPaid = true,
                        ServiceDIscount = 0,
                        ClientGetService = true,
                        Total = order.Total,
                        UserId = userid,
                        OrderListId = order.OrderListId
                        // Add other properties as needed
                    });
                }

                await dbContext.SaveChangesAsync();
                // Print the order details
                PrintOrdersToPaper(ordersToConfirm);
                // If insertion to OrdersConfirmed was successful, remove orders from Orders table
                dbContext.Orders.RemoveRange(ordersToConfirm);
                await dbContext.SaveChangesAsync();
             
                return true; // Return true to indicate successful execution
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error processing orders: " + ex.Message);
                return false; // Return false to indicate failure
            }
        }

        private async void BillButton_Click(object sender, RoutedEventArgs e)
        {
            // Call the method to save orders to OrdersConfirmed and delete from Orders
            await SaveOrdersToConfirmed();

            // Add other actions or messages as needed
        }

        private void PrintOrdersToPaper(List<Orders> orders)
        {
            if (orders == null || orders.Count == 0)
            {
                MessageBox.Show("No orders to print.");
                return;
            }

            // Create a PrintDocument instance
            PrintDocument printDocument = new PrintDocument();

            // Set up the PrintPage event handler
            printDocument.PrintPage += new PrintPageEventHandler((sender, e) =>
            {
                // Set the print size in millimeters
                double printWidth = 72.1 * 10; // Convert inches to millimeters (1 inch = 25.4 mm)
                double printHeight = 99999 * 10; // Set the height in millimeters

                // Create a visual to print (e.g., a StackPanel)
                StackPanel stackPanel = new StackPanel();
                stackPanel.HorizontalAlignment = HorizontalAlignment.Center; // Center the stack panel horizontally

                // Create a TextBlock for the "Paid" label
                TextBlock paidLabel = new TextBlock
                {
                    Text = "Paid",
                    FontSize = 20,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 10) // Margin for spacing
                };
                stackPanel.Children.Add(paidLabel);

                // Iterate through the orders and create TextBlocks for each order
                foreach (var order in orders)
                {
                    if (order != null && order.Service != null)
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = $"Service Name: {order.Service.ServiceName}\n" +
                                            $"Quantity: {order.Quantity}\n" +
                                            $"Price: ${order.ServicePrice}\n" +
                                            "------------------------------------\n"; // Separator between items
                        textBlock.HorizontalAlignment = HorizontalAlignment.Center; // Center the text horizontally
                        stackPanel.Children.Add(textBlock);
                    }
                }

                // Measure and arrange the stack panel for printing
                Size visualSize = new Size(printWidth, printHeight);
                stackPanel.Measure(visualSize);
                stackPanel.Arrange(new Rect(new Point(0, 0), visualSize));
                stackPanel.UpdateLayout();

                // Render the stack panel to the print page
                var printDialog = new PrintDialog();
                printDialog.PrintVisual(stackPanel, "Print Document");

                // Mark the event as handled to prevent additional printing
                e.Cancel = true;
            });

            // Start printing without showing the print dialog
            printDocument.Print();
        }

        private void DeleteServiceButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get the selected items from the data grid
                List<Service> selectedServices = dataGridOrdersNew.SelectedItems.Cast<Service>().ToList();

                // Remove the selected items from the data grid
                foreach (Service service in selectedServices)
                {
                    dataGridOrdersNew.Items.Remove(service);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting selected services: " + ex.Message);
            }
        }

        public void UpdateMessageLabel(string message)
        {
            MessageLabel.Content = "Message: " + message;
            MessageLabel.Visibility = Visibility.Visible; // Show the MessageLabel
        }

        private void MessageButton_Click(object sender, RoutedEventArgs e)
        {
            var keyboardWindow = new KeyboardWindow();
            keyboardWindow.ShowDialog();
        }
    }
}
