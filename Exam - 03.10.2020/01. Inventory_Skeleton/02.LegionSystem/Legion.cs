namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;

    public class Legion : IArmy
    {
        private SortedSet<IEnemy> legion;

        public Legion()
        {
            legion = new SortedSet<IEnemy>();        
        }

        //O(1)
        public int Size => legion.Count;

        //O(n)
        public bool Contains(IEnemy enemy)
        {
            return legion.Contains(enemy);
        }

        //O(logn)
        public void Create(IEnemy enemy)
        {
            legion.Add(enemy);
        }

        //O(n)
        public IEnemy GetByAttackSpeed(int speed)
        {
            foreach (IEnemy enemy in legion)
            {
                if (enemy.AttackSpeed == speed)
                {
                    return enemy;
                }
            }

            return null;
        }

        //O(n)
        public List<IEnemy> GetFaster(int speed)
        {
            List<IEnemy> enemies = legion.Where(e => e.AttackSpeed > speed).ToList();

            return enemies;
        }

        //O(1)
        public IEnemy GetFastest()
        {
            if (legion.Count == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            IEnemy fastest = legion.Last();

            return fastest;
        }

        //O(n)
        public IEnemy[] GetOrderedByHealth()
        {
            IEnemy[] array = legion.OrderByDescending(e => e.Health).ToArray();

            return array;
        }
        
        //O(n)
        public List<IEnemy> GetSlower(int speed)
        {
            List<IEnemy> enemies = legion.Where(e => e.AttackSpeed < speed).ToList();

            return enemies;
        }
        
        //O(1)
        public IEnemy GetSlowest()
        {
            if (legion.Count == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            return legion.First();
        }

        //O(1)
        public void ShootFastest()
        {
            if (legion.Count == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            var enemy = legion.Last();
            legion.Remove(enemy);
        }

        //O(1)
        public void ShootSlowest()
        {
            if (legion.Count == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }

            var enemy = legion.First();
            legion.Remove(enemy);
        }
    }
}
