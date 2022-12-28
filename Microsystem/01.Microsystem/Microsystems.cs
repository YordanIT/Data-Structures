namespace _01.Microsystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Microsystems : IMicrosystem
    {
        private Dictionary<int, Computer> computers = new Dictionary<int, Computer>();

        public void CreateComputer(Computer computer)
        {
            if (computers.ContainsKey(computer.Number))
            {
                throw new ArgumentException();
            }

            computers.Add(computer.Number, computer);
        }

        public bool Contains(int number)
        {
            return computers.ContainsKey(number);
        }

        public int Count()
        {
            return computers.Count;
        }

        public Computer GetComputer(int number)
        {
            if (!computers.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            return computers[number];
        }

        public void Remove(int number)
        {
            if (!computers.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            computers.Remove(number);
        }

        public void RemoveWithBrand(Brand brand)
        {
            if (!computers.Values.Any(x => x.Brand == brand))
            {
                throw new ArgumentException();
            }

            foreach (var computer in computers.Values)
            {
                if (computer.Brand == brand)
                {
                    computers.Remove(computer.Number);
                }
            }
        }

        public void UpgradeRam(int ram, int number)
        {
            if (!computers.ContainsKey(number))
            {
                throw new ArgumentException();
            }

            var pc = computers[number];

            if (pc.RAM < ram)
            {
                pc.RAM = ram;
            }
        }

        public IEnumerable<Computer> GetAllFromBrand(Brand brand)
        {
            var result = computers.Values.Where(pc => pc.Brand == brand).OrderByDescending(pc => pc.Price);
            return result;
        }

        public IEnumerable<Computer> GetAllWithScreenSize(double screenSize)
        {
            var result = computers.Values.Where(pc => pc.ScreenSize == screenSize).OrderByDescending(pc => pc.Number);
            return result;
        }

        public IEnumerable<Computer> GetAllWithColor(string color)
        {
            var result = computers.Values.Where(pc => pc.Color == color).OrderByDescending(pc => pc.Price);
            return result;
        }

        public IEnumerable<Computer> GetInRangePrice(double minPrice, double maxPrice)
        {
            var result = computers.Values.Where(pc => pc.Price >= minPrice && pc.Price <= maxPrice)
                .OrderByDescending(pc => pc.Price);
            return result;
        }
    }
}
