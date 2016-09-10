using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerBot
{
    public class Influences // TODO: поподбирать константы
    {
        public const int MonsterInfluence = 5;
        public const int HealthPackInluence = -5; // TODO: подумать, надо ли
        public const int TrapInfluence = -1; //для того, чтобы заманивать врагов в ловушки
        public const int WallInfluence = 10;
        public const int ItemInfluence = -2;
    }
}
