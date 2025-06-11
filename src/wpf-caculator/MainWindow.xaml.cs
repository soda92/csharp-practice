using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.Marshalling;
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

namespace wpf_caculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private int step = 0; // 0: first param, 1: second param
        private string firstParam = "";
        private string secondParam = "";
        private string _operator = "+";
        private bool isDisplayResult = false;
        private string _result = "";

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string ScreenText
        {
            set
            {
                if(isDisplayResult)
                {
                    _result = value;
                }
                else
                {
                    if (step == 0)
                    {
                        firstParam = value;
                    }
                    else
                    {
                        secondParam = value;
                    }
                }
            }
            get
            {
                if (isDisplayResult)
                {
                    return _result;
                }
                if (step == 0)
                {
                    return firstParam;
                }
                else
                {
                    return secondParam;
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = this;

            Screen.Text = "";
            firstParam = "";
            secondParam = "";
            step = 0;
            Calc.IsEnabled = false;
        }

        private void ClearInputs(object sender, RoutedEventArgs e)
        {
            Screen.Text = "";
            firstParam = "";
            secondParam = "";
            step = 0;
            Calc.IsEnabled = false;
        }

        private void CalcResult(object sender, RoutedEventArgs eventArgs)
        {
            isDisplayResult = true;
            int oprand1 = 0;
            int oprand2 = 0;
            if (firstParam != "")
            {
                try
                {
                    oprand1 = int.Parse(firstParam);
                }catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }
            if (secondParam != "")
            {
                try
                {
                    oprand2 = int.Parse(secondParam);
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            switch (_operator)
            {
                case "+":
                    {
                        ClearInputs(null, null);
                        int result = oprand1 + oprand2;
                        Screen.Text = result.ToString();
                        break;
                    }
                case "-":
                    {
                        ClearInputs(null, null);
                        int result = oprand1 - oprand2;
                        Screen.Text = result.ToString();
                        break;
                    }
                case "*":
                    {
                        ClearInputs(null, null);
                        int result = oprand1 * oprand2;
                        Screen.Text = result.ToString();
                        break;
                    }
                case "/":
                    {
                        ClearInputs(null, null);
                        decimal result = (decimal)oprand1 / (decimal)oprand2;
                        Screen.Text = result.ToString();
                        break;
                    }
            }

        }

        private void AddNumber(int number)
        {
            isDisplayResult = false;
            if (step == 0)
            {
                firstParam += number;
            }
            else
            {
                secondParam += number;
            }
            OnPropertyChanged(nameof(ScreenText));
            OnPropertyChanged(nameof(firstParam));
            OnPropertyChanged(nameof(secondParam));
        }

        private void Operator(string _operator1)
        {
            _operator = _operator1;
            step = 1;
            Calc.IsEnabled = true;
        }

        private void Number_Click(object sender, RoutedEventArgs eventArgs)
        {
            var button = sender as Button;
            if (button == null) return;

            int number = int.Parse(button.Content.ToString()!);

            AddNumber(number);
        }
        
        private void Operator_Click(object sender, RoutedEventArgs eventArgs)
        {
            var button = sender as Button;
            if (button == null) return;
            Operator(button.Content.ToString()!);
        }
    }
}