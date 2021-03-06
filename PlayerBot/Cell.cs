﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpurRoguelike.Core.Primitives;

namespace PlayerBot
{
    public class Cell
    {
        public Location Location;
        public Cell PreviousCell; // для A*
        public int DistanсeFromStartPoint;

        public Cell(int x, int y, int distanceFromStart = 0)
        {
            Location = new Location(x, y);
            PreviousCell = null;
            DistanсeFromStartPoint = distanceFromStart; // TODO: подумать, может, лучше не инициализировать здесь
        }

        public List<Cell> GetCellNeighbours(int fieldWidth, int fieldHeight) // TODO: подумать над тем, какую коллекцию здесь использовать
        {
            int x = Location.X;
            int y = Location.Y;
            var neighbours = new List<Cell>();
            if (x > 0)
            {
                neighbours.Add(new Cell(x - 1, y, DistanсeFromStartPoint + 1));
            }
            if (y > 0)
            {
                neighbours.Add(new Cell(x, y - 1, DistanсeFromStartPoint + 1));
            }
            if (x < fieldWidth - 1)
            {
                neighbours.Add(new Cell(x + 1, y, DistanсeFromStartPoint + 1));
            }
            if (x < fieldHeight - 1)
            {
                neighbours.Add(new Cell(x, y + 1, DistanсeFromStartPoint + 1));
            }

            return neighbours;
        }
    }
}
