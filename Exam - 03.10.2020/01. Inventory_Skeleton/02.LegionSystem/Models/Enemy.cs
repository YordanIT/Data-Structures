namespace _02.LegionSystem.Models
{
    using System;
    using _02.LegionSystem.Interfaces;

    public class Enemy : IEnemy
    {
        public Enemy(int attackSpeed, int health)
        {
            this.AttackSpeed = attackSpeed;
            this.Health = health;
        }

        public int AttackSpeed { get; set; }

        public int Health { get; set; }

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            IEnemy otherEnemy = obj as Enemy;

            if (otherEnemy != null)
                return this.AttackSpeed.CompareTo(otherEnemy.AttackSpeed);
            else
                throw new ArgumentException("Object is not a Enemy");
        }
    }
}
