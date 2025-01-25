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
            GameDisplay gameDisplay=new GameDisplay();
            
            UsefulMethods usefulMethods = new UsefulMethods();

            int maxRange = 14;

            if(CoolDownEnd(token.cooldowmSkill))
            {
                gameDisplay.ShowGame(maze,"Teleporting the token");
                
                Console.ReadKey();

                if(TeleportSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;
                    
                    gameDisplay.ShowGame(maze,"Token teleport's skill was successfully realized");
                    
                    Console.ReadKey();
                }

                else
                {
                    gameDisplay.ShowGame(maze,"Something was wrong");
                    
                    Console.ReadKey();
                }
            }

            else
            {
                gameDisplay.ShowGame(maze,"you can't do this, not yet");
                
                Console.ReadKey();
            }
        }


        bool TeleportSuccessfully(int maxRange, Tokens token, Maze maze)
        {
            UsefulMethods usefulMethods = new UsefulMethods();

            int mazeCenterX = maze.maze.GetLength(0)/2 ;

            int mazeCenterY = maze.maze.GetLength(1)/2 ;

            for (int i = 0; i <= maxRange; i++)
            {
                for (int j = 0; j <= maxRange; j++)
                {
                    int teleportToX = token.myX + i;

                    int teleportToY = token.myY + j;

                    if(usefulMethods.isAValidFreeCell(teleportToX,teleportToY,maze.maze))// && maze.IsAValidCell(maze.maze.GetLength(0)/2, maze.maze.GetLength(1)/2 )
                    {
                        maze.maze[token.myX,token.myY] = new FreeCell();

                        maze.maze[teleportToX,teleportToY] = token;

                        bool [,] mask= new bool [maze.maze.GetLength(0),maze.maze.GetLength(1)];

                        if(maze.IsAValidCell(mazeCenterX, mazeCenterY) && maze.DFS(teleportToX,teleportToY,mazeCenterX,mazeCenterY,mask))
                        {
                            token.myX = teleportToX;

                            token.myY = teleportToY;

                            return true;
                        }

                        maze.maze[teleportToX,teleportToY] = new FreeCell();

                        maze.maze[token.myX, token.myY] = token;
                    }
                }
            }

            return false;
        }
    }


    //Bloquea una casilla del maze
    public class Blocker : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay=new GameDisplay();

            int maxRange = 2;

            UsefulMethods usefulMethods = new UsefulMethods();

            if(CoolDownEnd(token.cooldowmSkill))
            {
                gameDisplay.ShowGame(maze,"Bloqueando celda");
                
                Console.ReadKey();

                if(BlockerSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;

                    gameDisplay.ShowGame(maze,"BlockerTokenSkill was succesfully activated");
                    
                    Console.ReadKey();
                }

                else
                {
                    gameDisplay.ShowGame(maze,"Something was wrong");
                
                    Console.ReadKey();

                }
            }

            else
            {
                gameDisplay.ShowGame(maze,"you can't do this, not yet");
                
                Console.ReadKey();
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

    public class DeleteTrap : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay=new GameDisplay();

            int maxRange = 2;

            if(CoolDownEnd(token.cooldowmSkill))
            {
                gameDisplay.ShowGame(maze,"Looking for traps");
                
                Console.ReadKey();

                //So hay una trampa cerca y se pudo eliminar
                if(TrapsFoundSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;

                    gameDisplay.ShowGame(maze,"the Trap was successfully deleted");
                    
                    Console.ReadKey();
                }

                else
                {
                    gameDisplay.ShowGame(maze,"This is not a valid operation");
                    
                    Console.ReadKey();
                }
            }

            else
            {
                gameDisplay.ShowGame(maze,"You can't do this...yet");
                
                Console.ReadKey();
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

    public class SpeedUp : TokenSkills
    {
        //Aumento de la cantiad de veces que se puede mover esta en MoveToken
        //Habilidad pasiva
        public override void ActivateSkill(Tokens token, Maze maze)
        {}
    }

    public class DestroyWall : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay = new GameDisplay();

            gameDisplay.ShowGame(maze,"Seleccione una direccion para destruir una pared");

            char goTo = Console.ReadKey().KeyChar;

            Dictionary<char,(int CordX,int CordY)> direccions = new Dictionary<char, (int CordX, int CordY)>
            {
                {'w',(-1,0)},
                {'s',(1,0)},
                {'a',(0,-1)},
                {'d',(0,1)}
            };

            if(direccions.ContainsKey(goTo) && CoolDownEnd(token.cooldowmSkill))
            {
                gameDisplay.ShowGame(maze,"Destruyendo muro");
                
                Console.ReadKey();
                
                (int CordX,int CordY) = direccions[goTo];

                int newCordX = token.myX + CordX;

                int newCordY = token.myY + CordY;

                if(maze.maze[newCordX,newCordY] is Wall && newCordX>0 && newCordY >0 && newCordX<maze.maze.GetLength(0)-1 && newCordY<maze.maze.GetLength(1)-1)
                {
                    maze.maze[newCordX,newCordY] = new FreeCell();

                    token.cooldowmSkill = 0;

                    gameDisplay.ShowGame(maze,"Wall was destroyed");
                    
                    Console.ReadKey();
                }
            }

            else
                gameDisplay.ShowGame(maze,"you can't destroy there");
        }
    }
}