using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SpurRoguelike.Core.Primitives;
using SpurRoguelike.Core.Views;

namespace PlayerBot
{
    public class PathFinder
    {
        public Path FindPath()
        {
            throw new NotImplementedException();
        }

        private int[,] GetInfluenceMap(LevelView levelView) // TODO: подумать про выделение типа и вынесении этого метода туда
        {
            var field = levelView.Field;
            var influenceMap = new int[field.Width, field.Height];
            foreach (var monster in levelView.Monsters)
            {
                influenceMap[monster.Location.X, monster.Location.Y] = Influences.MonsterInfluence;
            }
            foreach (var hp in levelView.HealthPacks)
            {
                influenceMap[hp.Location.X, hp.Location.Y] = Influences.HealthPackInluence;
            }
            foreach (var item in levelView.Items) // TODO: разнести по методам
            {
                influenceMap[item.Location.X, item.Location.Y] = Influences.ItemInfluence; 
            }
            foreach (var wall in field.GetCellsOfType(CellType.Wall))
            {
                influenceMap[wall.X, wall.Y] = Influences.WallInfluence;
            }
            foreach (var trap in field.GetCellsOfType(CellType.Trap))
            {
                influenceMap[trap.X, trap.Y] = Influences.TrapInfluence;
            }

            return influenceMap;
        }
    }
}
