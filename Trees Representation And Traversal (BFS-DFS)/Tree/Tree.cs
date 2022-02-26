namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Tree<T> : IAbstractTree<T>
    {
        private readonly List<Tree<T>> children;

        public Tree(T key, params Tree<T>[] children)
        {
            Key = key;
            this.children = children.ToList();

            foreach (var child in this.children)
            {
                child.AddParent(this);
            }
        }

        public T Key { get; private set; }

        public Tree<T> Parent { get; private set; }

        public IReadOnlyCollection<Tree<T>> Children
            => this.children.AsReadOnly();

        public void AddChild(Tree<T> child)
        {
            children.Add(child);
        }

        public void AddParent(Tree<T> parent)
        {
            Parent = parent;
        }

        public string GetAsString()
        {
            return GetAsString(0).Trim();
        }
        private string GetAsString(int indent = 0)
        {
            var result = new string(' ', indent) + Key + "\r\n";

            foreach (var child in children)
            {
                result += child.GetAsString(indent + 2);
            }

            return result;
        }

        public Tree<T> GetDeepestLeftMostNode()
        {
            var node = this;

            while (node.Children.Count > 0)
            {
                node = node.Children.FirstOrDefault();
            }

            return node;
        }

        public List<T> GetLeafKeys()
        {
            var leafs = GetLeafNotes();

            return leafs.Select(n => n.Key).ToList();
        }

        private List<Tree<T>> GetLeafNotes()
        {
            var leafNotes = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Children.Count == 0)
                {
                    leafNotes.Add(node);
                }

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return leafNotes;
        }

        public List<T> GetMiddleKeys()
        {
            var middleNodes = GetMiddleNotes();

            return middleNodes.Select(n => n.Key).ToList();
        }

        private List<Tree<T>> GetMiddleNotes()
        {
            var middleNotes = new List<Tree<T>>();
            var queue = new Queue<Tree<T>>();
            queue.Enqueue(this);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();

                if (node.Children.Count != 0 && node.Parent != null)
                {
                    middleNotes.Add(node);
                }

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }

            return middleNotes;
        }

        public List<T> GetLongestPath()
        {
            var node = this;
            var path = new List<T>();

            while (node.Children.Count > 0)
            {
                path.Add(node.Key);
                node = node.Children.FirstOrDefault();
            }
            path.Add(node.Key);

            return path;
        }

        public List<List<T>> PathsWithGivenSum(int sum)
        {
            var leafs = GetLeafNotes();
            var result = new List<List<T>>();

            foreach (var leaf in leafs)
            {
                var node = leaf;
                var currentSum = 0;
                var currentNodes = new List<T>();

                while (node != null)
                {
                    currentNodes.Add(node.Key);
                    currentSum += Convert.ToInt32(node.Key);
                    node = node.Parent;
                }

                if (currentSum == sum)
                {
                    currentNodes.Reverse();
                    result.Add(currentNodes);
                }
            }

            return result;
        }

        public List<Tree<T>> SubTreesWithGivenSum(int sum)
        {
            var roots = new List<Tree<T>>();
            SumSubTree(this, sum, roots);

            return roots;
        }

        private int SumSubTree(Tree<T> node, int goalSum, List<Tree<T>> roots)
        {
            var currentSum = Convert.ToInt32(node.Key);

            foreach (var child in node.Children)
            {
                currentSum += SumSubTree(child, goalSum, roots);
            }

            if (currentSum == goalSum)
            {
                roots.Add(node);
            }

            return currentSum;
        }
    }
}
