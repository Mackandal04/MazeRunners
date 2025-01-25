using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public class UsefulMethods //Aqui van los metodos q no tuve claro donde poner pero q me hacian falta
    {
        public string FormatMatrix( string[,]maze)
        {
            StringBuilder formatedString = new StringBuilder(); //Creando el StringBuilder

            int high = maze.GetLength(0);

            int width = maze.GetLength(1);

            for (int i = 0; i < high; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    //Append metodo de la clase StringBuilder
                    //+ " " luego de maze[i,j]
                    formatedString.Append(maze[i,j]); //Agregar cada componente de la matriz al final del StringBuilder y luego el " "
                }

                formatedString.AppendLine();//Solo hace el salto de linea, pues no tiene elementos para concatenar 
            }

            return formatedString.ToString();//Convierte el StringBuilder a string y lo devuelve
        }

        public bool isAValidMove(int newCordX, int newCordY, Cell[,] maze)//Valida una operacion de movimiento en el maze
        {

            if(newCordX >= 0 && newCordY >= 0 && newCordX < maze.GetLength(0) && newCordY<maze.GetLongLength(1) && maze[newCordX,newCordY] is not Wall && maze[newCordX,newCordY] is not ObstaclesCell )
                return true;

            else
                return false;
        }

        public bool isAValidFreeCell(int newCordX, int newCordY, Cell[,] maze)
        {

            if(newCordX > 0 && newCordY > 0 && newCordX < maze.GetLength(0)-1 && newCordY<maze.GetLongLength(1)-1 && maze[newCordX,newCordY] is FreeCell)
                return true;

            else
                return false;
        }

        public bool isAValidTrap(int newCordX, int newCordY, Cell[,] maze)
        {

            if(newCordX > 0 && newCordY > 0 && newCordX < maze.GetLength(0)-1 && newCordY<maze.GetLongLength(1)-1 && maze[newCordX,newCordY] is TrapCell)
                return true;

            else
                return false;
        }

        public string NoNullName(string message)
        {
            while(true)
            {
                System.Console.WriteLine(message);
                
                string name = System.Console.ReadLine();
                
                if(!string.IsNullOrEmpty(name))
                {
                    return name;
                }

                else
                    System.Console.WriteLine("No es un nombre valido");

            }
        }

        public void TokensPositions(Maze maze,List<(int,int)> positions, Tokens token)
        {
            // int high = maze.maze.GetLength(0);

            // int width = maze.maze.GetLength(1);

            // List<(int,int)> positions = new List<(int, int)>
            // {
            //     (1,1),
            //     (high-2,width-2),
            //     (1,width-2),
            //     (high-2,1)
            // };

            Random random = new Random();
            
            int mazeCord = random.Next(0,positions.Count);

            token.myX = positions[mazeCord].Item1;

            token.myY = positions[mazeCord].Item2;

            positions.RemoveAt(mazeCord);
        }
    }
}