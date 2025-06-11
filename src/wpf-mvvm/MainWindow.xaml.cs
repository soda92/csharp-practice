using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using wpf_mvvm.Models;
using wpf_mvvm.ViewModels;

namespace wpf_mvvm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User user;
        public MainWindow()
        {
            InitializeComponent();
            user = new User { Id = 1 , FirstName="Jane", LastName="Doe"};
            this.DataContext = new UserViewModel(user);
        }
    }
}