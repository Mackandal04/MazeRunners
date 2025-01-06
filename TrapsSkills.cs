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
            Game game = new Game();
            game.ShowGame(maze,"Teleporting the token");
            Thread.Sleep(1000);
            token.myX = 1;

            token.myY = 1;

            maze.maze[token.myX,token.myY] = token;

            game.ShowGame(maze,"TokenTeleportedSkill successfully activated");
            Thread.Sleep(1800);
        }
    }

    public class TakeDamage : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {

            int life = token.Health;

            life -= 3;

            token.Health = life;

            Game game = new Game();
            game.ShowGame(maze,token.name +" takes 3 of damage");
            Thread.Sleep(1000);
        }
    }

    public class EliminateTokenSkill : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            token.cooldowmSkill = int.MinValue;
            Game game = new Game();
            game.ShowGame(maze,"This token won't be able to use his skill no more");
            Thread.Sleep(1800);
        }
    }

    public class StuckToken : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            token.StuckTurns = 3;
                        Game game = new Game();
            game.ShowGame(maze,"This token is stuck for three turns");
            Thread.Sleep(1000);
        }
    }
}