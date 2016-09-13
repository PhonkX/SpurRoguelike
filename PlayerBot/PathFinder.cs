using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpurRoguelike.Core.Primitives;
using SpurRoguelike.Core.Views;

namespace PlayerBot
{
    public class PathFinder
    {
        private HashSet<Cell> openSet;
        private HashSet<Cell> closedSet;

        public PathFinder()
        {
            openSet = new HashSet<Cell>();
            closedSet = new HashSet<Cell>();
        }

        public Path FindPath(LevelView levelView, Location start, Location end)
        {
            // TODO: подумать про включение диагональных клеток в окрестность
            // если не нашли путь, то, если рядом есть монстр, атакуем либо бежим (вероятность?)
            // TODO: посмотреть, можно ли узнать, есть ли на уровне босс
            openSet.Clear();
            closedSet.Clear();

            var influenceMap = InfluenceMap.GetInfluenceMap(levelView);

            openSet.Add(new Cell(start.X, start.Y));

            while (openSet.Count > 0)
            {
                var currentCell = openSet.OrderBy(cell => cell.DistanсeFromStartPoint).First();
                if (currentCell.Location.Equals(end))
                {
                    var path = GetPathForCell(currentCell);
                    path.Add(currentCell);
                    return path;
                }

                openSet.Remove(currentCell);
                closedSet.Add(currentCell);

                foreach (var neighbour in currentCell.GetCellNeighbours(levelView.Field.Width, levelView.Field.Height))
                {
                    if (closedSet.Contains(neighbour))
                    {
                        continue;
                    }
                    // TODO: подумать, как лучше здесь сделать
                    var openCell = openSet.FirstOrDefault(cell => cell.Location.Equals(neighbour.Location));
                    if (openCell == null || openCell.Equals(default(Cell)))
                    {
                        openSet.Add(new Cell(
                            neighbour.Location.X,
                            neighbour.Location.Y,
                            neighbour.DistanсeFromStartPoint
                            + influenceMap.Map[neighbour.Location.X, neighbour.Location.Y]
                            )); // TODO: подумать, как сделать лучше
                    }
                    else
                    {
                        if (openCell.DistanсeFromStartPoint > neighbour.DistanсeFromStartPoint)
                        {
                            openCell.DistanсeFromStartPoint = neighbour.DistanсeFromStartPoint
                                + influenceMap.Map[openCell.Location.X, openCell.Location.Y];
                            openCell.PreviousCell = currentCell;
                        }
                    }
                }
                
            }

            return null; // TODO: подумать, что вернуть
        }

        private Path GetPathForCell(Cell cell)
        {
            var path = new Path();
            Cell previousCell;
            while ((previousCell = cell.PreviousCell).PreviousCell != null)
            {
                path.Add(previousCell);
            }

            return path;
        }
    }
}
