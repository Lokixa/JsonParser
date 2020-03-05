using System;

namespace JsonParser
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree bt = new BinaryTree(31);
            bt.AddNode(55);
            bt.AddNode(44);
            bt.AddNode(30);
            bt.Display();
        }
    }
    class BinaryTree
    {
        public class Node
        {
            public int value;
            public Node left, right;
            public Node(int value) => this.value = value;
        }
        public Node root;
        public BinaryTree(int value)
        {
            root = new Node(value);
        }
        public void AddNode(int value)  //TODO Fix algorithm 
        {
            Node temp = root;
            int index = 0;
            while (temp.left != null && temp.right != null)
            {
                if (value > temp.value)
                {
                    temp = temp.right;
                }
                else if (value < temp.value)
                {
                    temp = temp.left;
                }
                else return;
                index++;
            }
            if (temp.left == null && temp.value > value)
            {
                temp.left = new Node(value);
                Console.WriteLine(value+" left of "+temp.value);
            }
            else if(temp.right == null && temp.value < value)
            {
                temp.right = new Node(value);
                Console.WriteLine(value + " right of "+temp.value);
            }
            else Console.WriteLine($"{value} got left out" 
            + $"+ {temp.value}:{temp.left?.value}:{temp.right?.value} at {index}");
        }
        public void Display()
        {
            Display(root);
        }
        void Display(Node temp)
        {
            if (temp == null) return;
            Display(temp.left);
            Console.WriteLine(temp.value);
            Display(temp.right);
        }

    }
}
