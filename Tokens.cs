using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace MazeRunners
{
    public abstract class Tokens:Cell
    {
        //Player player;
        public string name;
        public int Health{get;set;} = 7;
        public int StuckTurns{get;set;} = 0;
        public int myX{get;set;}
        public int myY{get;set;}
        public int cooldowmSkill = 6;
        public abstract void TokenSkill(Maze maze);

        public Tokens(string name,int myX, int myY)
        {
            this.name = name;

            this.myX = myX;

            this.myY = myY;
        }
    }

    public class NormalToken : Tokens
    {
        public NormalToken(string name, int myX, int myY) : base(name, myX, myY)
        {

        }

        public override void Show()
        {}

        public override void TokenSkill(Maze maze)
        {
            System.Console.WriteLine("This token has no skill");
        } 
    }


    public class TrapDeleteToken : Tokens
    {
        public TrapDeleteToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }
        public override void TokenSkill(Maze maze)
        {
            DeleteTrapToken deleteTrapToken = new DeleteTrapToken();

            deleteTrapToken.ActivateSkill(this,maze);
        }

        public override void Show()
        {}
    }

    public class ObstacleToken : Tokens
    {
        public ObstacleToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }
        public override void TokenSkill(Maze maze)
        {
            BlockerToken blockerToken = new BlockerToken();

            blockerToken.ActivateSkill(this,maze);
        }

        public override void Show()
        {}
    }

    public class FlashToken : Tokens
    {
        public FlashToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }
        public override void TokenSkill(Maze maze)
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {}
    }

    public class TeleportToken : Tokens
    {
        public TeleportToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }
        public override void TokenSkill(Maze maze)
        {
            if(cooldowmSkill>=6)
            {
                Teleport teleport = new Teleport();

                teleport.ActivateSkill(this,maze);//Le pasamos el token actual junto con el maze
            }

            else
                System.Console.WriteLine("Skill is not charge yet");
        }

        public override void Show()
        {}
    }

    public class BloquerToken : Tokens
    {
        public BloquerToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }

        public override void TokenSkill(Maze maze)
        {
            BlockerToken blockerToken = new BlockerToken();

            blockerToken.ActivateSkill(this,maze);
        }

        public override void Show()
        {}
    }
}