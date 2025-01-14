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
            GameDisplay gameDisplay=new GameDisplay();

            gameDisplay.ShowGame(maze,"Teleporting the token");
            
            Thread.Sleep(1000);
            
            token.myX = 1;

            token.myY = 1;

            maze.maze[token.myX,token.myY] = token;

            gameDisplay.ShowGame(maze,"TokenTeleportedSkill successfully activated");
            
            Thread.Sleep(1800);
        }
    }

    public class TakeDamage : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay=new GameDisplay();

            int life = token.Health;

            life -= 3;

            token.Health = life;

            gameDisplay.ShowGame(maze,token.name +" takes 3 of damage");
            
            Thread.Sleep(1000);
        }
    }

    public class EliminateTokenSkill : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            token.cooldowmSkill = int.MinValue;
            
            GameDisplay gameDisplay=new GameDisplay();

            gameDisplay.ShowGame(maze,"This token won't be able to use his skill no more");
            
            Thread.Sleep(1800);
        }
    }

    public class StuckToken : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            token.StuckTurns = 3;

            GameDisplay gameDisplay=new GameDisplay();
            
            gameDisplay.ShowGame(maze,"This token is stuck for three turns");
            
            Thread.Sleep(1000);
        }
    }

    public class HealingToken : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay=new GameDisplay();

            int life = token.Health;

            life += 3;

            token.Health = life;

            gameDisplay.ShowGame(maze,token.name +" receives 3 life points");
            
            Thread.Sleep(1000);

        }
    }
}