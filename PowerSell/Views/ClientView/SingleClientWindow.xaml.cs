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

namespace PowerSell.Views.ClientView
{
    public partial class SingleClientWindow : MetroWindow
    {
        public ObservableCollection<ServiceCategory> YourServiceCategoriesCollection { get; set; }
        public ICommand YourCommandForButtonClick { get; set; }
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();
        private Stack<int> navigationStack = new Stack<int>();

        public int TableId { get; private set; }

        public SingleClientWindow(int tableId)
        {
            InitializeComponent();
            TableId = tableId;
            YourServiceCategoriesCollection = new ObservableCollection<ServiceCategory>();
            LoadCategories();
            LoadOrdersData();
            YourCommandForButtonClick = new RelayCommand(ExecuteYourCommandForButtonClick);
        }

        private void LoadOrdersData()
        {
            List<OrderDTO> orders = GetActiveOrdersByTableId(TableId);
            dataGridOrders.ItemsSource = orders;
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
                Width = 130,
                Height = 50,
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
                Width = 130,
                Height = 50,
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
            var keyboardWindow = new KeyboardWindow();
            keyboardWindow.ShowDialog();
        }

        private void dataGridOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changes if needed
        }
        private void dataGridOrdersNew_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Handle selection changes if needed
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClientWindow = new AddClient();
            addClientWindow.ShowDialog();
        }
        private void PrintService_Click(object sender, RoutedEventArgs e)
        {
            // Save data to Orders table
            foreach (Service service in dataGridOrdersNew.Items.Cast<Service>())  // Changed type to Service
            {
                // Assuming Orders table has OrdersId, ServiceId, Quantity, ServicePrice columns
                dbContext.Orders.Add(new Orders
                {
                    // Map properties from Service to Order
                    OrdersId = service.ServiceId,  // Assuming OrdersId exists in Orders table
                    ServiceId = service.ServiceId,
                    Quantity = service.Quantity,  // Call a method to get quantity (optional)
                    ServicePrice = service.ServicePrice,
                    TableId = TableId  // Add TableId to associate orders with the table
                });
            }

            try
            {
                dbContext.SaveChanges();  // Save changes to the database
                MessageBox.Show("Orders saved successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving orders: " + ex.Message);
            }
        }
  
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Handle button click event here
            // This method is currently empty, you can add your logic as needed
        }
    }
}
