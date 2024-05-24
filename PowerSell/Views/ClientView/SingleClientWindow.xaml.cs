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

namespace PowerSell.Views.ClientView
{
    public partial class SingleClientWindow : MetroWindow
    {
        public ObservableCollection<ServiceCategory> YourServiceCategoriesCollection { get; set; }
        public ICommand YourCommandForButtonClick { get; set; }
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();
        private Stack<int> navigationStack = new Stack<int>();
        public int TableId { get; private set; }
        string userName = SessionManager.Instance.UserName;
        int userid = SessionManager.Instance.UserId;
     
        public SingleClientWindow(int tableId)
        {
            InitializeComponent();
            TableId = tableId;
            YourServiceCategoriesCollection = new ObservableCollection<ServiceCategory>();
            LoadCategories();
            LoadOrdersData(dataGridOrders);
            LoadClients(); // Call the method to load clients when the window is initialized
            AddClientDisable();
            ShowClientDetails(NameLabel, ClientIdLabel);
            YourCommandForButtonClick = new RelayCommand(ExecuteYourCommandForButtonClick);
            TotalToOrders();
            UpdateTotalOrders();
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
                Binding = new Binding("OrdersId")
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
            using (var dbContext = new PowerSellDbContext())
            {
                var orders = dbContext.Database.SqlQuery<OrderDTO>(
                    "EXEC GetActiveOrdersByTableId @TableId",
                    new SqlParameter("@TableId", tableId)
                ).ToList();

                return orders;
            }
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
                Width = 200,
                Height = 90,
                FontSize = 20,
                Margin = new Thickness(5),
                Tag = category.CategoryId,
           
            };
            // Apply Material Design styles
            categoryButton.Background = (Brush)new BrushConverter().ConvertFrom("#125535"); // Aqua color
            categoryButton.Foreground = Brushes.White; // White text color
            categoryButton.Style = (Style)App.Current.Resources["MaterialDesignFlatButton"];
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
                FontSize=20,
                Margin = new Thickness(5),
                Tag = subcategory.CategoryId
            };

            // Apply Material Design styles
            subcategoryButton.Background = (Brush)new BrushConverter().ConvertFrom("#305500"); // Aqua color
            subcategoryButton.Foreground = Brushes.White; // White text color
            subcategoryButton.Style = (Style)App.Current.Resources["MaterialDesignFlatButton"];
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
                Content = service.ServiceName + "\n" + service.ServicePrice,
                Width = 200,
                Height = 90,
                FontSize = 20,
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
                    TotalToOrders();
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
                    TotalToOrders();
                }
            }
            TotalToOrders();
        }

        private void Transport_Btn(object sender, RoutedEventArgs e)
        {
          
        }

        private void dataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalToOrders();
            // Handle selection changes if needed
        }
        private void dataGridOrdersNew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TotalToOrders();
            // Handle selection changes if needed
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            // Open the AddClient dialog and pass the instance of SingleClientWindow
            AddClient addClient = new AddClient(this);
            addClient.ShowDialog();
        }

        private void ShowClientDetails(Label nameLabel, Label clientIdLabel)
        {
            try
            {
                using (var dbContext = new PowerSellDbContext())
                {
                    // Get the TableId from your application logic
                    int tableId = TableId; // Implement GetTableId() method to fetch the TableId

                    // Initialize output parameters
                    var clientNameParameter = new SqlParameter("@ClientName", SqlDbType.NVarChar, 100);
                    clientNameParameter.Direction = ParameterDirection.Output;

                    var clientIdParameter = new SqlParameter("@ClientId", SqlDbType.Int);
                    clientIdParameter.Direction = ParameterDirection.Output;

                    // Call the stored procedure
                    dbContext.Database.ExecuteSqlCommand(
                        "EXEC ShowClientDetailsByTableId @TableId, @ClientName OUT, @ClientId OUT",
                        new SqlParameter("@TableId", tableId),
                        clientNameParameter,
                        clientIdParameter);

                    // Retrieve output parameter values
                    string clientName = clientNameParameter.Value?.ToString();
                    int clientId = (int)(clientIdParameter.Value ?? 0);

                    // Update the labels with client details
                    nameLabel.Content = clientName ?? "No client details found";
                    clientIdLabel.Content = clientId.ToString();
                }
            }
            catch (Exception ex)
            {
                // Update the labels with client details
                nameLabel.Content = "No client details found" + ex;
            }
        }


        // Method to update the UI with the newly added client
        public void UpdateClient(Client newClient)
        {
            // Update the labels with the new client information
            ClientIdLabel.Content = newClient.ClientId;
            NameLabel.Content = newClient.ClientName;
            PhoneLabel.Content = newClient.ClientPhone;
            EmailLabel.Content = newClient.ClientEmail;

            // Reload the data grid to remove the added items

        }
        public void AddClientDisable()
        {
            try
            {
                using (var dbContext = new PowerSellDbContext())
                {
                    int tableId = TableId; // Get the TableId from your application logic

                    // Create parameters for the stored procedure
                    var tableIdParam = new SqlParameter("@TableId", tableId);
                    var existsParam = new SqlParameter("@Exists", SqlDbType.Bit)
                    {
                        Direction = ParameterDirection.Output
                    };

                    // Execute the stored procedure
                    dbContext.Database.ExecuteSqlCommand("CheckOrderList @TableId, @Exists OUTPUT",
                        tableIdParam,
                        existsParam);

                    bool orderListExists = Convert.ToBoolean(existsParam.Value);

                    // Update UI based on the stored procedure result
                    AddButton.IsEnabled = !orderListExists;
                    clientComboBox.IsEnabled = !orderListExists;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error checking OrderList: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadClients()
        {
            try
            {
                // Retrieve the list of clients from the database
                List<Client> clients = dbContext.Client.ToList();

                // Set the ComboBox's ItemsSource to the list of clients
                clientComboBox.ItemsSource = clients;

                // Define how the ComboBox displays the client information (e.g., by ClientName)
                clientComboBox.DisplayMemberPath = "DisplayName";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading clients: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void clientComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changes in the ComboBox (e.g., update labels with selected client's details)
            if (clientComboBox.SelectedItem is Client selectedClient)
            {
                NameLabel.Content = selectedClient.ClientName;
                ClientIdLabel.Content = selectedClient.ClientId;
                PhoneLabel.Content = selectedClient.ClientPhone;
            }
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
                        // Get the client name from NameLabel
                        string clientName = NameLabel.Content?.ToString();
                        int clientID = Convert.ToInt32(ClientIdLabel.Content?.ToString());

                        // Call ShowClientDetails with the clientName
                        ShowClientDetails(NameLabel, ClientIdLabel);

                        // Instantiate OrderManager
                        var orderManager = new OrderManager(dbContext);

                        // Get the TableId and UserId from your application logic
                        int tableId = TableId; // Implement GetTableId() method to fetch the TableId
                        int userId = userid; // Implement GetUserId() method to fetch the UserId

                        // Get the collection of Service objects from your data grid
                        var services = dataGridOrdersNew.Items.Cast<Service>();

                        // Create an OrderList object
                        var orderList = new OrderList
                        {
                            Total = CalculateTotal(services), // Calculate the total based on services
                            Message = MessageLabel.Content?.ToString(), // Get the message from your UI elements
                            Transport = TransportLabel.Content?.ToString(), // Get the transport details from your UI elements
                            ClientName = clientName, // Use the client name obtained earlier
                            ClientId = clientID,
                            IsReady = 0,
                            ServiceDateCreated = DateTime.Now,
                            TableId = tableId
                        };

                        // Add the OrderList to the database
                        dbContext.OrderList.Add(orderList);
                        dbContext.SaveChanges(); // Save changes to get the OrderListId

                        // Decrement the quantity of products
                        foreach (var service in services)
                        {
                            var product = dbContext.Service.SingleOrDefault(p => p.ServiceId == service.ServiceId);
                            if (product != null)
                            {
                                product.Quantity -= service.Quantity;
                            }
                        }

                        dbContext.SaveChanges(); // Save changes to update product quantities

                        // Call PrintServiceClick method of OrderManager and pass OrderListId
                        orderManager.PrintServiceClick(tableId, userId, services, orderList.OrderListId);
                    }

                    PrintButton_Click(sender, e); // Call the print button click event
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
                    int tableId = TableId; // Get the TableId for which to update IsReady status

                    if (tableId > 0)
                    {
                        var ordersForTable = dbContext.Orders.Where(o => o.TableId == TableId).ToList();

                        if (ordersForTable.Any())
                        {
                            int orderListId = ordersForTable.First().OrderListId; // Get OrderListId

                            var orderListForTable = dbContext.OrderList
                                .Where(ol =>ol.IsReady == 0 && ol.TableId == TableId)
                                .ToList();

                            if (orderListForTable.Count > 0)
                            {
                                // Update IsReady status for the found orders
                                foreach (var orderList in orderListForTable)
                                {
                                    if (orderList.IsReady == 0)
                                    {
                                        orderList.IsReady = 1; // Set IsReady to 1
                                    }
                                }

                                // Save changes to the database
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
            // Create a PrintDocument instance
            PrintDocument printDocument = new PrintDocument();

            // Set up the PrintPage event handler
            printDocument.PrintPage += new PrintPageEventHandler(PrintDocument_PrintPage);

            // Start printing without showing the print dialog
            printDocument.Print();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Load print settings from XML
            PrintSettings printSettings = PrintSettingsLoader.LoadPrintSettings("psprint.xml");

            // Set the print size
            double printWidth = printSettings.Width * 10; // Convert inches to millimeters (1 inch = 25.4 mm)
            double printHeight = printSettings.Height * 10; // Set the height in millimeters

            // Create a visual to print (e.g., a StackPanel)
            StackPanel stackPanel = new StackPanel();

            // Iterate through the items in the DataGrid
            foreach (var item in dataGridOrdersNew.Items)
            {
                if (item is Service service)
                {
                    // Create a TextBlock for each service and add it to the stack panel
                    TextBlock textBlock = new TextBlock();
                    textBlock.Text = $"\n" +
                                     $"{printSettings.Separator}\n" +
                                     $"{printSettings.ServiceName.Replace("{ServiceName}", service.ServiceName)}\n" +
                                     $"{printSettings.ServicePrice.Replace("{ServicePrice}", service.ServicePrice.ToString())}\n" +
                                     $"{printSettings.Quantity.Replace("{Quantity}", service.Quantity.ToString())}\n" +
                                     $"{printSettings.Total.Replace("{Total}", service.Total.ToString())}\n" +
                                     $"{printSettings.Employee.Replace("{UserName}", userName)}\n" +
                                     $"{printSettings.Separator}"; // Separator between items
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
                var orderListsToConfirm = dbContext.OrderList
                    .Where(o => o.TableId == TableId)
                    .ToList();

                bool allPaid = false;

                while (!allPaid)
                {
                    allPaid = true; // Assume all orders are paid unless proven otherwise
                                    // Prompt the user for IsPaid status
                    var result = MessageBox.Show($"Is order paid?", "Is Paid", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    foreach (var order in ordersToConfirm)
                    {
                        // Check if the order is already confirmed
                        var confirmedOrder = dbContext.OrdersConfirmed.FirstOrDefault(o => o.OrdersId == order.OrdersId);

                        if (confirmedOrder == null)
                        {
                           

                            // Update ClientGetServiceDate in OrderList
                            var orderToUpdate = orderListsToConfirm.FirstOrDefault(o => o.OrderListId == order.OrderListId);
                            if (orderToUpdate != null)
                            {
                                orderToUpdate.ClientGetServiceDate = DateTime.Now;
                                orderToUpdate.IsPaid = result == MessageBoxResult.Yes; // Set IsPaid based on user input
                            }

                            // If the order is not paid, set allPaid to false
                            if (result != MessageBoxResult.Yes)
                            {
                                allPaid = false;
                                continue; // Skip adding to OrdersConfirmed if not paid
                            }

                            // Add selected order to OrdersConfirmed table
                            dbContext.OrdersConfirmed.Add(new OrdersConfirmed
                            {
                                OrdersId = order.OrdersId,
                                ServiceId = order.ServiceId,
                                Quantity = order.Quantity,
                                ServicePrice = order.ServicePrice,
                                TableId = order.TableId,
                                ClientGetServiceDate = DateTime.Now,
                                ServiceDateIsReady = DateTime.Now,
                                IsPaid = true, // Set IsPaid to true since it's confirmed
                                ClientGetService = true, // Assuming client gets service for now
                                Total = order.Total,
                                UserId = userid,
                                OrderListId = order.OrderListId
                                // Add other properties as needed
                            });
                        }
                    }

                    if (!allPaid)
                    {
                        // If not all orders are paid, show a message and wait for further action
                        MessageBox.Show("Not all orders are paid. Please confirm payment for all orders before proceeding.", "Payment Confirmation", MessageBoxButton.OK, MessageBoxImage.Warning);
                        return false;
                    }
                }
                // Print the order details
                PrintOrdersToPaper(ordersToConfirm);
                await dbContext.SaveChangesAsync();
              

                // If insertion to OrdersConfirmed was successful and IsPaid is true, remove orders from Orders table
                dbContext.Orders.RemoveRange(ordersToConfirm);
                await dbContext.SaveChangesAsync();
                this.Close();
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
            TotalToOrders();

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
                // Create a TextBlock for the "Paid" label
                TextBlock orderinfo = new TextBlock
                {
                    Text = "Artikal - Kolicina - Cena",
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 10) // Margin for spacing
                };
                TextBlock lines = new TextBlock();
                lines.Text = "-----------------------------------";
                stackPanel.Children.Add(orderinfo); 
                stackPanel.Children.Add(lines);

                // Iterate through the orders and create TextBlocks for each order
                foreach (var order in orders)
                {
                    if (order != null && order.Service != null)
                    {
                        TextBlock textBlock = new TextBlock();
                        textBlock.Text = $"{order.Service.ServiceName} - {order.Quantity} {order.ServicePrice}";
                        textBlock.HorizontalAlignment = HorizontalAlignment.Center; // Center the text horizontally
                        textBlock.Margin = new Thickness(0, 20, 0, 10);
                        stackPanel.Children.Add(textBlock);
                    }
                }
                TextBlock linesbottom = new TextBlock();
                linesbottom.Text = "-----------------------------------";
                stackPanel.Children.Add(lines);
                var total = orders.Sum(t => t.ServicePrice * t.Quantity);
                TextBlock totalpaid = new TextBlock
                {
                    Text = $"Total: {total}",
                    FontSize=18,
                    FontWeight = FontWeights.Bold,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 10) // Margin for spacing
                };
                stackPanel.Children.Add(totalpaid);
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
            MessageLabel.Visibility = Visibility.Visible; // Show the MessageLabel
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
                        int tableId = TableId; // Get the TableId for which to update IsReady status

                        if (tableId > 0)
                        {
                            var ordersForTable = dbContext.Orders.Where(o => o.TableId == TableId).ToList();

                            if (ordersForTable.Any())
                            {
                                int orderListId = ordersForTable.First().OrderListId; // Get OrderListId

                                var orderListForTable = dbContext.OrderList
                                    .Where(ol => ol.IsPaid == null && ol.TableId == TableId)
                                    .ToList();

                                if (orderListForTable.Count > 0)
                                {
                                    // Update IsReady status for the found orders
                                    foreach (var orderList in orderListForTable)
                                    {
                                        if (orderList.IsPaid == null)
                                        {
                                            orderList.IsPaid = true; // Set IsReady to 1
                                        }
                                    }

                                    // Save changes to the database
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

            // Loop through each row in the DataGrid
            foreach (var item in dataGridOrdersNew.Items)
            {
                // Use reflection to get the property value if item is not DataRowView
                var totalProperty = item.GetType().GetProperty("Total");
                if (totalProperty != null)
                {
                    var cellValue = totalProperty.GetValue(item)?.ToString();
                  //  Console.WriteLine("Cell Value: " + cellValue); // Debugging output

                    if (double.TryParse(cellValue, out double value))
                    {
                        totalSum += value;
                       // Console.WriteLine("Parsed Value: " + value + " | Running Total: " + totalSum); // Debugging output
                    }
                    else
                    {
                      //  Console.WriteLine("Failed to parse: " + cellValue); // Log failed parse attempts
                    }
                }
            }

            // Display the sum in the Label
            TotalToOrder.Content = "Total: " + totalSum.ToString("N2");
            Console.WriteLine("Final Total: " + totalSum); // Debugging output
        }
        private void UpdateTotalOrders()
        {
            // Assuming the DataContext of the DataGrid is a collection of OrderDTO
            var orders = dataGridOrders.Items.Cast<OrderDTO>();

            // Calculate the total
            decimal total = orders.Sum(order => order.Quantity * order.ServicePrice);

            // Update the TotalOrders label
            TotalOrders.Content = $"Total: {total:C}";
        }
    }
}
