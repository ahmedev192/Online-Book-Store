using OnlineBookStore.Models;
using OnlineBookStore.Services;
using OnlineBookStore.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OnlineBookStore.Views
{
    public partial class BooksView : Window
    {
        private readonly BookService _bookService;
        private readonly CartService _cartService;
        private int _customerId = UserSession.Instance.UserId;

        public BooksView()
        {
            InitializeComponent();
            _bookService = new BookService();
            _cartService = new CartService();
            LoadBooks();
        }

        // Load books from the database
        private void LoadBooks(List<Book> books = null)
        {
            books = books ?? _bookService.BrowseBooks();
            BooksListView.ItemsSource = books;
        }

        // Apply Filters, Search, and Sort Together
        private void ApplyFiltersButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = SearchTextBox.Text;
            string selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string selectedSortOption = (SortComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();

            ISortingStrategy sortingStrategy = selectedSortOption switch
            {
                "Sort by Price" => new SortByPrice(),
                "Sort by Popularity" => new SortByPopularity(),
                "Sort by Title" => new SortByTitle(),
                _ => null
            };

            if (selectedCategory == "All Categories")
                selectedCategory = null;

            // Use composite-based service method
            var results = _bookService.SearchFilterAndSortBooks(searchQuery, selectedCategory, sortingStrategy);
            LoadBooks(results);
        }

        // Add to Cart Button Click Event
        private void AddToCartButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var book = button?.DataContext as Book;

            if (book != null)
            {
                var addToCartCommand = new AddToCartCommand(_cartService, _customerId, book);
                addToCartCommand.Execute(); 

                MessageBox.Show($"{book.Title} has been added to your cart.");
            }

        }
        private void ViewDetailsButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected book from the list view
            var button = sender as Button;
            var book = button?.DataContext as Book;

            if (book != null)
            {
                // Open the BookDetailsView window for the selected book
                var bookDetailsView = new BookDetailsView(book.BookId);
                bookDetailsView.ShowDialog();
            }
        }
        // Go to Cart Button Click Event
        private void GoToCartButton_Click(object sender, RoutedEventArgs e)
        {
            // Navigate to Cart View
            var cartView = new CartView(); // Pass the customerId to CartView
            cartView.Show();
            this.Close(); // Close the current view
        }
    }
}
