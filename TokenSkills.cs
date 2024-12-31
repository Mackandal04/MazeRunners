using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public abstract class TokenSkills
    {
        public abstract void ActivateSkill(Tokens token, Maze maze);

        public bool CoolDownEnd(int cooldownSkill)
        {
            if(cooldownSkill>=6)
                return true;

            else
                return false;
        }

        public bool isInAValidRange(int maxRange,int cordX,int cordY, int newCordX, int newCordY)
        {
            int range = (int)Math.Sqrt(Math.Pow(newCordX-cordX,2)+Math.Pow(newCordY-cordY,2));

            if(range<= maxRange)
                return true;

            else
                return false;
        }
    }

    public class Teleport : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            int maxRange = 4;

            if(CoolDownEnd(token.cooldowmSkill))
            {
                System.Console.WriteLine("Teleporting the token");

                if(TeleportSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;

                    System.Console.WriteLine("Token teleport's skill was successfully realized");
                }

                else
                    System.Console.WriteLine("Something was wrong");
            }

            else
                System.Console.WriteLine("you can't do this, not yet");
        }

        bool TeleportSuccessfully(int maxRange, Tokens token, Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            for (int i = 0; i <= maxRange; i++)
            {
                for (int j = 0; j <= maxRange; j++)
                {
                    int teleportToX = token.myX + i;

                    int teleportToY = token.myY + j;

                    if(usefulMethods.isAValidFreeCell(teleportToX,teleportToY,maze.maze) && maze.IsAValidMaze(teleportToX,teleportToY))
                    {
                        maze.maze[token.myX,token.myY] = new FreeCell();

                        maze.maze[teleportToX,teleportToY] = token;

                        token.myX = teleportToX;

                        token.myY = teleportToY;

                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class BlockerToken : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            int maxRange = 2;

            UsefulMethods usefulMethods = new UsefulMethods();

            if(CoolDownEnd(token.cooldowmSkill))
            {
                System.Console.WriteLine("Bloqueando celda");

                if(BlockerSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;

                    System.Console.WriteLine("BlockerTokenSkill was succesfully activated");
                }

                else
                    System.Console.WriteLine("Something was wrong");
            }

            else
                System.Console.WriteLine("you can't do this, not yet");
        }

        bool BlockerSuccessfully(int maxRange, Tokens token, Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            for (int i = -maxRange; i <= 0; i++)
            {
                for (int j = -maxRange; j <= 0; j++)
                {
                    int blockerOnX = token.myX + i;

                    int blockerOnY = token.myY + j;

                    if(usefulMethods.isAValidFreeCell(blockerOnX,blockerOnY,maze.maze))
                    {
                        maze.maze[blockerOnX,blockerOnY] = new ObstaclesCell();

                        return true;
                    }
                }
            }

            return false;
        }
    }

    public class DeleteTrapToken : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            int maxRange = 2;

            if(CoolDownEnd(token.cooldowmSkill))
            {
                System.Console.WriteLine("Looking for traps");

                //So hay una trampa cerca y se pudo eliminar
                if(TrapsFoundSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;

                    System.Console.WriteLine("the Trap was successfully deleted");
                }

                else
                    System.Console.WriteLine("This is not a valid operation");
            }

            else
                System.Console.WriteLine("You can't do this...yet");
        }

        bool TrapsFoundSuccessfully(int maxRange, Tokens token, Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            for (int i = -maxRange; i <= maxRange; i++)
            {
                for (int j = -maxRange; j <= maxRange; j++)
                {
                    int trapCordX = token.myX + i;

                    int trapCordY = token.myY + j;

                    if(usefulMethods.isAValidTrap(trapCordX,trapCordY,maze.maze))
                    {
                        maze.maze[trapCordX,trapCordY] = new FreeCell();

                        return true;
                    }
                }
            }

            return false;
        }
    }
}