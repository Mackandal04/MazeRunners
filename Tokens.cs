using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace MazeRunners
{
    public abstract class Tokens:Cell
    {
        public string name;
        public string icon;
        public int Health{get;set;} = 10;
        public int TurnsLeft = 8;
        public int StuckTurns{get;set;} = 0;
        public int invalidateSkill{get;set;} = 0;
        public int myX{get;set;}
        public int myY{get;set;}
        public int cooldowmSkill = 10;
        public abstract void TokenSkill(Maze maze);

        public Tokens(string name,string icon,int myX, int myY)
        {
            this.name = name;

            this.icon = icon;

            this.myX = myX;

            this.myY = myY;
        }
    }

    public class NormalToken : Tokens
    {
        public NormalToken(string name, string icon,int myX, int myY) : base(name,icon, myX, myY)
        {

        }

        public override void Show()
        {}

        public override void TokenSkill(Maze maze)
        {
            JustHandsome justHandsome = new JustHandsome();

            justHandsome.ActivateSkill(this,maze);
        } 
    }


    public class TrapDeleteToken : Tokens
    {
        public TrapDeleteToken(string name, string icon,int myX, int myY) : base(name,icon, myX, myY)
        {
        }
        public override void TokenSkill(Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            if(usefulMethods.CheckCoolDownToken(maze,this))
            {
                DeleteTrap deleteTrap = new DeleteTrap();

                deleteTrap.ActivateSkill(this,maze);
            }
        }

        public override void Show()
        {}
    }

    public class ObstacleToken : Tokens
    {
        public ObstacleToken(string name, string icon,int myX, int myY) : base(name,icon, myX, myY)
        {
        }
        public override void TokenSkill(Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            if(usefulMethods.CheckCoolDownToken(maze,this))
            {
                Blocker blocker = new Blocker();

                blocker.ActivateSkill(this,maze);
            }
        }

        public override void Show()
        {}
    }

    public class FlashToken : Tokens
    {
        public FlashToken(string name, string icon,int myX, int myY) : base(name,icon, myX, myY)
        {
        }
        public override void TokenSkill(Maze maze)
        {
            SpeedUp speedUp = new SpeedUp();
            
            speedUp.ActivateSkill(this,maze);
        }

        public override void Show()
        {}
    }

    public class TeleportToken : Tokens
    {
        public TeleportToken(string name, string icon,int myX, int myY) : base(name,icon, myX, myY)
        {
        }
        public override void TokenSkill(Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            if(usefulMethods.CheckCoolDownToken(maze,this))
            {
                Teleport teleport = new Teleport();

                teleport.ActivateSkill(this,maze);//Le pasamos el token actual junto con el maze
            }
        }

        public override void Show()
        {}
    }

    public class WallDestroyerToken : Tokens
    {
        public WallDestroyerToken(string name, string icon,int myX, int myY) : base(name,icon, myX, myY)
        {
        }

        public override void TokenSkill(Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();
            
            if(usefulMethods.CheckCoolDownToken(maze,this))
            {
                DestroyWall destroyWall = new DestroyWall();

                destroyWall.ActivateSkill(this,maze);
            }
        }
        public override void Show()
        {}
    }
}