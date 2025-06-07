using book_manager_wpf.Models;
using book_manager_wpf.Services;
using System.Collections.ObjectModel;
using System.Windows;
namespace book_manager_wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IDisposable
    {
        private BookService _bookService;
        private ObservableCollection<Book> _books;

        public MainWindow()
        {
            InitializeComponent();
            _bookService = new BookService();
            _books = new ObservableCollection<Book>();
            dgBooks.ItemsSource = _books;
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await LoadBooks();
        }

        private async Task LoadBooks()
        {
            var books = await _bookService.GetBooksAsync();
            _books.Clear();
            foreach (var book in books)
            {
                _books.Add(book);
            }
        }

        private async void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtISBN.Text) || string.IsNullOrWhiteSpace(txtName.Text) ||
                string.IsNullOrWhiteSpace(txtAuthor.Text) || !int.TryParse(txtCount.Text, out int count))
            {
                MessageBox.Show("Please fill in all fields with valid values.");
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
            _books.Add(book);
            ClearInputFields();
        }

        private async void ModifyButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = dgBooks.SelectedItem as Book;
            if (selectedBook == null)
            {
                MessageBox.Show("Please select a book to modify.");
                return;
            }

            if (int.TryParse(txtCount.Text, out int count))
            {
                selectedBook.ISBN = txtISBN.Text;
                selectedBook.Name = txtName.Text;
                selectedBook.Author = txtAuthor.Text;
                selectedBook.Count = count;

                await _bookService.SaveBookAsync(selectedBook);
                dgBooks.Items.Refresh();
                ClearInputFields();
            }
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedBook = dgBooks.SelectedItem as Book;
            if (selectedBook != null)
            {
                await _bookService.DeleteBookAsync(selectedBook);
                _books.Remove(selectedBook);
            }
        }
        private void ClearInputFields()
        {
            txtISBN.Clear(); txtName.Clear(); txtAuthor.Clear(); txtCount.Clear();
        }
    }
 }