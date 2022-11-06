using System;

public class Competitor : IComparable
{
    public Competitor(int id, string name)
    {
        this.Id = id;
        this.Name = name;
        this.TotalScore = 0;
    }

    public int Id { get; set; }

    public string Name { get; set; }

    public long TotalScore { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (!(obj is Competitor))
        {
            return false;
        }

        return (this.Id == ((Competitor)obj).Id);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        if (obj is Competitor comp)
            return this.Id.CompareTo(comp.Id);
        else
            throw new ArgumentException();
    }
}
