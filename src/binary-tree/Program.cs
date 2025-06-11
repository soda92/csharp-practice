using binary_tree;

public class Program
{
    public static void Main(string[] args)
    {
        BinaryTree tree = new BinaryTree();

        tree.Insert(50);
        tree.Insert(30);
        tree.Insert(70);
        tree.Insert(20);
        tree.Insert(40);
        tree.Insert(60);
        tree.Insert(80);

        Console.WriteLine("--- Initial Tree Traversal ---");
        tree.InOrderTraversal();   // Expected: 20 30 40 50 60 70 80
        tree.PreOrderTraversal();  // Expected: 50 30 20 40 70 60 80
        tree.PostOrderTraversal(); // Expected: 20 40 30 60 80 70 50
        tree.LevelOrderTraversal(); // Expected: 50 30 70 20 40 60 80

        Console.WriteLine("\n--- Searching ---");
        Console.WriteLine($"Contains 40: {tree.Contains(40)}"); // True
        Console.WriteLine($"Contains 90: {tree.Contains(90)}"); // False

        Console.WriteLine("\n--- Deleting 20 (Leaf Node) ---");
        tree.Delete(20);
        tree.InOrderTraversal(); // Expected: 30 40 50 60 70 80

        Console.WriteLine("\n--- Deleting 70 (Node with two children) ---");
        tree.Delete(70);
        tree.InOrderTraversal(); // Expected: 30 40 50 60 80 (70 replaced by 80, then 80 deleted)

        Console.WriteLine("\n--- Deleting 50 (Root Node) ---");
        tree.Delete(50);
        tree.InOrderTraversal(); // Expected: 30 40 60 80

        Console.WriteLine("\n--- Searching again ---");
        Console.WriteLine($"Contains 50: {tree.Contains(50)}"); // False
    }
}
