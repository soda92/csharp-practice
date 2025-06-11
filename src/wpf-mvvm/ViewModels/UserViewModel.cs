using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using wpf_mvvm.Models;

namespace wpf_mvvm.ViewModels
{
    public class UserViewModel : INotifyPropertyChanged
    {
        // Boilerplate for INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        // --- Model Data (exposed via properties) ---
        private User _user;
        public UserViewModel(User user)
        {
            _user = user;
            // Initialize commands
            SaveUserCommand = new RelayCommand(SaveUser, CanSaveUser);
        }

        public string FirstName
        {
            get { return _user.FirstName; }
            set
            {
                if (_user.FirstName != value)
                {
                    _user.FirstName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FullName)); // Notify that FullName also changed
                    (SaveUserCommand as RelayCommand)?.RaiseCanExecuteChanged(); // Re-evaluate CanExecute
                }
            }
        }

        public string LastName
        {
            get { return _user.LastName; }
            set
            {
                if (_user.LastName != value)
                {
                    _user.LastName = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FullName)); // Notify that FullName also changed
                    (SaveUserCommand as RelayCommand)?.RaiseCanExecuteChanged(); // Re-evaluate CanExecute
                }
            }
        }

        public string FullName => _user.FullName; // Read-only property from Model

        // --- Commands (for actions/behavior) ---
        public ICommand SaveUserCommand { get; private set; }
        private void SaveUser(object parameter)
        {
            // This is where you'd call your business logic/service to save the user
            // e.g., UserService.SaveUser(_user);
            System.Windows.MessageBox.Show($"Saving user: {FullName}");
        }
        private bool CanSaveUser(object parameter)
        {
            // Example validation: allow saving only if first name is not empty
            return !string.IsNullOrWhiteSpace(FirstName);
        }

        // --- Other ViewModel properties/logic ---
        private bool _isLoading;
        public bool IsLoading
        {
            get { return _isLoading; }
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }
    }
}
