namespace _01.RoyaleArena
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class RoyaleArena : IArena
    {
        private Dictionary<int, BattleCard> cards = new Dictionary<int, BattleCard>();

        public void Add(BattleCard card)
        {
            cards.Add(card.Id, card);
        }

        public bool Contains(BattleCard card)
        {
            return cards.ContainsKey(card.Id);
        }

        public int Count => cards.Count;

        public void ChangeCardType(int id, CardType type)
        {
            if (!cards.ContainsKey(id))
                throw new InvalidOperationException();

            cards[id].Type = type; 
        }

        public BattleCard GetById(int id)
        {
            if (!cards.ContainsKey(id))
                throw new InvalidOperationException();

            return cards[id];
        }

        public void RemoveById(int id)
        {
            if (!cards.ContainsKey(id))
                throw new InvalidOperationException();

            cards.Remove(id);
        }

        public IEnumerable<BattleCard> GetByCardType(CardType type)
        {
            return cards.Values.Where(c => c.Type == type);
        }

        public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
        {
            return cards.Values.Where(c => c.Type == type && c.Damage > lo && c.Damage < hi)
                .OrderByDescending(c => c.Damage)
                .ThenBy(c => c.Id);
        }

        public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
        {
            var result = cards.Values.Where(c => c.Type == type && c.Damage <= damage)
                .OrderByDescending(c => c.Damage)
                .ThenBy(c => c.Id);

            if (!result.Any())
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
        {
            var result = cards.Values.Where(c => c.Name == name)
                .OrderByDescending(c => c.Swag)
                .ThenBy(c => c.Id);

            if (!result.Any())
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
        {
            var result = cards.Values.Where(c => c.Name == name && c.Swag >= lo && c.Swag < hi)
                .OrderByDescending(c => c.Swag)
                .ThenBy(c => c.Id);

            if (!result.Any())
            {
                throw new InvalidOperationException();
            }

            return result;
        }

        public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
        {
            if (cards.Count < n)
            {
                throw new InvalidOperationException();
            }

            return cards.Values.OrderBy(c => c.Swag).ThenBy(c => c.Id).Take(n);
        }

        public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
        {
            return cards.Values.Where(c => c.Swag >= lo && c.Swag <= hi)
                .OrderBy(c => c.Swag);
        }


        public IEnumerator<BattleCard> GetEnumerator()
        {
            foreach (var item in cards)
            {
                yield return item.Value;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}