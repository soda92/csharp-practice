using book_manager_wpf.ViewModels;
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

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }
    }
 }