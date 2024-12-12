using OnlineBookStore.Services;
using OnlineBookStore.Utilities;
using OnlineBookStore.ViewModels;
using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;

namespace OnlineBookStore.Views
{
    public partial class BookDetailsView : Window
    {
        private readonly BookDetailsViewModel _viewModel;

        public BookDetailsView(int bookId)
        {
            InitializeComponent();

            _viewModel = new BookDetailsViewModel();
            _viewModel.LoadBookDetails(bookId);

            DataContext = _viewModel;
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Update UI with the book details and reviews
            byte[] imageBytes = FileHelper.LoadImage(_viewModel.Book.CoverImage);

            // Convert byte[] to ImageSource
            ImageSource imageSource = ConvertByteArrayToImageSource(imageBytes);

            // Set the ImageSource to the Image control
            BookImage.Source = imageSource;
            BookTitle.Text = _viewModel.Book.Title;
            BookAuthor.Text = _viewModel.Book.Author;

            ReviewsList.ItemsSource = _viewModel.Reviews;
            ReviewInputPanel.Visibility = _viewModel.CanLeaveReview ? Visibility.Visible : Visibility.Collapsed;
        }

        private void SubmitReview_Click(object sender, RoutedEventArgs e)
        {
            var reviewText = ReviewTextBox.Text;

            // Get selected rating
            var selectedRating = (RatingComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            int rating = int.TryParse(selectedRating, out int parsedRating) ? parsedRating : 0;

            if (rating < 1 || rating > 5)
            {
                MessageBox.Show("Please select a rating between 1 and 5.", "Invalid Rating", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _viewModel.SubmitReview(_viewModel.Book.BookId, reviewText, rating);

            // Refresh reviews after submitting
            _viewModel.LoadBookDetails(_viewModel.Book.BookId); // Re-load the reviews
            UpdateUI();

            MessageBox.Show("Review submitted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private ImageSource ConvertByteArrayToImageSource(byte[] byteArray)
        {
            if (byteArray == null || byteArray.Length == 0)
                return null;

            using (var stream = new MemoryStream(byteArray))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                return image;
            }
        }
    }
}
