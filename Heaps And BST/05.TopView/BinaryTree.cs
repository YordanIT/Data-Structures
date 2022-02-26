namespace _05.TopView
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(T value, BinaryTree<T> left, BinaryTree<T> right)
        {
            Value = value;
            LeftChild = left;
            RightChild = right;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public List<T> TopView()
        {
            var view = new List<T>();
            var currentNode = this;
          
            while (currentNode != null)
            {
                view.Add(currentNode.Value);
                currentNode = currentNode.LeftChild;
            }

            currentNode = RightChild;

            while (currentNode != null)
            {
                view.Add(currentNode.Value);
                currentNode = currentNode.RightChild;
            }

            return view;
        }
    }
}
