using book_manager_wpf.Models;
using book_manager_wpf.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace book_manager_wpf.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        private readonly BookService _bookService;
        private ObservableCollection<Book> _books;
        private Book? _selectedBook;

        private string _inputISBN = string.Empty;
        private string _inputName = string.Empty;
        private string _inputAuthor = string.Empty;
        private string _inputCountText = string.Empty;

        public ICommand AddBookCommand { get; }
        public ICommand ModifyBookCommand { get; }
        public ICommand DeleteBookCommand { get; }
        public ICommand LoadBooksCommand { get; }

        public MainViewModel()
        {
            _bookService = new BookService();
            _books = new ObservableCollection<Book>();

            AddBookCommand = new RelayCommand(async param => await AddBookAsync(), param => CanAddBook());
            ModifyBookCommand = new RelayCommand(
                async param => await ModifyBookAsync(), param => CanModifyOrDeleteBook());
            DeleteBookCommand = new RelayCommand(async param => await DeleteBookAsync(), param => CanModifyOrDeleteBook());
            LoadBooksCommand = new RelayCommand(async param => await LoadBooksAsync());

            // Load books when ViewModel is created
            _ = LoadBooksAsync();
        }

        // actions
        private bool CanAddBook()
        {
            return !string.IsNullOrWhiteSpace(InputISBN) &&
                !string.IsNullOrWhiteSpace(InputName) &&
                !string.IsNullOrWhiteSpace(InputAuthor) &&
                int.TryParse(InputCountText, out int count) && count >= 0;
        }
        private async Task AddBookAsync()
        {
            if (!CanAddBook())
            {
                MessageBox.Show("ISBN、名字、作者不能为空；数量必须为正", "数据验证错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var book = new Book()
            {
                ISBN = InputISBN,
                Name = InputName,
                Author = InputAuthor,
                Count = int.Parse(InputCountText)
            };

            try
            {
                await _bookService.SaveBookAsync(book);
                Books.Add(book);
                ClearInputFields();
                MessageBox.Show("书籍添加成功", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"添加书籍发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task LoadBooksAsync()
        {
            try
            {
                var booksList = await _bookService.GetBooksAsync();
                Books.Clear();
                if (booksList != null)
                {
                    foreach (var book in booksList)
                    {
                        Books.Add(book);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"加载书籍发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CanModifyOrDeleteBook() => SelectedBook != null;

        private async Task ModifyBookAsync()
        {
            if (SelectedBook == null || !CanAddBook()) // Re-use CanAddBook for validation logic of fields
            {
                MessageBox.Show("请选择一个书籍并保证ISBN、名字、作者不能为空；数量必须为正", "数据验证错误", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedBook.ISBN = InputISBN;
            SelectedBook.Name = InputName;
            SelectedBook.Author = InputAuthor;
            SelectedBook.Count = int.Parse(InputCountText);

            try
            {
                await _bookService.SaveBookAsync(SelectedBook);
                // UI updates automatically due to INotifyPropertyChanged on Book and ObservableCollection
                ClearInputFields(); // Also deselects
                MessageBox.Show("书籍修改成功!", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show($"修改书籍发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async Task DeleteBookAsync()
        {
            if (SelectedBook == null) return;

            var result = MessageBox.Show($"确认删除书籍 '{SelectedBook.Name}'?", "确认删除", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _bookService.DeleteBookAsync(SelectedBook);
                    Books.Remove(SelectedBook);
                    ClearInputFields(); // Also deselects
                    MessageBox.Show("书籍删除成功!", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show($"删除书籍发生错误: {ex.Message}", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void UpdateInputFieldsFromSelectedBook()
        {
            if (SelectedBook != null)
            {
                InputISBN = SelectedBook.ISBN;
                InputName = SelectedBook.Name;
                InputAuthor = SelectedBook.Author;
                InputCountText = SelectedBook.Count.ToString();
            }
            else
            {
                ClearInputFields(false); // Don't deselect if already null
            }
        }
        private void ClearInputFields(bool deselect = true)
        {
            InputISBN = string.Empty;
            InputName = string.Empty;
            InputAuthor = string.Empty;
            InputCountText = string.Empty;
            if (deselect)
            {
                SelectedBook = null;
            }
        }

        // backing fields
        public ObservableCollection<Book> Books
        {
            get => _books;
            set
            {
                _books = value;
                OnPropertyChanged();
            }
        }

        public Book? SelectedBook
        {
            get => _selectedBook;
            set
            {
                _selectedBook = value;
                OnPropertyChanged();
                UpdateInputFieldsFromSelectedBook();
                // Notify commands that their CanExecute status might have changed
                (ModifyBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
                (DeleteBookCommand as RelayCommand)?.RaiseCanExecuteChanged();
            }
        }

        public string InputISBN
        {
            get => _inputISBN;
            set { _inputISBN = value; OnPropertyChanged(); }
        }

        public string InputName
        {
            get => _inputName;
            set { _inputName = value; OnPropertyChanged(); }
        }

        public string InputAuthor
        {
            get => _inputAuthor;
            set { _inputAuthor = value; OnPropertyChanged(); }
        }

        public string InputCountText
        {
            get => _inputCountText;
            set { _inputCountText = value; OnPropertyChanged(); }
        }
    }
}
