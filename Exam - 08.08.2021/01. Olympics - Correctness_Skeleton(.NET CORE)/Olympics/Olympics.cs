using System;
using System.Collections.Generic;
using System.Linq;

public class Olympics : IOlympics
{
    private SortedSet<Competitor> competitors;
    private SortedSet<Competition> competitions;

    public Olympics()
    {
        competitors = new SortedSet<Competitor>();
        competitions = new SortedSet<Competition>();
    }

    public void AddCompetition(int id, string name, int participantsLimit)
    {
        var isAdded = competitions.Add(new Competition(name, id, participantsLimit));

        if (!isAdded)
        {
            throw new ArgumentException();
        }
    }

    public void AddCompetitor(int id, string name)
    {
        var isAdded = competitors.Add(new Competitor(id, name));

        if (!isAdded)
        {
            throw new ArgumentException();
        }
    }

    public void Compete(int competitorId, int competitionId)
    {
        var competitor = competitors.FirstOrDefault(c => c.Id == competitorId);
        var competition = competitions.FirstOrDefault(c => c.Id == competitionId);

        if (competitor == null || competition == null)
        {
            throw new ArgumentException();
        }

        competition.Competitors.Add(competitor);
        competitor.TotalScore += competition.Score;
    }

    public int CompetitionsCount()
    {
        return competitions.Count;
    }

    public int CompetitorsCount()
    {
        return competitors.Count;
    }

    public bool Contains(int competitionId, Competitor comp)
    {
        var competition = competitions.FirstOrDefault(c => c.Id == competitionId);
        if (competition == null)
        {
            throw new ArgumentException();
        }

        var competitor = competition.Competitors.FirstOrDefault(c => c.Id == comp.Id);
        return competitor != null;
    }

    public void Disqualify(int competitionId, int competitorId)
    {
        var competition = competitions.FirstOrDefault(c => c.Id == competitionId);
        if (competition == null)
        {
            throw new ArgumentException();
        }

        var competitor = competition.Competitors.FirstOrDefault(c => c.Id == competitorId);

        competition.Competitors.Remove(competitor);
        var score = competitor.TotalScore - competition.Score;
        competitor.TotalScore = score < 0 ? 0 : score;
    }

    public IEnumerable<Competitor> FindCompetitorsInRange(long min, long max)
    {
        var comps = competitors.Where(c => c.TotalScore > min && c.TotalScore <= max).ToList();

        return comps;
    }

    public IEnumerable<Competitor> GetByName(string name)
    {
        var comps = competitors.Where(c => c.Name == name).ToList();

        if (comps.Count == 0)
        {
            throw new ArgumentException();
        }

        return comps;
    }

    public Competition GetCompetition(int id)
    {
        var comp = competitions.FirstOrDefault(c => c.Id == id);

        if (comp == null)
        {
            throw new ArgumentException();
        }

        return comp;
    }

    public IEnumerable<Competitor> SearchWithNameLength(int min, int max)
    {
        var comps = competitors.Where(c => c.Name.Length >= min && c.Name.Length <= max);

        return comps;
    }
}