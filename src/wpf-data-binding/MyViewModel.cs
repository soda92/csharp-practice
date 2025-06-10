using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace wpf_data_binding
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class MyViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set
            {
                if (_userName != value)
                {
                    _userName = value;
                    OnPropertyChanged();
                }
            }
        }

        private int _age;
        public int Age
        {
            get { return _age; }
            set
            {
                if (_age != value)
                {
                    _age = value;
                    OnPropertyChanged();
                }
            }
        }

        private bool _isLoggedIn;
        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    OnPropertyChanged();
                }
            }
        }

        private double _volume;
        public double Volume
        {
            get { return _volume; }
            set
            {
                if (_volume != value)
                {
                    _volume = value;
                    OnPropertyChanged();
                }
            }
        }

        private ObservableCollection<Category> _availableCategories;
        public ObservableCollection<Category> AvailableCategories
        {
            get { return _availableCategories; }
            set
            {
                _availableCategories = value;
                OnPropertyChanged();
            }
        }

        private int _selectedCategoryId;
        public int SelectedCategoryId
        {
            get { return _selectedCategoryId; }
            set
            {
                if (_selectedCategoryId != value)
                {
                    _selectedCategoryId = value;
                    OnPropertyChanged();
                }
            }
        }

        public MyViewModel()
        {
            UserName = "John Doe";
            Age = 30;
            IsLoggedIn = false;
            Volume = 50;

            AvailableCategories = new ObservableCollection<Category>
            {
                new Category {Id =1, Name = "Electronics"},
                new Category {Id =2, Name = "Books"},
                new Category {Id = 3, Name = "Clothing"},
            };

            SelectedCategoryId = 2; // Pre-select "Books"
        }
    }
}
