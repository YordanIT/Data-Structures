namespace _02._AA_Tree
{
    using System;

    public class AATree<T> : IBinarySearchTree<T>
        where T : IComparable<T>
    {

        private Node<T> root;

        public int CountNodes()
        {
            return CountNodes(root);
        }

        private int CountNodes(Node<T> root)
        {
            if (root == null)
            {
                return 0;
            }
            
            return 1 + CountNodes(root.Left) + CountNodes(root.Right);
        }

        public bool IsEmpty()
        {
            return root == null;
        }

        public void Clear()
        {
            root = null;
        }

        public void Insert(T element)
        {
            Insert(element, root);
        }

        private Node<T> Insert(T element, Node<T> node)
        {
            if (node == null)
            {
                node = new Node<T>(element);
            }
            else if (element.CompareTo(node.Element) < 0)
            {
                node.Left = Insert(element, node.Left);
            }
            else
            {
                node.Right = Insert(element, node.Right);
            }

            node = Skew(node);
            node = Split(node);

            return node;
        }

        private Node<T> Split(Node<T> node)
        {
            if (node.Left == null || node.Right.Right == null)
            {
                return node;
            }
            else if (node.Right.Right.Level == node.Level)
            {
                node = Promote(node);
            }

            return node;
        }

        private Node<T> Skew(Node<T> node)
        {
            if (node.Left != null && node.Left.Level == node.Level)
            {
                node = RotateLeft(node);
            }

            return node;
        }

        private Node<T> Promote(Node<T> node)
        {
            var newNode = node.Right;
            node.Right = newNode.Left;
            newNode.Left = node;
            newNode.Level++;
                
            return newNode;
        }

        private Node<T> RotateLeft(Node<T> node)
        {
            var newNode = node.Left;
            node.Left = newNode.Right;
            newNode.Right = node;

            return newNode;
        }


        public bool Search(T element)
        {
            throw new NotImplementedException();
        }

        public void InOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void PreOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }

        public void PostOrder(Action<T> action)
        {
            throw new NotImplementedException();
        }
    }
}