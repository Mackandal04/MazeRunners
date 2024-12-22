    using System;
    using System.Collections.Generic;
    using Spectre.Console;

    namespace MazeRunners
    {
        class Program
        {
            static void Main(string[] args)
            {
                int high = 37;
                int width = 37;

                Maze maze = new Maze(high, width);

                maze.MazeGenerator(1,1);//Siempre empezar en el 1-1

                maze.AddTrapsAndObstacles(3,3);
                
                maze.PrintMaze();

            }
        }
    }