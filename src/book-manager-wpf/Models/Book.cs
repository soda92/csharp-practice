using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace book_manager_wpf.Models
{
    public class Book : INotifyPropertyChanged
    {
        private int _id;
        private string _isbn = string.Empty;
        private string _name = string.Empty;
        private string _author = string.Empty;
        private int _count;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get => _id;
            set { if (_id != value) { _id = value; OnPropertyChanged(); } }
        }

        public string ISBN
        {
            get => _isbn;
            set { if (_isbn != value) { _isbn = value; OnPropertyChanged(); } }
        }

        public string Name
        {
            get => _name;
            set { if (_name != value) { _name = value; OnPropertyChanged(); } }
        }

        public string Author
        {
            get => _author;
            set { if (_author != value) { _author = value; OnPropertyChanged(); } }
        }

        public int Count
        {
            get => _count;
            set { if (_count != value) { _count = value; OnPropertyChanged(); } }
        }

        // Parameterless constructor for SQLite-net-pcl and to satisfy CS8618
        public Book()
        {
            // Backing fields are already initialized
        }
    }
}