namespace _01.BSTOperations
{
    using System;
    using System.Collections.Generic;

    public class BinarySearchTree<T> : IAbstractBinarySearchTree<T>
        where T : IComparable<T>
    {
        public BinarySearchTree()
        {
        }

        public BinarySearchTree(Node<T> root)
        {
            Root = root;
        }

        public Node<T> Root { get; private set; }

        public int Count { get; private set; }

        public bool Contains(T element)
        {
            var currentNode = Root;

            while (currentNode != null)
            {
                if (currentNode.Value.CompareTo(element) == 0)
                {
                    return true;
                }

                if (IsGreater(currentNode.Value, element))
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                }
            }

            return false;
        }

        public void Insert(T element)
        {
            var node = new Node<T>(element, null, null);

            if (Count == 0)
            {
                Root = node;
                Count++;
                return;
            }

            var currentNode = Root;

            while (true)
            {
                if (IsGreater(currentNode.Value, element))
                {
                    if (currentNode.LeftChild == null)
                    {
                        currentNode.LeftChild = node;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.LeftChild;
                    }
                }
                else
                {
                    if (currentNode.RightChild == null)
                    {
                        currentNode.RightChild = node;
                        break;
                    }
                    else
                    {
                        currentNode = currentNode.RightChild;
                    }
                }
            }

            Count++;
        }

        private bool IsGreater(T value, T comparer)
        {
            return value.CompareTo(comparer) > 0;
        }

        public IAbstractBinarySearchTree<T> Search(T element)
        {
            var currentNode = Root;

            while (currentNode != null)
            {
                if (currentNode.Value.CompareTo(element) == 0)
                {
                    var root = new Node<T>(element, currentNode.LeftChild, currentNode.RightChild);
                    var bst = new BinarySearchTree<T>
                    {
                        Root = root
                    };
                    return bst;
                }

                if (IsGreater(currentNode.Value, element))
                {
                    currentNode = currentNode.LeftChild;
                }
                else
                {
                    currentNode = currentNode.RightChild;
                }
            }

            return null;
        }

        public void EachInOrder(Action<T> action)
        {
            EachInOrder(action, Root);
        }

        private void EachInOrder(Action<T> action, Node<T> node)
        {
            if (node == null)
            {
                return;
            }

            EachInOrder(action, node.LeftChild);
            action(node.Value);
            EachInOrder(action, node.RightChild);
        }

        public List<T> Range(T lower, T upper)
        {
            var list = new List<T>();
            Range(lower, upper, Root, list);

            return list;
        }

        private void Range(T start, T end, Node<T> node, List<T> rangeNodes)
        {
            if (node == null)
            {
                return;
            }

            var startRange = start.CompareTo(node.Value);
            var endRange = end.CompareTo(node.Value);

            if (startRange < 0)
            {
                Range(start, end, node.LeftChild, rangeNodes);
            }

            if (startRange <= 0 && endRange >= 0)
            {
                rangeNodes.Add(node.Value);
            }

            if (endRange > 0)
            {
                Range(start, end, node.RightChild, rangeNodes);
            }

        }

        public void DeleteMin()
        {
            var currentNode = Root;
            Node<T> parentNode = null;

            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            while (currentNode.LeftChild != null)
            {
                parentNode = currentNode;
                currentNode = currentNode.LeftChild;
            }

            if (currentNode.RightChild != null)
            {
                parentNode.LeftChild = currentNode.RightChild;
            }
            else
            {
                parentNode.LeftChild = null;
            }

            Count--;
        }

        public void DeleteMax()
        {
            var currentNode = Root;
            Node<T> parentNode = null;

            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            while (currentNode.RightChild != null)
            {
                parentNode = currentNode;
                currentNode = currentNode.RightChild;
            }

            if (currentNode.LeftChild != null)
            {
                parentNode.RightChild = currentNode.LeftChild;
            }
            else
            {
                parentNode.RightChild = null;
            }

            Count--;
        }

        public int GetRank(T element)
        {
            var list = new List<T>();
            Range(MinValue(), element, Root, list);
            
            return list.Count;
        }

        private T MinValue()
        {
            var currentNode = Root;
            Node<T> parentNode = null;

            if (Count == 0)
            {
                throw new InvalidOperationException();
            }

            while (currentNode.LeftChild != null)
            {
                parentNode = currentNode;
                currentNode = currentNode.LeftChild;
            }

            if (currentNode.RightChild != null)
            {
                parentNode.LeftChild = currentNode.RightChild;
            }
            else
            {
                currentNode = parentNode.LeftChild;
            }

            return currentNode.Value;
        }
    }
}
