﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace binary_tree
{
    public class Node
    {
        public int Value { get; set; } // Or use a generic type for more flexibility
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int value)
        {
            Value = value;
            Left = null;
            Right = null;
        }
    }

}
