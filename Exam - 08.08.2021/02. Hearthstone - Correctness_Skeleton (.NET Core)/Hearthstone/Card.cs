using System;
using System.Collections.Generic;
using System.Text;

public class Card : IComparable
{
    public Card(string name, int damage, int score, int level)
    {
        this.Name = name;
        this.Damage = damage;
        this.Score = score;
        this.Level = level;
        this.Health = 20;
    }
    public string Name { get; set; }

    public int Damage { get; set; }

    public int Score { get; set; }

    public int Health { get; set; }

    public int Level { get; set; }

    public override bool Equals(object obj)
    {
        if (obj == null)
        {
            return false;
        }
        if (!(obj is Card))
        {
            return false;
        }

        return (this.Name == ((Card)obj).Name);
    }

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }
    
    public int CompareTo(object obj)
    {
        if (obj == null) return 1;

        if (obj is Card card)
            return this.Name.CompareTo(card.Name);
        else
            throw new ArgumentException();
    }
}