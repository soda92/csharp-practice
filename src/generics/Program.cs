Box<int> intBox = new Box<int>(123);
intBox.DisplayContent();

int x = 10, y = 20;
Console.WriteLine($"Before swap: x={x}, y={y}");
Utilities.Swap(ref x, ref y);
Console.WriteLine($"After swap: x={x}, y={y}");

public class Box<T>
{
    public T Content { get; set; }
    public Box(T content)
    {
        Content = content;
    }

    public void DisplayContent()
    {
        Console.WriteLine($"The content of the box is: {Content}");
    }
}

public class Utilities
{
    public static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }

    public static void PrintArray<T>(T[] array)
    {
        foreach(T item in array)
        {
            Console.Write($"{item} ");
        }
        Console.WriteLine();
    }    
}