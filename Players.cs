using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public abstract class Player
    {
        public abstract void Play(); 
    }

    public class PlayerOne: Player
    {
        Tokens[] playerOneTokens;

        public override void Play()
        {
            throw new NotImplementedException();
        }
    }

    public class PlayerTwo: Player
    {
        Tokens[] playerTwoTokens;

        public override void Play()
        {
            throw new NotImplementedException();
        }
    }
}