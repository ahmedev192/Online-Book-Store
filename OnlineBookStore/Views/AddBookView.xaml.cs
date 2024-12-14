using Microsoft.Win32;
using OnlineBookStore.Models;
using OnlineBookStore.Services;
using OnlineBookStore.Utilities;
using System;
using System.IO;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class AddBookView : Window
    {
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;

        public AddBookView()
        {
            InitializeComponent();
            _categoryService = new CategoryService();
            _bookService = new BookService();
            // Load categories into the ComboBox
            LoadCategories();
        }

        private void LoadCategories()
        {
            try
            {
                CategoryComboBox.ItemsSource = _categoryService.GetCategories();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BrowseImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Open a file dialog to select the image
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                CoverImagePathTextBox.Text = openFileDialog.FileName;
            }
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CategoryComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Please select a category.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Save the image using the FileHelper
                string imageFilePath = CoverImagePathTextBox.Text;
                string imageDestination = Path.Combine("Images", Path.GetFileName(imageFilePath));
                if (!Directory.Exists("Images"))
                {
                    Directory.CreateDirectory("Images");
                }


                // Create and populate the Book object
                var book = new Book
                {
                    Title = TitleTextBox.Text,
                    Author = AuthorTextBox.Text,
                    Price = decimal.Parse(PriceTextBox.Text),
                    Stock = int.Parse(StockTextBox.Text),
                    Edition = EditionTextBox.Text,
                    CoverImage = imageFilePath,
                    CategoryId = ((Category)CategoryComboBox.SelectedItem).CategoryId
                };

                // Add the book to the database
                if (_bookService.AddBook(book))
                {
                    MessageBox.Show("Book added successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to add the book.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
