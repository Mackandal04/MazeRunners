using System.Text;
using Spectre.Console;

namespace MazeRunners
{
    class Program
    {
        static void Main(string[]args)
        {
                int high = 35;//Tope 35
                
                int width = 31; //Tope31

                Maze maze = new Maze(high, width);

                maze.MazeGenerator(1,1);//Siempre empezar en el 1-1

                while(!maze.IsAValidMaze())
                {
                    maze.MazeGenerator(1,1);
                }
                
                maze.AddTrapsAndObstacles(4);
                
                UsefulMethods useful = new UsefulMethods();

            var layout = new Layout("Root").SplitColumns
            (
                new Layout("Left"),
                new Layout("Right").SplitRows(new Layout("Top"), new Layout("Bottom"))
            );

            layout["Left"].Update
            (
                new Panel
                (
                    Align.Center
                    (
                        new Markup(useful.FormatMatrix(maze.ConcatMazeWithStringMaze())),
                        VerticalAlignment.Middle
                    )
                ).Expand()
            );

            layout["Top"].Update
            (
                new Panel
                (
                    Align.Center
                    (
                        new Markup("[blue]Menu[/]"),
                        VerticalAlignment.Middle
                    )
                ).Expand()
            );

            layout["Bottom"].Update
            (
                new Panel
                (
                    Align.Center
                    (
                        new Markup("[blue]PiecesStade[/]"),
                        VerticalAlignment.Middle
                    )
                ).Expand()
            );

            AnsiConsole.Write(layout);
        }
    }
}