using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public abstract class Tokens
    {
        Player player;

        Cell cell;

        string name;

        public abstract void TokenMove();

        public abstract void TokenSkill();

        public Tokens(string name)
        {
            this.name = name;
        }
    }


    public class TrapIgnore : Tokens
    {

        public override void TokenMove()
        {
            throw new NotImplementedException();
        }

        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }
        public TrapIgnore(string name) : base(name)
        {
            System.Console.WriteLine("called the constructor");
        }
    }

    public class ObstacleDestroyerToken : Tokens
    {

        public override void TokenMove()
        {
            throw new NotImplementedException();
        }

        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }
        public ObstacleDestroyerToken(string name) : base(name)
        {
            System.Console.WriteLine("called the constructor");
        }
    }

    public class FlashToken : Tokens
    {

        public override void TokenMove()
        {
            throw new NotImplementedException();
        }

        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }
        public FlashToken(string name) : base(name)
        {
            System.Console.WriteLine("called the constructor");
        }
    }

    public class TeleportToken : Tokens
    {

        public override void TokenMove()
        {
            throw new NotImplementedException();
        }

        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }
        public TeleportToken(string name) : base(name)
        {
            System.Console.WriteLine("called the constructor");
        }
    }

    public class BloquerToken : Tokens
    {

        public override void TokenMove()
        {
            throw new NotImplementedException();
        }

        public override void TokenSkill()
        {
            throw new NotImplementedException();
        }
        public BloquerToken(string name) : base(name)
        {
            System.Console.WriteLine("called the constructor");
        }
    }
}