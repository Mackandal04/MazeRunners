using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace MazeRunners
{
    public class GameDisplay
    {
        //Para mostrar el estado del laberinto junto a un texto seleccionado
        public void ShowGame(Maze maze, string message)
            {
                UsefulMethods useful = new UsefulMethods();

                var layout = new Layout("Root").SplitColumns
                (
                    new Layout("Left").Ratio(3),
                    new Layout("Right").SplitRows(new Layout("Top"),new Layout("Middle"), new Layout("Bottom")).Ratio(1)
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
                    )
                    .Header("[green bold]Board[/]")
                    .Expand()
                    .RoundedBorder()
                    .BorderColor(Color.Blue)//BlueViolet //CadetBlue y _1 //Chartreuse1
                );

                layout["Top"].Update
                (
                    new Panel
                    (
                        Align.Center
                        (
                            new Markup("[blue bold]Menu[/]"),
                            VerticalAlignment.Middle
                        )
                    )
                    .Header("[bold yellow]Options[/]")
                    .RoundedBorder()
                    .BorderColor(Color.Yellow)
                    .Expand()
                );

                layout["Bottom"].Update
                (
                    new Panel
                    (
                        Align.Left
                        (
                            new Markup("[green]Points:[/]0\n[red]Health:[/]7\n[cyan]Skill:[/] NormalToken"),
                            VerticalAlignment.Top
                        )
                    )
                    .Header("[bold cyan]Player's Stade[/]")
                    .RoundedBorder()
                    .BorderColor(Color.Cyan1)
                    .Expand()
                );

                layout["Middle"].Update
                (
                    new Panel
                    (
                        Align.Left
                        (
                            new Markup("[white bold]" + message + "[/]"),
                            VerticalAlignment.Top
                        )
                    )
                    .Header("[bold magenta]Message[/]")
                    .RoundedBorder()
                    .BorderColor(Color.Magenta1)
                    .Expand()
                );

                AnsiConsole.Write(layout);
            }
    }
}