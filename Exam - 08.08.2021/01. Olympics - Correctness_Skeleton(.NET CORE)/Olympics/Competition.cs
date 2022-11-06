using System;
using System.Collections.Generic;

public class Competition : IComparable
{
    public Competition(string name, int id, int score)
    {
        this.Name = name;
        this.Id = id;
        this.Score = score; 
        this.Competitors = new SortedSet<Competitor>();
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public int Score { get; set; }

    public ICollection<Competitor> Competitors { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (!(obj is Competition))
        {
            return false;
        }

        return (this.Id == ((Competition)obj).Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        if (obj is Competition comp)
            return this.Id.CompareTo(comp.Id);
        else
            throw new ArgumentException();
    }
}
