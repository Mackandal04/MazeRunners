using System;

using Spectre.Console;

namespace MazeRunners
{
    public class Maze
    {
        Cell[,] maze;

        int high;
        int width;

        public Maze(int high, int width) //Metodo que crea el tablero
        {
            this.high = high;
            this.width = width;

            maze = new Cell[high, width];

            for (int i = 0; i < high; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    maze[i,j] = new Wall(); //Iniciamos todas las casillas con bloques xq es mas facil e intuitivo
                                           //ir abriendo caminos que ir poniendo paredes
                }
            }
        }


        public void MazeGenerator(int CordX, int CordY)//Metodo que genera un tablero valido
        {
            maze[CordX,CordY] = new FreeCell();

            (int, int)[] direcArray = {(0,2),(0,-2),(2,0),(-2,0)};

            Random random = new Random();

            FisherYates(direcArray, random);//reoganizamos el array

            foreach (var(dx,dy) in direcArray)
            {
                int newX = CordX + dx;
                int newY = CordY + dy;

                //Revisar si la nueva posicion esta dentro de los limites y es un Wall
                if(newX>0 && newY >0 && newX< high-1 && newY < width-1 && maze[newX,newY] is Wall)
                {
                    maze[CordX + dx/2, CordY + dy/2] = new FreeCell();

                    MazeGenerator(newX,newY);
                }
            }

            maze[1,0] = new FreeCell();

            maze[high-2,width-1] = new FreeCell();
        }

        void FisherYates((int,int)[]array, Random random)
        {
            //Metodo de Fisher Yates para organizar un array de forma random
            int n = array.Length;

            (int,int) temp = (0,0);

            for (int i = 0; i < n; i++)
            {
                int j = random.Next(i,n);

                temp = array[i];

                array[i] = array[j];

                array[j] = temp;
            }
        }

        public void AddTrapsAndObstacles(int traps, int obstacles)
        {
            Random random = new Random();

            for (int i = 0; i < traps; i++)
            {
                while (true)//Mientras la casilla no sea valida
                {
                    int CordX = random.Next(0,high-1); // [) Evitar los bordes
                    
                    int CordY = random.Next(0,width-1);

                    if(maze[CordX,CordY] is FreeCell && !(CordX==1 && CordY==1) && !(CordX==high-2 && CordY==width-2 ))
                    {
                        maze[CordX,CordY] = new TrapCell();
                        break;
                    }
                }
                
            }

            for (int j = 0; j < obstacles; j++)
            {
                while(true)
                {
                    int CordX = random.Next(1,high-1);

                    int CordY = random.Next(1,width-1);

                    if(maze[CordX,CordY] is FreeCell && !(CordX==1 && CordY==1) && !(CordX==high-2 && CordY==width-2 ))
                    {
                        maze[CordX,CordY] = new Obstacles(); //Obstaculo no era un bool ?
                        break;
                    }
                }
            }
        }

        public void PrintMaze()
        {
            System.Console.WriteLine("printing maze....");
            for (int i = 0; i < high; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    maze[i,j].Show();
                }

                Console.WriteLine();
            }
        }
    }
}