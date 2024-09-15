using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BattleshipGameAPI.Models
{
    public class GameBoard
    {
        public int Size { get; } = 10;
        public List<Ship> Ships { get; set; } = new List<Ship>();
        private Random random = new Random();

        public GameBoard()
        {
            PlaceShips();
        }

        private void PlaceShips()
        {
            Ships.Add(PlaceShip("Battleship", 5));
            Ships.Add(PlaceShip("Destroyer 1", 4));
            Ships.Add(PlaceShip("Destroyer 2", 4));
        }

        private Ship PlaceShip(string name, int size)
        {
            Ship ship = new Ship { Name = name, Size = size, Coordinates = new List<(int x, int y)>() };
            bool placed = false;

            while (!placed)
            {
                bool vertical = random.Next(2) == 0;
                int x = random.Next(0, Size - (vertical ? size : 0));
                int y = random.Next(0, Size - (vertical ? 0 : size));

                var coords = new List<(int, int)>();
                for (int i = 0; i < size; i++)
                {
                    coords.Add((x + (vertical ? i : 0), y + (vertical ? 0 : i)));
                }

                if (!Ships.Any(s => s.Coordinates.Intersect(coords).Any()))
                {
                    ship.Coordinates.AddRange(coords);
                    placed = true;
                }
            }

            return ship;
        }

        public bool Attack(int x, int y, out Ship hitShip)
        {
            hitShip = Ships.FirstOrDefault(s => s.Coordinates.Any(c => c.x == x && c.y == y));

            if (hitShip != null)
            {
                for (int i = 0; i < hitShip.Coordinates.Count; i++)
                {
                    if (hitShip.Coordinates[i] == (x, y))
                    {
                        hitShip.Coordinates[i] = (x, -1); // Mark as hit
                        break;
                    }
                }

                return true;
            }

            return false;
        }

        public bool IsAllShipsSunk => Ships.All(s => s.IsSunk);
    }


}
