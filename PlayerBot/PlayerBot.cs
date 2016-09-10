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
    public class PlayerBot: IPlayerController
    {
        public Turn MakeTurn(LevelView levelView, IMessageReporter messageReporter)
        {
            throw new NotImplementedException();
        }
    }
}
