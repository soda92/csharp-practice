using System;
using System.Threading.Tasks;

namespace events_async_callback
{
    /// <summary>
    /// Custom event arguments to carry data from the background operation.
    /// </summary>
    public class DataFetchedEventArgs : EventArgs
    {
        public string Data { get; }
        public bool HasError { get; }
        public string ErrorMessage { get; }

        public DataFetchedEventArgs(string data)
        {
            Data = data;
            HasError = false;
        }

        public DataFetchedEventArgs(string errorMessage, bool isError)
        {
            ErrorMessage = errorMessage;
            HasError = isError;
        }
    }

    /// <summary>
    /// Simulates a worker that performs a long-running operation.
    /// </summary>
    public class Worker
    {
        public event EventHandler<DataFetchedEventArgs> DataFetched;

        public void FetchDataAsync()
        {
            Task.Run(async () =>
            {
                await Task.Delay(3000); // Simulate 3 seconds of work (e.g., network call)
                DataFetched?.Invoke(this, new DataFetchedEventArgs("Data successfully fetched from the background!"));
            });
        }
    }
}