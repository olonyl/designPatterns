using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Coding.Exercise;

namespace ArrayBackedProperty.DesignPattern
{
    public class Creature : IEnumerable<int>
    {
        private int[] stats = new int[3];
        private const int strenght = 0, agility = 1, intelligence = 2;
        public int Strength { get => stats[strenght]; set => stats[strenght] = value; }
        public int Agility { get => stats[agility]; set => stats[agility] = value; }
        public int Intelligence { get => stats[intelligence]; set => stats[intelligence] = value; }

        public double AverageStat => stats.Average();
        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int this[int index]
        {
            get { return stats[index]; }
            set { stats[index] = value; }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            var root = new Node<char>('a', new Node<char>('b', new Node<char>('c'), new Node<char>('d')),
                new Node<char>('e'));
            foreach (var node in root.PreOrder)
            {
                Console.WriteLine(node);
            }
        }
    }
}
namespace Coding.Exercise
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

        public Node(T value, Node<T> left, Node<T> right)
        {
            Value = value;
            Left = left;
            Right = right;

            left.Parent = right.Parent = this;
        }

        IEnumerable<T> PreOrderTraverse(Node<T> current)
        {
            yield return current.Value;
            if (current.Left != null)
                foreach (var node in PreOrderTraverse(current.Left))
                    yield return node;
            if (current.Right != null)
                foreach (var node in PreOrderTraverse(current.Right))
                    yield return node;
        }

        public IEnumerable<T> PreOrder => PreOrderTraverse(this);
    }
}