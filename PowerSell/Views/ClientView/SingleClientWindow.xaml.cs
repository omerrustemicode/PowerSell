using PowerSell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Data.Entity;
using PowerSell.Services;

namespace PowerSell.Views.ClientView
{
    public partial class SingleClientWindow : Window
    {
        public ObservableCollection<ServiceCategory> YourServiceCategoriesCollection { get; set; }
        public ICommand YourCommandForButtonClick { get; set; }
        private readonly PowerSellDbContext dbContext = new PowerSellDbContext();

        public int TableId { get; private set; }

        public SingleClientWindow(int tableId)
        {
            InitializeComponent();
            TableId = tableId;

            YourServiceCategoriesCollection = new ObservableCollection<ServiceCategory>();
            LoadCategories();

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
            List<ServiceCategory> categories = GetAllCategories();
            PopulateCategoryButtons(categories);
        }

        private List<ServiceCategory> GetAllCategories()
        {
            return dbContext.ServiceCategory
                            .Where(category => category.CategoryParentId == null)
                            .ToList();
        }

        private void PopulateCategoryButtons(List<ServiceCategory> categories)
        {
            foreach (ServiceCategory category in categories)
            {
                Button categoryButton = CreateCategoryButton(category);
                YourWrapPanel.Children.Add(categoryButton);
            }
        }

        private Button CreateCategoryButton(ServiceCategory category)
        {
            Button categoryButton = new Button
            {
                Content = category.CategoryName,
                Width = 100,
                Height = 30,
                Margin = new Thickness(5),
                Tag = category.CategoryId
            };

            categoryButton.Click += CategoryButton_Click;

            return categoryButton;
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button clickedButton && clickedButton.Tag is int categoryId)
            {
                ExecuteYourCommandForButtonClick(categoryId);
            }
        }

        private void ExecuteYourCommandForButtonClick(object parameter)
        {
            if (parameter is int categoryId)
            {
                List<ServiceCategory> subcategories = GetSubcategories(categoryId);

                YourWrapPanel.Children.Clear();

                foreach (ServiceCategory subcategory in subcategories)
                {
                    Button subcategoryButton = CreateCategoryButton(subcategory);
                    YourWrapPanel.Children.Add(subcategoryButton);
                }
            }
        }

        private List<ServiceCategory> GetSubcategories(int parentCategoryId)
        {
            return dbContext.ServiceCategory
                .Where(category => category.CategoryParentId == parentCategoryId)
                .ToList();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Your existing button click logic
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
    }
}
