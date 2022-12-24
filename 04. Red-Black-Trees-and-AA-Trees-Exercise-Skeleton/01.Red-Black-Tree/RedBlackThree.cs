namespace _01.Red_Black_Tree
{
    using System;
    using System.Collections.Generic;

    public class RedBlackTree<T> 
        : IBinarySearchTree<T> where T : IComparable
    {
        private const bool Black = false;
        private const bool Red = true;
        private Node root;

        public RedBlackTree()
        {
        }

        public int Count => root == null ? 0 : root.Count; 

        private int NodeCount(Node root)
        {
            if (root == null)
            {
                return 0;
            }

            var nodeValue = IsRed(root) ? 0 : 1;
            return nodeValue + NodeCount(root.Left) + NodeCount(root.Right);
        }

        public void Insert(T element)
        {
            root = Insert(element, root);
            root.Color = Black;
        }

        private Node Insert(T element, Node node)
        {
            if (node == null)
            {
                node = new Node(element);
            }
            else if (element.CompareTo(node.Value) < 0)
            {
                node.Left = Insert(element, node.Left);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                node.Right = Insert(element, node.Right);
            }

            node = BalanceTree(node);
            node.Count = NodeCount(node);  

            return node;
        }

        private Node BalanceTree(Node node)
        {
            if (IsRed(node.Right) && !IsRed(node.Left))
            {
                node = RotateLeft(node);
            }

            if (IsRed(node.Left) && IsRed(node.Left.Left))
            {
                node = RotateRight(node);
            }

            if (IsRed(node.Left) && IsRed(node.Right))
            {
                SwapColors(node);
            }

            return node;
        }

        private void SwapColors(Node node)
        {
            node.Color = Red;
            node.Left.Color = Black;
            node.Right.Color = Black;
        }

        private Node RotateRight(Node node)
        {
            var newNode = node.Left;
            node.Left = newNode.Right;
            newNode.Right = node;

            newNode.Color = node.Color;
            node.Color = Red;
            newNode.Count = node.Count;
            node.Count = NodeCount(node);

            return newNode;
        }

        private Node RotateLeft(Node node)
        {
            var newNode = node.Right;
            node.Right = newNode.Left;
            newNode.Left = node;

            newNode.Color = node.Color;
            node.Color = Red;
            newNode.Count = node.Count;
            node.Count = NodeCount(node);

            return newNode;
        }

        private bool IsRed(Node node)
        {
            if (node == null)
            {
                return false;
            }

            return node.Color == Red;
        }

        public T Select(int rank)
        {
            throw new NotImplementedException();
        }

        public int Rank(T element)
        {
            return 0;
        }

        public bool Contains(T element)
        {
            return Search(element) != null;
        }

        public IBinarySearchTree<T> Search(T element)
        {
            var node = Search(element, root);

            if (node != null)
            {
                var result = new RedBlackTree<T>();
                EachInOrder(x => result.Insert(x), node);
                return result;
            }

            return null;
        }

        private Node Search(T element, Node node)
        {
            if (node == null)
            {
                return null;
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                return Search(element, node.Right);
            }
            else if (element.CompareTo(node.Value) > 0)
            {
                return Search(element, node.Left);
            }

            return node;
        }

        public void DeleteMin()
        {
        }

        public void DeleteMax()
        {
        }

        public IEnumerable<T> Range(T startRange, T endRange)
        {
            return null;
        }

        public  void Delete(T element)
        {
        }

        public T Ceiling(T element)
        {
            throw new NotImplementedException();
        }

        public T Floor(T element)
        {
            throw new NotImplementedException();
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(action, root);
        }
        private void EachInOrder(Action<T> action, Node node)
        {
            if (node == null)
            {
                return;
            }

            EachInOrder(action, node.Left);
            action(node.Value);
            EachInOrder(action, node.Right);
        }

        private class Node
        {
            public Node(T value)
            {
                this.Value = value;
                Color = Red;
            }

            public T Value { get; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
            public int Count { get; set; }
        }
    }
}