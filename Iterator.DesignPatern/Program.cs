﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

namespace Iterator.DesignPatern
{
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
        public readonly Node<T> root;
        public Node<T> Current;
        private bool yiedldesStart;

        public InOrderIterator(Node<T> root)
        {
            this.root = root;
            Current = root;

            while (Current.Left != null)
            {
                Current = Current.Left;
            }
        }

        public bool MoveNext()
        {
            if (!yiedldesStart)
            {
                yiedldesStart = true;
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

        public void Reset() 
        {
            Current = root;
            yiedldesStart = false;
        }

    }

    public class BinaryTree<T>
    {
        private Node<T> root;

        public BinaryTree(Node<T> root)
        {
            this.root = root;
        }

        public IEnumerable<Node<T>> InOrder
        {
            get
            {
                IEnumerable<Node<T>> Traverse(Node<T> current)
                {
                    if(current.Left !=  null)
                        foreach (var left in Traverse(current.Left))
                            yield return left;
                    yield return current;
                    if (current.Right != null)
                        foreach (var right in Traverse(current.Right))
                            yield return right;
                }

                foreach (var node in Traverse(root))
                    yield return node;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //        1
            //       / \
            //      /   \
            //     /     \
            //    2       3
            //   / \     / \
            // 10   14  5   8
            // in-order:  10,2,14,1,5,3,8
            // pre-order: 123

            var root = new Node<int>(1, 
                new Node<int>(2, new Node<int>(10), new Node<int>(14)), 
                new Node<int>(3, new Node<int>(5), new Node<int>(8)));

            var it = new InOrderIterator<int>(root);
            while (it.MoveNext())
            {
                Console.Write(it.Current.Value);
                Console.Write(',');
            }
            Console.WriteLine();

            var tree = new BinaryTree<int>(root);
            Console.WriteLine(string.Join(",", tree.InOrder.Select(x=> x.Value)));
        }
    }
}
