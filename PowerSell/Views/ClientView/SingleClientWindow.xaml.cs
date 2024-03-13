using PowerSell.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using PowerSell.Services;
using System.Linq;

namespace PowerSell.Views.ClientView
{
    public partial class SingleClientWindow : Window
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
                Tag = service.ServiceId // Assuming ServiceId is unique
            };

            serviceButton.Click += ServiceButton_Click;

            return serviceButton;
        }

        private void ServiceButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is int serviceId)
            {
                // Handle service button click event here
                // You can navigate to another window, display details, etc.
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            AddClient addClientWindow = new AddClient();
            addClientWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Handle button click event here
            // This method is currently empty, you can add your logic as needed
        }
    }
}
