using System;
using System.Collections.Generic;

namespace JsonParser
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            BinaryTree bt = new BinaryTree(31);
            for(int i = 0;i<55;i++){
                bt.AddNode(rand.Next(100));
            }
            bt.Display();
            BinaryTree.Node node = bt.Search(44);
            System.Console.WriteLine(node!=null?"Found":"Not found");
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
        public void AddNode(int value)   
        {
            Node temp = root;
            while(true){
                if(temp.value < value){
                    if(temp.right == null){
                        temp.right = new Node(value);
                        //System.Console.WriteLine($"right of {temp.value} : {value}");
                        return;
                    }
                    temp = temp.right;
                }
                else {
                    if(temp.left == null){
                        temp.left = new Node(value);
                        //System.Console.WriteLine($"left of {temp.value} : {value}");
                        return;
                    }
                    temp = temp.left;
                }
            }
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
        #nullable enable
        public Node? Search(int value){
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while(queue.Count > 0){
                Node temp = queue.Dequeue();
                if(temp.value == value)return temp;
                if(temp.left != null)queue.Enqueue(temp.left);
                if(temp.right != null)queue.Enqueue(temp.right);
            }
            return null;
        }
        public override string ToString(){  //TODO show binary tree as actual tree
            return base.ToString();
        }

    }
}
