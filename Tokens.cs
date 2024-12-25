using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace MazeRunners
{
    public abstract class Tokens:Cell
    {
        Player player;

        Cell cell;
        string name;
        public int myX{get;set;}
        public int myY{get;set;}

        public abstract void TokenMove(int CordX, int CordY, Cell[,] maze);

        public abstract void TokenSkill();

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
        {
            AnsiConsole.Markup("[red]⇯⇯[/]");
        }

        public override void TokenMove(int CordX, int CordY, Cell[,] maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            int newCordX = myX + CordX;

            int newCordY = myY + CordY;

            if(usefulMethods.isAValidMove(newCordX,newCordY,maze))
            {
                System.Console.WriteLine("Se actualizo el estado del token");
                myX = newCordX;
                myY = newCordY;
            }
        }

        public override void TokenSkill()
        {}//No tiene skill 
    }


    public class TrapIgnore : Tokens
    {
        public TrapIgnore(string name, int myX, int myY) : base(name, myX, myY)
        {
        }
        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            throw new NotImplementedException();
        }

        public override void TokenMove(int CordX, int CordY, Cell[,] maze)
        {
            throw new NotImplementedException();
        }
    }

    public class ObstacleDestroyerToken : Tokens
    {
        public ObstacleDestroyerToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }
        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            throw new NotImplementedException();
        }

        public override void TokenMove(int CordX, int CordY, Cell[,] maze)
        {
            throw new NotImplementedException();
        }
    }

    public class FlashToken : Tokens
    {
        public FlashToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }
        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            throw new NotImplementedException();
        }

        public override void TokenMove(int CordX, int CordY, Cell[,] maze)
        {
            throw new NotImplementedException();
        }
    }

    public class TeleportToken : Tokens
    {
        public TeleportToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }
        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            throw new NotImplementedException();
        }

        public override void TokenMove(int CordX, int CordY, Cell[,] maze)
        {
            throw new NotImplementedException();
        }
    }

    public class BloquerToken : Tokens
    {
        public BloquerToken(string name, int myX, int myY) : base(name, myX, myY)
        {
        }

        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }

        public override void Show()
        {
            throw new NotImplementedException();
        }

        public override void TokenMove(int CordX, int CordY, Cell[,] maze)
        {
            throw new NotImplementedException();
        }
    }
}