namespace Tree
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class TreeFactory
    {
        private Dictionary<int, Tree<int>> nodesBykeys;

        public TreeFactory()
        {
            nodesBykeys = new Dictionary<int, Tree<int>>();
        }

        public Tree<int> CreateTreeFromStrings(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                var args = input[i].Split(' ').Select(int.Parse).ToArray();

                if (!nodesBykeys.ContainsKey(args[0]))
                {
                    var node = CreateNodeByKey(args[0]);
                    nodesBykeys[args[0]] = node;
                }

                if (!nodesBykeys.ContainsKey(args[1]))
                {
                    var node = CreateNodeByKey(args[1]);
                    nodesBykeys[args[1]] = node;
                }

                AddEdge(args[0], args[1]);
            }

            return GetRoot();
        }

        public Tree<int> CreateNodeByKey(int key)
        {
            var node = new Tree<int>(key);
            return node;
        }

        public void AddEdge(int parent, int child)
        {
            var parentNode = nodesBykeys[parent];
            var childNode = nodesBykeys[child];

            parentNode.AddChild(childNode);
            childNode.AddParent(parentNode);
        }

        private Tree<int> GetRoot()
        {
            var root = nodesBykeys.Values.Where(n => n.Parent == null).FirstOrDefault();
            return root;
        }
    }
}
