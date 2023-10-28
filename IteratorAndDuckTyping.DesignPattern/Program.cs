using System;
using System.Collections;
using System.Collections.Generic;

namespace IteratorAndDuckTyping.DesignPattern
{
    public class BinaryTree<T>
    {
        private Node<T> root;

        public BinaryTree(Node<T> root)
        {
            this.root = root;
        }
        public InOrderIterator<T> GetEnumerator()
        {
            return new InOrderIterator<T>(root);
        }
        }

    public class Node<T>
    {
        public T Value;
        public Node<T> Left, Right;
        public Node<T> Parent;

        public Node(T value)
        {
            Value = value;
        }

        public Node(T value, Node<T> left, Node<T> right) : this(value)
        {
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }
    }

    public class InOrderIterator<T>
    {

        private readonly Node<T> root;
        public Node<T> Current { get; set; }
        private bool yieldedStart;

        public InOrderIterator(Node<T> root)
        {
            this.root = root;
            Current = root;
            while (Current.Left != null)
                Current = Current.Left;
        }

        public bool MoveNext()
        {
            if (!yieldedStart)
            {
                yieldedStart = true;
                return true;
            }

            if (Current.Right != null)
            {
                Current = Current.Right;
                while (Current.Left != null)
                    Current = Current.Left;
                return true;
            }
            else
            {
                var parent = Current.Parent;
                while (parent != null && Current == parent.Right)
                {
                    Current = parent;
                    parent = parent.Parent;
                }

                Current = parent;
                return Current != null;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var root = new Node<int>(1, new Node<int>(2), new Node<int>(3));

            var tree = new BinaryTree<int>(root);
            foreach (var node in tree)
                Console.WriteLine(node.Value);
        }
    }
}
