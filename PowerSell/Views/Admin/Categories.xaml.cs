using PowerSell.Models;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PowerSell.Views.Admin
{
    public partial class Categories : UserControl
    {
        private readonly PowerSellDbContext _dbContext;
        private List<CategoryViewModel> _categories;

        public Categories()
        {
            InitializeComponent();
            _dbContext = new PowerSellDbContext();
            LoadCategories();
            cmbParentCategory.ItemsSource = _categories.Where(c => c.Subcategories.Count == 0).ToList();
            treeCategories.SelectedItemChanged += treeCategories_SelectedItemChanged;
        }

        private void LoadCategories()
        {
            var categoriesFromDb = _dbContext.ServiceCategory.ToList();
            var parentCategories = categoriesFromDb
                .Where(c => c.CategoryParentId == null)
                .Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Subcategories = new List<CategoryViewModel>()
                })
                .ToList();

            foreach (var parent in parentCategories)
            {
                parent.Subcategories.AddRange(categoriesFromDb
                    .Where(s => s.CategoryParentId == parent.CategoryId)
                    .Select(s => new CategoryViewModel
                    {
                        CategoryId = s.CategoryId,
                        CategoryName = s.CategoryName,
                        Subcategories = new List<CategoryViewModel>()
                    })
                    .ToList());
            }

            _categories = parentCategories;
            cmbParentCategory.ItemsSource = categoriesFromDb; // Set ItemsSource to the list of categories
            cmbParentCategory.DisplayMemberPath = "CategoryName"; // Set DisplayMemberPath to CategoryName
            treeCategories.ItemsSource = _categories; // Set ItemsSource to _categories, which contains the parent categories
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();
            if (!string.IsNullOrEmpty(categoryName))
            {
                ServiceCategory category = new ServiceCategory { CategoryName = categoryName };
                _dbContext.ServiceCategory.Add(category);
                _dbContext.SaveChanges();
                LoadCategories(); // Reload categories to update the tree view and ComboBox
            }
        }

        private void AddSubcategory_Click(object sender, RoutedEventArgs e)
        {
            string subcategoryName = txtSubcategoryName.Text.Trim();
            if (!string.IsNullOrEmpty(subcategoryName) && cmbParentCategory.SelectedItem is CategoryViewModel parentCategory)
            {
                ServiceCategory subcategory = new ServiceCategory
                {
                    CategoryName = subcategoryName,
                    CategoryParentId = parentCategory.CategoryId
                };
                _dbContext.ServiceCategory.Add(subcategory);
                _dbContext.SaveChanges();
                LoadCategories(); // Reload categories to update the tree view and ComboBox
            }
            else
            {
                MessageBox.Show("Please select a parent category.");
            }
        }

        private void treeCategories_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Handle selected item changed event if needed
        }

        public class CategoryViewModel
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public List<CategoryViewModel> Subcategories { get; set; }
        }
    }
}
