using OnlineBookStore.Models;
using OnlineBookStore.Services;
using OnlineBookStore.Utilities;
using OnlineBookStore.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace OnlineBookStore.Views
{
    public partial class CartView : Window
    {
        private readonly CartService _cartService;
        private ObservableCollection<CartItemViewModel> _cartItems;

        public CartView()
        {
            InitializeComponent();
            _cartService = new CartService();
            LoadCart();
        }

        // Load Cart Items
        private void LoadCart()
        {
            int userId = UserSession.Instance.UserId;
            var cart = _cartService.GetCartByCustomerId(userId);

            if (cart != null && cart.Books != null)
            {
                _cartItems = new ObservableCollection<CartItemViewModel>(
                    cart.Books.GroupBy(b => b.BookId).Select(g => new CartItemViewModel
                    {
                        BookId = g.Key,
                        Title = g.First().Title,
                        Author = g.First().Author,
                        Price = g.First().Price,
                        CoverImage = g.First().CoverImage, // Include cover image
                        Quantity = g.Count(),
                        TotalPrice = g.Sum(b => b.Price)
                    })
                );
                CartItemsList.ItemsSource = _cartItems;
                UpdateTotalPrice();
            }
        }

        // Update Total Price
        private void UpdateTotalPrice()
        {
            var totalPrice = _cartItems.Sum(item => item.TotalPrice);
            TotalPriceText.Text = $"${totalPrice:F2}";
        }

        // Increase Quantity
        private void IncreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var cartItem = (sender as FrameworkElement).DataContext as CartItemViewModel;
            if (cartItem != null)
            {
                var book = _cartService.GetCartByCustomerId(UserSession.Instance.UserId)?.Books.FirstOrDefault(b => b.BookId == cartItem.BookId);

                if (book != null)
                {
                    var addToCartCommand = new AddToCartCommand(_cartService, UserSession.Instance.UserId, book);
                    addToCartCommand.Execute(); 
                    cartItem.Quantity++;
                    cartItem.TotalPrice += cartItem.Price;
                    CartItemsList.Items.Refresh(); // Refresh ListView
                    UpdateTotalPrice();
                }
            }
        }

        // Decrease Quantity
        private void DecreaseQuantity_Click(object sender, RoutedEventArgs e)
        {
            var cartItem = (sender as FrameworkElement).DataContext as CartItemViewModel;
            if (cartItem != null && cartItem.Quantity > 1)
            {
                var book = _cartService.GetCartByCustomerId(UserSession.Instance.UserId)?.Books.FirstOrDefault(b => b.BookId == cartItem.BookId);

                if (book != null)
                {
                    var removeFromCartCommand = new RemoveFromCartCommand(_cartService, UserSession.Instance.UserId, book);
                    removeFromCartCommand.Execute(); // Execute the command to remove the book from the cart

                    cartItem.Quantity--;
                    cartItem.TotalPrice -= cartItem.Price;
                    CartItemsList.Items.Refresh(); // Refresh ListView
                    UpdateTotalPrice();
                }
            }
        }

        // Remove Book
        private void RemoveBook_Click(object sender, RoutedEventArgs e)
        {
            var cartItem = (sender as FrameworkElement).DataContext as CartItemViewModel;
            if (cartItem != null)
            {
                var booksToRemove = _cartService.GetCartByCustomerId(UserSession.Instance.UserId)?.Books
                    .Where(b => b.BookId == cartItem.BookId)
                    .ToList();

                if (booksToRemove != null)
                {
                    foreach (var book in booksToRemove)
                    {
                    var removeFromCartCommand = new RemoveFromCartCommand(_cartService, UserSession.Instance.UserId, book);
                    removeFromCartCommand.Execute(); // Execute the command to remove the book from the cart
                    }

                    _cartItems.Remove(cartItem);
                    UpdateTotalPrice();
                }
            }
        }

        // Place Order
        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            // Check if cart has items
            if (!_cartItems.Any())
            {
                MessageBox.Show("Your cart is empty. Add some items before placing an order.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Redirect to OrderConfirmationView
            var orderConfirmationView = new OrderConfirmationView(UserSession.Instance.UserId);
            this.Close();

            orderConfirmationView.ShowDialog(); // Open the order confirmation modally

        }

    }
}
