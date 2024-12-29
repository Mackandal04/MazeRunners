using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public class UsefulMethods
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

            if(newCordX >= 0 && newCordY >= 0 && newCordX < maze.GetLength(0) && newCordY<maze.GetLongLength(1) && maze[newCordX,newCordY] is FreeCell)
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
    }
}