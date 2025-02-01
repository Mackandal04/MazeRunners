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

    public class JustHandsome : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay = new GameDisplay();

            gameDisplay.ShowGame(maze,"[bold yellow]Lo siento...solo soy poderosamente bello[/]");

            Console.ReadKey();
        }
    }

    public class Teleport : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay=new GameDisplay();
            
            UsefulMethods usefulMethods = new UsefulMethods();

            int maxRange = 14;

            if(TeleportSuccessfully(maxRange,token,maze) && CoolDownEnd(token.cooldowmSkill))
            {
                token.cooldowmSkill = 0;

                gameDisplay.ShowGame(maze,"[green]El teletransporte fue realizado con exito ![/]");
                
                Console.ReadKey();
            }

            else
            {
                gameDisplay.ShowGame(maze,"[red]Algo salio mal[/]");
                
                Console.ReadKey();
            }
        }

        bool TeleportSuccessfully(int maxRange, Tokens token, Maze maze)
        {
            GameDisplay gameDisplay = new GameDisplay();
            
            gameDisplay.ShowGame(maze, "[bold yellow]La ficha está intentando teletransportarse...[/]");
            
            Console.ReadKey();

            UsefulMethods usefulMethods = new UsefulMethods();
            
            int mazeCenterX = maze.maze.GetLength(0) / 2;
            
            int mazeCenterY = maze.maze.GetLength(1) / 2;

            List<(int CellX,int CellY)> teleportCells = new List<(int X,int Y)>();//lista de celdas para teletransportarse

            for (int i = -maxRange; i <= maxRange; i++)
            {
                for (int j = -maxRange; j <= maxRange; j++)
                {
                    int teleportToX = token.myX + i;
                    
                    int teleportToY = token.myY + j;

                    if (teleportToX >= 0 && teleportToX < maze.maze.GetLength(0) && teleportToY >= 0 && teleportToY < maze.maze.GetLength(1) && usefulMethods.isAValidFreeCell(teleportToX, teleportToY, maze.maze))
                    {
                        // Verificar si el centro es accesible desde esta celda
                        bool[,] mask = new bool[maze.maze.GetLength(0), maze.maze.GetLength(1)];
                        
                        if (maze.DFS(teleportToX, teleportToY, mazeCenterX, mazeCenterY, mask))
                            teleportCells.Add((teleportToX, teleportToY));//Si permite llegar al medio, agregala a la lista
                    }
                }
            }

            if (teleportCells.Count == 0)
            {
                gameDisplay.ShowGame(maze,"[bold red]No es posible teletransportarse ahora mismo[/]");
                
                return false;
            }

            Random random = new Random();
            
            var goToThisCell = teleportCells[random.Next(teleportCells.Count)];

            maze.maze[token.myX, token.myY] = new FreeCell();
            
            maze.maze[goToThisCell.CellX, goToThisCell.CellY] = token;
            
            token.myX = goToThisCell.CellX;
            
            token.myY = goToThisCell.CellY;
            
            return true;
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
                gameDisplay.ShowGame(maze,"[bold yellow]Bloqueando celda...[/]");
                
                Console.ReadKey();

                if(BlockerSuccessfully(maxRange,token,maze))
                {
                    token.cooldowmSkill = 0;

                    gameDisplay.ShowGame(maze,"[green]El token bloque la celda con exito ![/]");
                    
                    Console.ReadKey();
                }

                else
                {
                    gameDisplay.ShowGame(maze,"[red]Algo salio mal :( [/]");
                
                    Console.ReadKey();

                }
            }

            else
            {
                gameDisplay.ShowGame(maze,"[bold yellow] Aun no puedes hacer esto[/]");
                
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

            gameDisplay.ShowGame(maze,"[bold yellow]Seleccione la trampa que desee eliminar[/]");

            char deleteThis = Console.ReadKey().KeyChar;

            Dictionary<char,(int CordX,int CordY)> direccions = new Dictionary<char, (int CordX, int CordY)>
            {
                {'w',(-1,0)},
                {'s',(1,0)},
                {'a',(0,-1)},
                {'d',(0,1)}
            };

            if(direccions.ContainsKey(deleteThis))
            {
                gameDisplay.ShowGame(maze,"[bold yellow]Eliminando trampa...[/]");
                
                Console.ReadKey();
                
                (int CordX,int CordY) = direccions[deleteThis];

                int newCordX = token.myX + CordX;

                int newCordY = token.myY + CordY;

                if(maze.maze[newCordX,newCordY] is TrapCell)
                {
                    maze.maze[newCordX,newCordY] = new FreeCell();

                    token.cooldowmSkill = 0;

                    gameDisplay.ShowGame(maze,"[green]La trampa fue eliminada del campo exitosamente ![/]");
                    
                    Console.ReadKey();
                }

                else
                {
                    gameDisplay.ShowGame(maze,"[bold yellow]Lastimosamente no se pudo completar el proceso[/]");
                    
                    Console.ReadKey();
                }
            }

            else
                gameDisplay.ShowGame(maze,"[red]No puedes eliminar una trampa en esa direccion :( [/]");

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
        {
            GameDisplay gameDisplay = new GameDisplay();

            gameDisplay.ShowGame(maze,"[bold yellow]Eres veloz?...[/]");

            Console.ReadKey();

            gameDisplay.ShowGame(maze,"[green]Francesco es...3 veces veloz jajajaj[/]");

            Console.ReadKey();
        }
    }

    public class DestroyWall : TokenSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay = new GameDisplay();

            gameDisplay.ShowGame(maze,"[bold yellow]Seleccione una direccion para destruir una pared[/]");

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
                gameDisplay.ShowGame(maze,"[bold yellow]Intentando destruir el muro...[/]");
                
                Console.ReadKey();
                
                (int CordX,int CordY) = direccions[goTo];

                int newCordX = token.myX + CordX;

                int newCordY = token.myY + CordY;

                if(maze.maze[newCordX,newCordY] is Wall && newCordX>0 && newCordY >0 && newCordX<maze.maze.GetLength(0)-1 && newCordY<maze.maze.GetLength(1)-1)
                {
                    maze.maze[newCordX,newCordY] = new FreeCell();

                    token.cooldowmSkill = 0;

                    gameDisplay.ShowGame(maze,"[green]El muro fue derrumbado exitosamente ![/]");
                    
                    Console.ReadKey();
                }

                else
                {
                    gameDisplay.ShowGame(maze,"[bold yellow]Lastimosamente no se pudo completar el proceso[/]");
                    
                    Console.ReadKey();
                }
            }

            else
                gameDisplay.ShowGame(maze,"[red]No puedes romper en esa direccion:( [/]");
        }
    }
}