namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons;

        public Inventory()
        {
            weapons = new List<IWeapon>();
        }

        public int Capacity => weapons.Count;

        //0(1)
        public void Add(IWeapon weapon)
        {
            weapons.Add(weapon);
        }

        //O(n)
        public void Clear()
        {
            weapons.Clear();
        }

        //O(n)
        public bool Contains(IWeapon weapon)
        {
            return weapons.Contains(weapon);
        }

        //O(n)
        public void EmptyArsenal(Category category)
        {
            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].Category == category)
                {
                    weapons[i].Ammunition = 0;
                }
            }
        }

        //O(n)
        public bool Fire(IWeapon weapon, int ammunition)
        {
            if (!weapons.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (weapon.Ammunition < ammunition)
            {
                return false;
            }
            else
            {
                weapon.Ammunition -= ammunition;
                return true;
            }
        }

        //O(n)
        public IWeapon GetById(int id)
        {
            IWeapon weapon = null;

            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].Id == id)
                {
                   weapon = weapons[i];
                }
            }

            return weapon;
        }

        //O(n)
        public IEnumerator GetEnumerator()
        {

            for (int i = 0; i < weapons.Count; i++)
            {
                yield return weapons[i];
            }
        }

        //O(n)
        public int Refill(IWeapon weapon, int ammunition)
        {
            if (!weapons.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            var weaponAmmuniton = weapon.Ammunition + ammunition;
            if (weaponAmmuniton > weapon.MaxCapacity)
            {
                return ammunition;
            }

            for (int i = 0; i < weapons.Count; i++)
            {
                weapons[i].Ammunition += ammunition;
            }

            return weaponAmmuniton;
        }

        //O(n)
        public IWeapon RemoveById(int id)
        {
            var count = weapons.Count;

            for (int i = 0; i < count; i++)
            {
                if (weapons[i].Id == id)
                {
                    var weapon = weapons[i];    
                    weapons.Remove(weapon);
                    return weapon;
                }
            }

            throw new InvalidOperationException("Weapon does not exist in inventory!");
        }

        //O(n)
        public int RemoveHeavy()
        {
            var count = weapons.Count;
            int heavyWeapons = 0;

            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].Category == Category.Heavy)
                {
                    weapons.Remove(weapons[i]);
                    i--;
                    heavyWeapons++;
                }
            }

            return heavyWeapons;
        }

        //O(1)
        public List<IWeapon> RetrieveAll()
        {
            weapons.Capacity = weapons.Count;
            return weapons;
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            var list = new List<IWeapon>();

            for (int i = 0; i < weapons.Count; i++)
            {
                if (weapons[i].Category >= lower && weapons[i].Category <= upper)
                {
                    list.Add(weapons[i]);
                }
            }

            return list;
        }

        //O(1)
        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            if (!(weapons.Contains(firstWeapon) || weapons.Contains(secondWeapon)))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (firstWeapon.Category != secondWeapon.Category)
            {
                return;
            }

            var indexOne = weapons.IndexOf(firstWeapon);
            var indexTwo = weapons.IndexOf(secondWeapon);

            weapons[indexOne] = secondWeapon;
            weapons[indexTwo] = firstWeapon;
        }
    }
}
