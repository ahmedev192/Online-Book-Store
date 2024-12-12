using OnlineBookStore.Models;
using OnlineBookStore.Services;
using System.Windows;

namespace OnlineBookStore.Views
{
    public partial class ManageBooksView : Window
    {
        private readonly BookService _bookService;

        public ManageBooksView()
        {
            InitializeComponent();
            _bookService = new BookService();
            LoadBooks();
        }

        private void LoadBooks()
        {
            var books = _bookService.BrowseBooks();
            BooksDataGrid.ItemsSource = books;
        }

        private void AddBook_Click(object sender, RoutedEventArgs e)
        {
            var addBookView = new AddBookView();
            addBookView.ShowDialog();
            LoadBooks();
        }

        private void EditBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                var editBookView = new EditBookView(selectedBook);
                editBookView.ShowDialog();
                LoadBooks();
            }
            else
            {
                MessageBox.Show("Please select a book to edit.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DeleteBook_Click(object sender, RoutedEventArgs e)
        {
            if (BooksDataGrid.SelectedItem is Book selectedBook)
            {
                if (_bookService.DeleteBook(selectedBook.BookId))
                {
                    MessageBox.Show($"Book '{selectedBook.Title}' deleted successfully.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadBooks();
                }
                else
                {
                    MessageBox.Show("Failed to delete the book.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select a book to delete.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
