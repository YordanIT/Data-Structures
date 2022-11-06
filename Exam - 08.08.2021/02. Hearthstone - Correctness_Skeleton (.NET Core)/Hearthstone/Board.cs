using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Board : IBoard
{
    private SortedSet<Card> cards = new SortedSet<Card>();

    public bool Contains(string name)
    {
        return cards.Any(c => c.Name == name);
    }

    public int Count()
    {
        return cards.Count;
    }

    public void Draw(Card card)
    {
        if (cards.Any(c => c.Name== card.Name))
        {
            throw new ArgumentException();
        }

        cards.Add(card);
    }

    public IEnumerable<Card> GetBestInRange(int start, int end)
    {
        return cards.Where(c => c.Score >= start && c.Score <= end)
            .OrderByDescending(c => c.Level);
    }

    public void Heal(int health)
    {
        cards.OrderBy(c => c.Health).FirstOrDefault().Health += health;
    }

    public IEnumerable<Card> ListCardsByPrefix(string prefix)
    {
        return cards.Where(c => c.Name.StartsWith(prefix))
            .OrderBy(c => c.Name.Reverse()).ThenBy(c => c.Level);
    }

    public void Play(string attackerCardName, string attackedCardName)
    {
        var attacker = cards.FirstOrDefault(c => c.Name == attackerCardName);
        var attacked = cards.FirstOrDefault(c => c.Name == attackedCardName);

        if (attacker == null || attacked == null)
        {
            throw new ArgumentException();
        }
        else if (attacker.Level != attacked.Level)
        {
            throw new ArgumentException();
        }
        else if (attacked.Health <= 0)
        {
            return;
        }
        
        attacked.Health -= attacker.Damage;
        if (attacked.Health <= 0)
        {
            attacker.Score += attacked.Level;
        }
    }

    public void Remove(string name)
    {
        var card = cards.FirstOrDefault(c => c.Name == name);

        if (card == null)
        {
            throw new ArgumentException();
        }

        cards.Remove(card);
    }

    public void RemoveDeath()
    {
        cards.RemoveWhere(c => c.Health <= 0);
    }

    public IEnumerable<Card> SearchByLevel(int level)
    {
        return cards.Where(c => c.Level == level).OrderByDescending(c => c.Score);
    }
}