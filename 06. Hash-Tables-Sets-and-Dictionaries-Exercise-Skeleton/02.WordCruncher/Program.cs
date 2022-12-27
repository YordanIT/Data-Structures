var syllables = Console.ReadLine().Split(", ").ToArray();
var word = Console.ReadLine();
var cruncher = new Cruncher(syllables, word);

foreach (var path in cruncher.GetSyllablePaths())
{
    Console.WriteLine(path);
}


class Cruncher
{
    private List<Node> syllableGroups;

    public Cruncher(string[] syllables, string word)
    {
        syllableGroups = new List<Node>();
        syllableGroups = GenerateSyllableGroups(syllables, word);
    }

    private List<Node> GenerateSyllableGroups(string[] syllables, string word)
    {
        if (string.IsNullOrEmpty(word) || syllables.Length == 0)
        {
            return null;
        }

        var result = new List<Node>();

        for (int i = 0; i < syllables.Length; i++)
        {
            var syllable = syllables[i];

            if (word.StartsWith(syllable))
            {
                var nextSyllables = GenerateSyllableGroups(
                    syllables.Where((_, index) => index != i).ToArray(),
                    word.Substring(syllable.Length));

                result.Add(new Node(syllable, nextSyllables));
            }
        }

        return result;
    }

    public HashSet<string> GetSyllablePaths()
    {
        var allPaths = new List<List<string>>();
        ReconstructsPaths(syllableGroups, new Stack<string>(), allPaths);

        return new HashSet<string>(allPaths.Select(x => string.Join(' ', x)));
    }

    private void ReconstructsPaths(List<Node> nodes, Stack<string> currPath, List<List<string>> allPaths)
    {
        if (nodes == null)
        {
            allPaths.Add(new List<string>(currPath.Reverse()));
            return;
        }

        foreach (var node in nodes)
        {
            currPath.Push(node.Syllable);
            ReconstructsPaths(node.Syllables, currPath, allPaths);
            currPath.Pop();
        }
    }

    private class Node
    {
        public Node(string syllable, List<Node> nextSyllables)
        {
            Syllable = syllable;
            Syllables = nextSyllables;
        }

        public string Syllable { get; set; }
        public List<Node> Syllables { get; set; }
    }
}