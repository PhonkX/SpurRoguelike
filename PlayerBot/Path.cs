using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerBot
{
    public class Path
    {
        public List<Cell> FoundPath { get; private set; }

        public Path()
        {
            FoundPath = new List<Cell>();
        }

        public void Add(Cell cell)
        {
            FoundPath.Add(cell);
        }
    }
}
