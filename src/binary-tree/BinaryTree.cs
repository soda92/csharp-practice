using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_tree
{
    public class BinaryTree
    {
        public Node Root { get; private set; }

        public BinaryTree()
        {
            Root = null;
        }

        // --- Insertion ---
        public void Insert(int value)
        {
            Root = InsertRecursive(Root, value);
        }

        private Node InsertRecursive(Node current, int value)
        {
            if (current == null)
            {
                return new Node(value);
            }

            if (value < current.Value)
            {
                current.Left = InsertRecursive(current.Left, value);
            }
            else if (value > current.Value)
            {
                current.Right = InsertRecursive(current.Right, value);
            }
            // else: Value already exists, do nothing or handle duplicates as needed

            return current;
        }

        // --- Search ---
        public bool Contains(int value)
        {
            return ContainsRecursive(Root, value);
        }

        private bool ContainsRecursive(Node current, int value)
        {
            if (current == null)
            {
                return false;
            }

            if (value == current.Value)
            {
                return true;
            }

            return value < current.Value
                ? ContainsRecursive(current.Left, value)
                : ContainsRecursive(current.Right, value);
        }

        // --- Deletion (More complex, but a basic example) ---
        public void Delete(int value)
        {
            Root = DeleteRecursive(Root, value);
        }

        private Node DeleteRecursive(Node current, int value)
        {
            if (current == null)
            {
                return null; // Value not found
            }

            if (value == current.Value)
            {
                // Case 1: No children or one child
                if (current.Left == null) return current.Right;
                if (current.Right == null) return current.Left;

                // Case 2: Two children (find in-order successor)
                int smallestValue = FindSmallestValue(current.Right);
                current.Value = smallestValue;
                current.Right = DeleteRecursive(current.Right, smallestValue);
                return current;
            }
            if (value < current.Value)
            {
                current.Left = DeleteRecursive(current.Left, value);
                return current;
            }

            current.Right = DeleteRecursive(current.Right, value);
            return current;
        }

        private int FindSmallestValue(Node root)
        {
            return root.Left == null ? root.Value : FindSmallestValue(root.Left);
        }


        // --- Traversals ---

        // In-order Traversal (Left, Root, Right) - Prints sorted values
        public void InOrderTraversal()
        {
            Console.Write("In-order Traversal: ");
            InOrderTraversalRecursive(Root);
            Console.WriteLine();
        }

        private void InOrderTraversalRecursive(Node node)
        {
            if (node != null)
            {
                InOrderTraversalRecursive(node.Left);
                Console.Write(node.Value + " ");
                InOrderTraversalRecursive(node.Right);
            }
        }

        // Pre-order Traversal (Root, Left, Right)
        public void PreOrderTraversal()
        {
            Console.Write("Pre-order Traversal: ");
            PreOrderTraversalRecursive(Root);
            Console.WriteLine();
        }

        private void PreOrderTraversalRecursive(Node node)
        {
            if (node != null)
            {
                Console.Write(node.Value + " ");
                PreOrderTraversalRecursive(node.Left);
                PreOrderTraversalRecursive(node.Right);
            }
        }

        // Post-order Traversal (Left, Right, Root)
        public void PostOrderTraversal()
        {
            Console.Write("Post-order Traversal: ");
            PostOrderTraversalRecursive(Root);
            Console.WriteLine();
        }

        private void PostOrderTraversalRecursive(Node node)
        {
            if (node != null)
            {
                PostOrderTraversalRecursive(node.Left);
                PostOrderTraversalRecursive(node.Right);
                Console.Write(node.Value + " ");
            }
        }

        // Level-order Traversal (Breadth-First Search)
        public void LevelOrderTraversal()
        {
            Console.Write("Level-order Traversal: ");
            if (Root == null)
            {
                Console.WriteLine();
                return;
            }

            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                Node current = queue.Dequeue();
                Console.Write(current.Value + " ");

                if (current.Left != null)
                {
                    queue.Enqueue(current.Left);
                }
                if (current.Right != null)
                {
                    queue.Enqueue(current.Right);
                }
            }
            Console.WriteLine();
        }
    }
}
