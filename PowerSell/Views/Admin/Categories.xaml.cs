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
            treeCategories.SelectedItemChanged += treeCategories_SelectedItemChanged;
        }

        private void LoadCategories()
        {
            var categoriesFromDb = _dbContext.ServiceCategory.ToList();

            // Create parent categories
            var parentCategories = categoriesFromDb
                .Where(c => c.CategoryParentId == null)
                .Select(c => new CategoryViewModel
                {
                    CategoryId = c.CategoryId,
                    CategoryName = c.CategoryName,
                    Subcategories = new List<CategoryViewModel>()
                })
                .ToList();

            // Add subcategories to parent categories
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
            treeCategories.ItemsSource = _categories;
            cmbParentCategory.ItemsSource = parentCategories;
            cmbParentCategory.DisplayMemberPath = "CategoryName";
        }

        private void AddCategory_Click(object sender, RoutedEventArgs e)
        {
            string categoryName = txtCategoryName.Text.Trim();
            if (!string.IsNullOrEmpty(categoryName))
            {
                ServiceCategory category = new ServiceCategory { CategoryName = categoryName };
                _dbContext.ServiceCategory.Add(category);
                _dbContext.SaveChanges();
                LoadCategories(); // Refresh UI
                txtCategoryName.Clear();
            }
            else
            {
                MessageBox.Show("Category name cannot be empty.");
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
                LoadCategories(); // Refresh UI
                txtSubcategoryName.Clear();
            }
            else
            {
                MessageBox.Show("Please enter a subcategory name and select a parent category.");
            }
        }

        private void DeleteCategory_Click(object sender, RoutedEventArgs e)
        {
            if (treeCategories.SelectedItem is CategoryViewModel selectedCategory)
            {
                var categoryToDelete = _dbContext.ServiceCategory.FirstOrDefault(c => c.CategoryId == selectedCategory.CategoryId);

                if (categoryToDelete != null)
                {
                    if (_dbContext.ServiceCategory.Any(c => c.CategoryParentId == categoryToDelete.CategoryId))
                    {
                        MessageBox.Show("Cannot delete a category with subcategories. Please delete subcategories first.");
                        return;
                    }

                    _dbContext.ServiceCategory.Remove(categoryToDelete);
                    _dbContext.SaveChanges();
                    LoadCategories(); // Refresh UI
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.");
            }
        }

        private void treeCategories_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            // Additional event handling if needed
        }

        public class CategoryViewModel
        {
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public List<CategoryViewModel> Subcategories { get; set; }
        }
    }
}
