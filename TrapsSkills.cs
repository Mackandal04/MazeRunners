using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public abstract class TrapsSkills
    {
        public abstract void ActivateSkill(Tokens token, Maze maze);
    }

    public class TeleportToBeginning : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            System.Console.WriteLine("Teleporting the token");

            token.myX = 1;

            token.myY = 1;

            maze.maze[token.myX,token.myY] = token;
            
            System.Console.WriteLine("TokenTeleportedSkill successfully activated");
        }
    }

    public class TakeDamage : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            int life = token.Health;

            life -= 3;

            token.Health = life;

            System.Console.WriteLine("The token takes 3 of damage");
        }
    }

    public class EliminateTokenSkill : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            token.cooldowmSkill = int.MinValue;

            System.Console.WriteLine("This token won't be able to use his skill no more");
        }
    }

    public class StuckToken : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            token.StuckTurns = 3;

            System.Console.WriteLine("This token is stuck for three turns");
        }
    }
}