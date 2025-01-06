using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public abstract class TokenSkills
    {
        public abstract void ActivateSkill(Tokens token, Maze maze);

        public bool CoolDownEnd(int cooldownSkill) //Tiempo de enfriamiento de cada skill
        {
            if(cooldownSkill>=6)
                return true;

            else
                return false;
        }

        //Verifica si es posible utilizar la skill en un rango determinado
        public bool isInAValidRange(int maxRange,int cordX,int cordY, int newCordX, int newCordY)
        {
            //formula de distancia distancia entre dos puntos adaptada a dos casillas 
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
            Game game = new Game();
            
            UsefulMethods usefulMethods = new UsefulMethods();

            int maxRange = 4;

            if(CoolDownEnd(token.cooldowmSkill))
            {
                game.ShowGame(maze,"Teleporting the token");
                
                Thread.Sleep(1000);

                if(TeleportSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;
                    
                    game.ShowGame(maze,"Token teleport's skill was successfully realized");
                    
                    Thread.Sleep(1800);
                }

                else
                {
                    game.ShowGame(maze,"Something was wrong");
                    
                    Thread.Sleep(1000);
                }
            }

            else
            {
                game.ShowGame(maze,"you can't do this, not yet");
                
                Thread.Sleep(1000);
            }
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

                    if(usefulMethods.isAValidFreeCell(teleportToX,teleportToY,maze.maze) && maze.IsAValidCell(maze.maze.GetLength(0)/2, maze.maze.GetLength(1)/2 ))
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


    //Bloquea una casilla del maze
    public class BlockerToken : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            int maxRange = 2;

            Game game = new Game();

            UsefulMethods usefulMethods = new UsefulMethods();

            if(CoolDownEnd(token.cooldowmSkill))
            {
                game.ShowGame(maze,"Bloqueando celda");
                
                Thread.Sleep(1000);

                if(BlockerSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;

                    game.ShowGame(maze,"BlockerTokenSkill was succesfully activated");
                    
                    Thread.Sleep(1600);
                }

                else
                {
                    game.ShowGame(maze,"Something was wrong");
                
                    Thread.Sleep(1000);
                }
            }

            else
            {
                game.ShowGame(maze,"you can't do this, not yet");
                
                Thread.Sleep(1000);
            }
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
            Game game = new Game();

            int maxRange = 2;

            if(CoolDownEnd(token.cooldowmSkill))
            {
                game.ShowGame(maze,"Looking for traps");
                
                Thread.Sleep(1000);
                //So hay una trampa cerca y se pudo eliminar
                if(TrapsFoundSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;

                    game.ShowGame(maze,"the Trap was successfully deleted");
                    
                    Thread.Sleep(1500);
                }

                else
                {
                    game.ShowGame(maze,"This is not a valid operation");
                    
                    Thread.Sleep(1000);
                }
            }

            else
            {
                game.ShowGame(maze,"You can't do this...yet");
                
                Thread.Sleep(1000);
            }
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

                bool TryingTrapsFoundSuccessfully(int maxRange, Tokens token, Maze maze)
        {
            //Nueva busqueda en espiral de trampas
            throw new NotImplementedException();
        }
    }
}