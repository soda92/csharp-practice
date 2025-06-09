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

namespace events_async_callback
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Worker _dataFetcher;
        public MainWindow()
        {
            InitializeComponent();

            _dataFetcher = new Worker();
            _dataFetcher.DataFetched += OnDataFetched;
        }

        private void OnDataFetched(object? sender, DataFetchedEventArgs e)
        {
            // this event handler is raised from a backgroud thread by the Worker.
            // We MUST use the dispatcher to update UI elements safely.
            Dispatcher.InvokeAsync(() =>
            {
                StatusTextBlock.Text = e.HasError ? $"Error: {e.ErrorMessage}" : e.Data;
                StartOperationButton.IsEnabled = true;
            });
        }

        private void StartOperationButton_Click(object sender, RoutedEventArgs e)
        {
            StartOperationButton.IsEnabled = false;
            StatusTextBlock.Text = "Fetching data, please wait...";

            _dataFetcher.FetchDataAsync();
        }
    }
}