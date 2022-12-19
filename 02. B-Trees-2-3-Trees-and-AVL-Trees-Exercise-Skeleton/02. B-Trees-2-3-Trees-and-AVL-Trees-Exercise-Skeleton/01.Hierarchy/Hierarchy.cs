namespace _01.Hierarchy
{
    using System;
    using System.Collections.Generic;
    using System.Collections;
    using System.Linq;

    public class Hierarchy<T> : IHierarchy<T>
    {
        private class Node
        {
            public Node(T value)
            {
                Value = value;
                Children = new List<Node>();
            }
            public T Value { get; set; }
            public List<Node> Children { get; set; }
        }

        private Node root;
        private Dictionary<T, Node> nodesByValue;
        private Dictionary<T, Node> parentsByChild;

        public Hierarchy(T item)
        {
            root = new Node(item);
            nodesByValue = new Dictionary<T, Node>();
            parentsByChild = new Dictionary<T, Node>();
            nodesByValue.Add(item, root);
            parentsByChild.Add(item, null);
        }

        public int Count => nodesByValue.Count;

        public void Add(T element, T child)
        {
            if (!nodesByValue.ContainsKey(element) || !nodesByValue.ContainsKey(child))
            {
                throw new ArgumentException();
            }
 
            var node = new Node(child);
            nodesByValue.Add(child, node);
            parentsByChild.Add(child, nodesByValue[element]);
            nodesByValue[element].Children.Add(node);
        }
        public void Remove(T element)
        {
            if (root.Value.Equals(element))
            {
                throw new InvalidOperationException();
            }
            else if (!nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            var parent = parentsByChild[element];
            var node = nodesByValue[element];
            parent.Children.Remove(node);
            parent.Children.AddRange(node.Children);

            foreach (var child in node.Children)
            {
                parentsByChild[child.Value] = parent;
            }

            nodesByValue.Remove(element);
            parentsByChild.Remove(element);
        }

        public IEnumerable<T> GetChildren(T element)
        {
            if (!nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            return nodesByValue[element].Children.Select(node => node.Value);
        }

        public T GetParent(T element)
        {
            if (!nodesByValue.ContainsKey(element))
            {
                throw new ArgumentException();
            }

            var parent = parentsByChild[element].Value != null ? parentsByChild[element].Value : default;
            return parent;
        }

        public bool Contains(T element)
        {
            return nodesByValue.ContainsKey(element);
        }

        public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
        {
            return nodesByValue.Keys.Intersect(other.nodesByValue.Keys);
        }

        public IEnumerator<T> GetEnumerator()
        {
            var queue = new Queue<Node>();
            queue.Enqueue(root);
            
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                yield return node.Value;

                foreach (var child in node.Children)
                {
                    queue.Enqueue(child);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}