using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace delegates_events
{
    public class CustomEventArgs: EventArgs
    {
        public string Data { get; }
        public CustomEventArgs(string data)
        {
            Data = data;
        }
    }

    public class DataProcessor
    {
        public event EventHandler<CustomEventArgs> DataProcessed;

        public void Process(string input)
        {
            Console.WriteLine($"Processing: {input}");
            // Simulate some processing
            string result = input.ToUpper();

            OnDataProcessed(new CustomEventArgs(result));
        }

        protected virtual void OnDataProcessed(CustomEventArgs e)
        {
            DataProcessed?.Invoke(this, e);
        }
    }

    public class DataConsumer
    {
        public DataConsumer(DataProcessor processor)
        {
            processor.DataProcessed += Processor_DataProcessed;
        }

        private void Processor_DataProcessed(object sender, CustomEventArgs e)
        {
            Console.WriteLine($"Consumer received processed data: {e.Data}");
        }
    }
}
