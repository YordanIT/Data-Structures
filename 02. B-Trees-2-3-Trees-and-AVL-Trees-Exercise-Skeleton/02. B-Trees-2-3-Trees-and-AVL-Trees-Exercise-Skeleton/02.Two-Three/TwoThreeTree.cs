namespace _02.Two_Three
{
    using System;
    using System.Text;

    public class TwoThreeTree<T> where T : IComparable<T>
    {
        private TreeNode<T> root;

        public void Insert(T key)
        {
            root = Insert(root, key);
        }

        private TreeNode<T> Insert(TreeNode<T> node, T element)
        {
            if (node == null)
            {
                return new TreeNode<T>(element);
            }

            if (node.IsLeaf())
            {
                return MergeElement(node, new TreeNode<T>(element));
            }

            TreeNode<T> returnNode;
            if (element.CompareTo(node.LeftKey) < 0)
            {
                returnNode = Insert(node.LeftChild, element);
                if (returnNode == node.LeftChild)
                {
                    return node;
                }
                else
                {
                    return MergeElement(node, returnNode);
                }
            }
            else if (node.IsTwoNode() || element.CompareTo(node.RightKey) < 0)
            {
                returnNode = Insert(node.MiddleChild, element);
                if (returnNode == node.MiddleChild)
                {
                    return node;
                }
                else
                {
                    return MergeElement(node, returnNode);
                }
            }
            else
            {
                returnNode = Insert(node.RightChild, element);
                if (returnNode == node.RightChild)
                {
                    return node;
                }
                else
                {
                    return MergeElement(node, returnNode);
                }
            }
        }

        private TreeNode<T> MergeElement(TreeNode<T> currNode, TreeNode<T> node)
        {
            if (currNode.IsTwoNode())
            {
                if (currNode.LeftKey.CompareTo(node.LeftKey) < 0)
                {
                    currNode.RightKey = node.LeftKey;
                    currNode.MiddleChild = node.LeftChild;
                    currNode.RightChild = node.MiddleChild;
                }
                else
                {
                    currNode.RightKey = currNode.LeftKey;
                    currNode.RightChild = currNode.MiddleChild;
                    currNode.LeftKey = node.LeftKey;
                    currNode.MiddleChild = node.MiddleChild;
                }

                return currNode;
            }
            else if (currNode.LeftKey.CompareTo(node.LeftKey) >= 0)
            {
                var newNode = new TreeNode<T>(currNode.LeftKey)
                {
                    LeftChild = node,
                    MiddleChild = currNode
                };

                node.LeftChild = currNode.LeftChild;
                currNode.LeftChild = currNode.MiddleChild;
                currNode.RightChild = null;
                currNode.LeftKey = currNode.RightKey;
                currNode.RightKey = default;

                return newNode;
            }
            else if (currNode.RightKey.CompareTo(node.LeftKey) >= 0)
            {
                node.MiddleChild = new TreeNode<T>(currNode.RightKey)
                {
                    LeftChild = node.MiddleChild,
                    MiddleChild = currNode.RightChild
                };

                node.LeftChild = currNode;
                currNode.RightKey = default;
                currNode.RightChild = null;

                return node;
            }
            else
            {
                var newNode = new TreeNode<T>(currNode.RightKey)
                {
                    LeftChild = currNode,
                    MiddleChild = node
                };

                node.LeftChild = currNode.RightChild;
                currNode.RightChild = null;
                currNode.RightKey = default;

                return newNode; 
            }
        }

        public override String ToString()
        {
            StringBuilder sb = new StringBuilder();
            RecursivePrint(this.root, sb);
            return sb.ToString();
        }

        private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
        {
            if (node == null)
            {
                return;
            }

            if (node.LeftKey != null)
            {
                sb.Append(node.LeftKey).Append(" ");
            }

            if (node.RightKey != null)
            {
                sb.Append(node.RightKey).Append(Environment.NewLine);
            }
            else
            {
                sb.Append(Environment.NewLine);
            }

            if (node.IsTwoNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
            }
            else if (node.IsThreeNode())
            {
                RecursivePrint(node.LeftChild, sb);
                RecursivePrint(node.MiddleChild, sb);
                RecursivePrint(node.RightChild, sb);
            }
        }
    }
}
