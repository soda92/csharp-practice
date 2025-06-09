using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates_events
{
    public class MainWindow
    {
        private Button myButton;

        public MainWindow()
        {
            myButton = new Button();
            myButton.Click += MyButton_Click;
            myButton.Click += AnotherClickHandler;
        }

        private void MyButton_Click(object sender, EventArgs e)
        {
            Console.WriteLine("MainWindow says: button was clicked!");
        }

        private void AnotherClickHandler(object sender, EventArgs e)
        {
            Console.WriteLine("Another handler triggered!");
        }

        public void SimulateAppRun()
        {
            myButton.SimulateClick();
        }

        public void DetachHandler()
        {
            myButton.Click -= MyButton_Click;
            Console.WriteLine("MyButton_Click handler detached.");
        }
    }
}
