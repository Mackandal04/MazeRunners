using System;

using Spectre.Console;

namespace NewProjectMazeRunners
{
    public abstract class Cell//Tipo de la matriz q reoresenta el tablero
    {
        public abstract void Show();
    }

    public class FreeCell: Cell
    {
        public override void Show()
        {
            AnsiConsole.Markup("[cyan]░░[/]");
        }
    }

    public class Wall : Cell
    {
        public override void Show()
        {
            AnsiConsole.Markup("[blue]██[/]");
        }
    }

    public class TrapCell : Cell
    {
        public override void Show()
        {
            AnsiConsole.Markup("🪤");
        }
    } 

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