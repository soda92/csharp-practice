using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        int[] arr1 = { 5, 1, 3 };
        int[] arr2 = { 66, 44, 2, 11, 10 };
        int[] arr3 = { 1, 2, 3, 5, 20, 10, 5 };

        var combinations = from n1 in arr1
                           from n2 in arr2
                           from n3 in arr3
                           select new List<int> { n1, n2, n3 };

        Console.WriteLine("\nCombinations using LINQ SelectMany:");
        foreach (var combo in combinations)
        {
            Console.WriteLine($"[{string.Join(", ", combo)}]");
        }
    }
}