using OnlineBookStore.Models;
using OnlineBookStore.Services;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class CategoryManagementView : Window
    {
        private readonly CategoryService _categoryService;

        public CategoryManagementView()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            LoadCategories();
        }

        private void LoadCategories()
        {
            CategoriesDataGrid.ItemsSource = _categoryService.GetCategories();
        }

        private void AddCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }



            if (_categoryService.AddCategory(name))
            {
                MessageBox.Show($"Category '{name}' added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadCategories();
                ClearInputFields();
            }
        }

        private void EditCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesDataGrid.SelectedItem is Category selectedCategory)
            {
                string newName = NameTextBox.Text.Trim();

                if (string.IsNullOrEmpty(newName))
                {
                    MessageBox.Show("Name cannot be empty.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (_categoryService.EditCategory(selectedCategory.CategoryId, newName))
                {
                    MessageBox.Show($"Category '{selectedCategory.Name}' updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadCategories();
                    ClearInputFields();
                }
            }
            else
            {
                MessageBox.Show("Please select a category to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoriesDataGrid.SelectedItem is Category selectedCategory)
            {
                if (MessageBox.Show($"Are you sure you want to delete '{selectedCategory.Name}'?", "Confirm Delete",
                                    MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    if (_categoryService.DeleteCategory(selectedCategory.CategoryId))
                    {
                        MessageBox.Show($"Category '{selectedCategory.Name}' deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                        LoadCategories();
                        ClearInputFields();
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a category to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void ClearInputFields()
        {
            NameTextBox.Text = string.Empty;
        }
    }
}
