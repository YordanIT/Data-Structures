namespace _02.LowestCommonAncestor
{
    using System;

    public class BinaryTree<T> : IAbstractBinaryTree<T>
        where T : IComparable<T>
    {
        public BinaryTree(
            T value,
            BinaryTree<T> leftChild,
            BinaryTree<T> rightChild)
        {
            Value = value;
            LeftChild = leftChild;
            RightChild = rightChild;
            Parent = this;
        }

        public T Value { get; set; }

        public BinaryTree<T> LeftChild { get; set; }

        public BinaryTree<T> RightChild { get; set; }

        public BinaryTree<T> Parent { get; set; }

        public T FindLowestCommonAncestor(T first, T second)
        {
            T result;
            var firstParent = Parent;
            var secondParent = Parent;

            if (first.CompareTo(Value) > 0 && second.CompareTo(Value) < 0 ||
                first.CompareTo(Value) < 0 && second.CompareTo(Value) > 0)
            {
                result = Value;
                return result;
            }

            if (first.CompareTo(firstParent.Value) > 0)
            {
                firstParent = firstParent.RightChild;
            }
            else
            {
                firstParent = firstParent.LeftChild;
            }

            if (second.CompareTo(secondParent.Value) > 0)
            {
                secondParent = secondParent.RightChild;
            }
            else
            {
                secondParent = secondParent.LeftChild;
            }

            while (true)
            {
                if (firstParent.Value.CompareTo(secondParent.Value) == 0)
                {
                    result = firstParent.Value;
                    break;
                }

                if (first.CompareTo(firstParent.Value) > 0)
                {
                    firstParent = firstParent.RightChild;
                }
                else
                {
                    firstParent = firstParent.LeftChild;
                }

                if (second.CompareTo(secondParent.Value) > 0)
                {
                    secondParent = secondParent.RightChild;
                }
                else
                {
                    secondParent = secondParent.LeftChild;
                }
            }

            return result;
        }
    }
}
