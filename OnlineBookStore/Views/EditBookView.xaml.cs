using OnlineBookStore.Models;
using OnlineBookStore.Services;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class EditBookView : Window
    {
        private readonly BookService _bookService;
        private readonly CategoryService _categoryService;

        private readonly Book _book;

        public EditBookView(Book book)
        {
            InitializeComponent();
            _bookService = new BookService();
            _book = book;
            _categoryService = new CategoryService();
            LoadBookDetails();
            LoadCategories();
        }

        private void LoadBookDetails()
        {
            TitleTextBox.Text = _book.Title;
            AuthorTextBox.Text = _book.Author;
            PriceTextBox.Text = _book.Price.ToString();
            StockTextBox.Text = _book.Stock.ToString();
            EditionTextBox.Text = _book.Edition;
            CoverImageTextBox.Text = _book.CoverImage;
        }

        private void LoadCategories()
        {
            try
            {
                // Fetch categories from the service
                var categories = _categoryService.GetCategories();
                CategoryComboBox.ItemsSource = categories;
                CategoryComboBox.DisplayMemberPath = "Name"; // Assuming Category has a Name property
                CategoryComboBox.SelectedItem = _book.Category; // Select the current category of the book
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading categories: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Get selected category from the ComboBox
                var selectedCategory = (Category)CategoryComboBox.SelectedItem;

                if (_bookService.EditBook(_book.BookId, TitleTextBox.Text, AuthorTextBox.Text,
                    decimal.Parse(PriceTextBox.Text), int.Parse(StockTextBox.Text), EditionTextBox.Text, CoverImageTextBox.Text))
                {
                    MessageBox.Show("Book updated successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Failed to update the book.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UploadImageButton_Click(object sender, RoutedEventArgs e)
        {
            // Open file dialog to select an image file
            var fileDialog = new Microsoft.Win32.OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp"
            };

            if (fileDialog.ShowDialog() == true)
            {
                // Save the selected image to a specific location
                var imageBytes = System.IO.File.ReadAllBytes(fileDialog.FileName);
                var savedPath = FileHelper.SaveImage( imageBytes);

                // Update the text box with the image path
                if (!string.IsNullOrEmpty(savedPath))
                {
                    CoverImageTextBox.Text = savedPath;
                }
                else
                {
                    MessageBox.Show("Error uploading the image.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
