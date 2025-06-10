using book_manager_wpf.Models;
using book_manager_wpf.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace book_manager_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window // Removed IDisposable as it's not fully implemented and BookService manages its own resources
    {
        private BookService _bookService;
        private ObservableCollection<Book> _books;

        public MainWindow()
        {
            InitializeComponent();
            _bookService = new BookService();
            _books = new ObservableCollection<Book>();
            dgBooks.ItemsSource = _books;
            dgBooks.SelectionChanged += DgBooks_SelectionChanged;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                await LoadBooks();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading books: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadBooks()
        {
            var books = await _bookService.GetBooksAsync();
            _books.Clear();
            if (books != null)
            {
                foreach (var book in books)
                {
                    _books.Add(book);
                }
            }
        }

        private void DgBooks_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (dgBooks.SelectedItem is Book selectedBook)
            {
                txtISBN.Text = selectedBook.ISBN;
                txtName.Text = selectedBook.Name;
                txtAuthor.Text = selectedBook.Author;
                txtCount.Text = selectedBook.Count.ToString();
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtISBN.Text) || string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtAuthor.Text) || !int.TryParse(txtCount.Text, out int count) || count < 0)
                {
                    MessageBox.Show("Please fill in all fields with valid values. Count must be a non-negative number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var book = new Book
                {
                    ISBN = txtISBN.Text,
                    Name = txtName.Text,
                    Author = txtAuthor.Text,
                    Count = count
                };

                await _bookService.SaveBookAsync(book);
                // The 'book' object will have its Id updated by SaveBookAsync (via SQLite-net-pcl)
                _books.Add(book); // Add the new book directly to the ObservableCollection
                ClearInputFields();
                MessageBox.Show("Book added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error adding book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedBook = dgBooks.SelectedItem as Book;
                if (selectedBook == null)
                {
                    MessageBox.Show("Please select a book to modify.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtISBN.Text) || string.IsNullOrWhiteSpace(txtName.Text) ||
                    string.IsNullOrWhiteSpace(txtAuthor.Text) || !int.TryParse(txtCount.Text, out int count) || count < 0)
                {
                    MessageBox.Show("Please ensure all fields for the selected book are filled with valid values. Count must be a non-negative number.", "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                selectedBook.ISBN = txtISBN.Text;
                selectedBook.Name = txtName.Text;
                selectedBook.Author = txtAuthor.Text;
                selectedBook.Count = count;

                await _bookService.SaveBookAsync(selectedBook);
                // With INotifyPropertyChanged implemented on Book, the DataGrid will update automatically
                // when properties of 'selectedBook' were changed above.
                // No need to remove and re-insert the item or refresh the whole grid.

                ClearInputFields();
                MessageBox.Show("Book modified successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error modifying book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedBook = dgBooks.SelectedItem as Book;
                if (selectedBook != null)
                {
                    var result = MessageBox.Show($"Are you sure you want to delete '{selectedBook.Name}'?", "Confirm Delete", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        await _bookService.DeleteBookAsync(selectedBook);
                        _books.Remove(selectedBook);
                        ClearInputFields();
                        MessageBox.Show("Book deleted successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Please select a book to delete.", "Selection Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error deleting book: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void ClearInputFields()
        {
            txtISBN.Clear(); txtName.Clear(); txtAuthor.Clear(); txtCount.Clear();
            dgBooks.SelectedItem = null;
        }
    }
 }