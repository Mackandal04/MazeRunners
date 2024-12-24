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
            //Annadir verificacion, deber ser como maximo 35x31
            
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
            //Annadir verificacion, debe empezar en 1,1
            System.Console.WriteLine("Entro al mazeGenerator");

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
            System.Console.WriteLine("Organizo el array de direcciones");

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

        public void AddTrapsAndObstacles(int tableObject)
        {
            //Annadir verificacion, debe ser una entrada par
            System.Console.WriteLine("Entro al AddTrapsAndObstacles");

            int traps = tableObject/2;

            int obstacles = tableObject-traps;
            
            Random random = new Random();

            bool isTrap = true;

            int counter = 0;

            ObstaclesOrtraps(counter,traps,isTrap,random);

            isTrap = false;

            ObstaclesOrtraps(counter,obstacles,isTrap,random);
        }
            void ObstaclesOrtraps(int counter,int tableObject,bool IsTrap, Random random)
            {
                int maxCount = high*width;

                for (int j = 0; j < tableObject; j++)
                {
                    while(counter<maxCount)
                    {
                        int CordX = random.Next(1,high-1);

                        int CordY = random.Next(1,width-1);
    
                        if(maze[CordX,CordY] is FreeCell && !(CordX==1 && CordY==1) && !(CordX==high-2 && CordY==width-1 ) )
                        {
                            if(IsTrap)
                            {
                                maze[CordX,CordY] = new TrapCell();

                                if(IsAValidMaze())
                                    break;
                                else
                                    maze[CordX,CordY] = new FreeCell();
                            }

                            else
                            {
                                maze[CordX,CordY] = new ObstaclesCell();

                                if(IsAValidMaze())
                                    break;
                                else
                                    maze[CordX,CordY] = new FreeCell();
                            }
                        }

                        else
                            counter++;
                    }
                }
            }



        public string[,] ConcatMazeWithStringMaze()//Devuelve la matriz de string enlazada al maze
        {
            //System.Console.WriteLine("Entro al concatMaze");
            string[,] stringMaze = new string[high,width];

            for (int i = 0; i < high; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    WriteComponentToString(stringMaze,i,j);
                }
            }

            return stringMaze;
        }

        void WriteComponentToString(string[,] stringMaze,int x, int y) //Metodo que toma cada componente del maze y lo lleva como string a su
        {                                             //componente correspondiente en el stringMaze
            //System.Console.WriteLine("Entro al Write");

            if(maze[x,y] is FreeCell)
                stringMaze[x,y] = "[cyan]░░[/]";
            

            else if(maze[x,y] is Wall)
                stringMaze[x,y] = "[blue]██[/]";
            

            else if(maze[x,y] is TrapCell)
                stringMaze[x,y] = "TT"; //🪤 || ##
            

            else if(maze[x,y] is ObstaclesCell)
                stringMaze[x,y] = "OO"; //🪨 || XX
            
        }

        public bool IsAValidMaze()
        {
            System.Console.WriteLine("Entro al IsAValidObstacleTrap");
            
            bool[,]arrive = new bool[high,width]; //Mascara para saber si ya visite una casilla

            arrive[1,1] = true; 

            int[]df = {-1,1,0,0};

            int[]dc = {0,0,-1,1};

            (int, int)[] direc = {(0,1), (0,-1), (1,0), (-1,0)};

            bool DFS(int CordX, int CordY)//Devuelve true si en su actual estado el laberinto es valido
            {
                if(CordX==high-2 && CordY == width-1) //Caso base la salida es true
                    return true;

                foreach(var item in direc)
                {
                    int newDirecX = CordX+ item.Item1;

                    int newDirecY = CordY+item.Item2;

                    if(ValidCheck(arrive,newDirecX,newDirecY))//Si la nueva casilla es valida, no es muro trampa u obstaculo y no ha sido visitada antes 
                    {                                           //devuelve true
                        arrive[newDirecX,newDirecY] = true;

                        if(DFS(newDirecX,newDirecY))
                            return true;
                    }
                }

                return false;
            }
            return DFS(1,1);
        }
        bool ValidCheck(bool[,] arrive,int newDirecX, int newDirecY)
            {
                if(newDirecX>0 && newDirecY>0 && newDirecX<high && newDirecY<width && !arrive[newDirecX,newDirecY] && !(maze[newDirecX,newDirecY] is Wall) && !(maze[newDirecX,newDirecY] is TrapCell) && !(maze[newDirecX,newDirecY] is ObstaclesCell))
                    return true;
                
                return false;
            }
    }
}