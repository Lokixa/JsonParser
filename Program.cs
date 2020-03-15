using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace JsonParser
{
    class Program
    {
        static Random rand = new Random();
        static void Main(string[] args)
        {
            #region ActualAppStuff
            // string sampleJson =
            //          "[{\"property\":\"value\",\"anotherOne\":74103}]";
            // List<JsonToken> jsonFile = new List<JsonToken>();
            // for (int i = 0; i < sampleJson.Length; i++)
            // {
            //     switch (sampleJson[i])
            //     {
            //         case '{':
            //             ParseJson(sampleJson, ref i);   //Should return on '}'
            //             break;
            //     }
            // }
            #endregion
            BinaryTree bt = new BinaryTree(1);
            int[] nodes = new int[]{
                0,2,-2,-1,3
            };
            for (int i = 0; i < nodes.Length; i++) bt.AddNode(nodes[i]);
            bt.Display();
        }
    }
    public class JsonToken
    {
        public string id;
        public object value;
        public JsonToken(object value, string id = "")
        {
            this.id = id;
            this.value = value;
        }
    }
    public class Node
    {
        public int value;
        public Node left,right;
        public Node(int value) => this.value = value;
    }
    class BinaryTree
    {
        public Node root;
        public BinaryTree(int value)
        {
            root = new Node(value);
        }
        public void AddNode(int value)
        {
            Node temp = root;
            while (true)
            {
                if (temp.value < value)
                {
                    //Go right
                    if (temp.right == null)
                    {
                        temp.right = new Node(value);
                        return;
                    }
                    temp = temp.right;
                }
                else
                {
                    //Go left
                    if (temp.left == null)
                    {
                        temp.left = new Node(value);
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
        public Node? BFS(int value)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                Node temp = queue.Dequeue();
                if (temp.value == value) return temp;
                if(temp.left != null)queue.Enqueue(temp.left);
                if(temp.right != null)queue.Enqueue(temp.right);
            }
            return null;
        }
    }
}