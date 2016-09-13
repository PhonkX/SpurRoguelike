using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpurRoguelike.Core;
using SpurRoguelike.Core.Primitives;
using SpurRoguelike.Core.Views;

namespace PlayerBot
{
    public class InfluenceMap
    {
        public int[,] Map { get; private set; }

        private InfluenceMap(int[,] map)
        {
            Map = map;
        }

        public static InfluenceMap GetInfluenceMap(LevelView levelView)
        {
            var influenceMap = new int[levelView.Field.Width, levelView.Field.Height];
            FillMonstersCells(ref influenceMap, levelView);
            FillHpCells(ref influenceMap, levelView);
            FillItemsCells(ref influenceMap, levelView);
            FillWallsCells(ref influenceMap, levelView);
            FillTrapsCells(ref influenceMap, levelView);
            
            return new InfluenceMap(influenceMap);
        }

        private static void FillMonstersCells(ref int[,] map, LevelView levelView) // TODO: подумать, как лучше сделать
        {
            foreach (var monster in levelView.Monsters)
            {
                map[monster.Location.X, monster.Location.Y] = Influences.MonsterInfluence; // TODO: подумать, что сюда впилить
                var neighbours = new Cell(monster.Location.X, monster.Location.Y)
                    .GetCellNeighbours(levelView.Field.Width, levelView.Field.Height);
                foreach (var neighbour in neighbours)
                {
                    map[neighbour.Location.X, neighbour.Location.Y] = Influences.MonsterInfluence;
                }
            }
        }

        private static void FillHpCells(ref int[,] map, LevelView levelView)
        {
            foreach (var hp in levelView.HealthPacks)
            {
                map[hp.Location.X, hp.Location.Y] = Influences.HealthPackInluence;
            }
        }

        private static void FillItemsCells(ref int[,] map, LevelView levelView)
        {
            foreach (var item in levelView.Items)
            {
                map[item.Location.X, item.Location.Y] = Influences.ItemInfluence;
            }
        }

        private static void FillWallsCells(ref int[,] map, LevelView levelView)
        {
            foreach (var wall in levelView.Field.GetCellsOfType(CellType.Wall))
            {
                map[wall.X, wall.Y] = Influences.WallInfluence;
            }
        }

        private static void FillTrapsCells(ref int[,] map, LevelView levelView)
        {
            foreach (var trap in levelView.Field.GetCellsOfType(CellType.Trap))
            {
                map[trap.X, trap.Y] = Influences.TrapInfluence;
                var neighbours = new Cell(trap.X, trap.Y)
                    .GetCellNeighbours(levelView.Field.Width, levelView.Field.Height);
                foreach (var neighbour in neighbours)
                {
                    map[neighbour.Location.X, neighbour.Location.Y] = Influences.MonsterInfluence;
                }
            }
        } 
    }
}
