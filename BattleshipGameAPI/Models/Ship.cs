using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameAPI.Models
{
    public class Ship
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public List<(int x, int y)> Coordinates { get; set; }
        public bool IsSunk => Coordinates.All(c => c.Item2 == -1); // -1 marks a hit
    }
}
