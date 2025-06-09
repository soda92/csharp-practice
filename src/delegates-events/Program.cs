using delegates_events;

public delegate void MyDelegate(string message);

public class Program
{
    public static void ShowMessage(string msg)
    {
        Console.WriteLine($"Message: {msg}");
    }

    public static void ShowMessage2(string msg)
    {
        Console.WriteLine($"Message2: {msg}");
    }
    public static void Main(string[] args)
    {
        MyDelegate handler = ShowMessage;
        handler += ShowMessage2;
        handler("Hello from delegate!");

        MainWindow app = new MainWindow();
        app.SimulateAppRun();
        app.DetachHandler();
        app.SimulateAppRun();

        DataProcessor dp = new DataProcessor();
        DataConsumer dc = new DataConsumer(dp);

        dp.Process("hello world");
    }
}