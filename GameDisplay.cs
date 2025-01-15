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
        public void ShowGame(Maze maze, string message, Tokens actualToken = null, Player actualPlayer = null)
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
                    .BorderColor(Color.Blue)
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

                string playerState = "";

                if(actualToken != null && actualPlayer != null)
                    playerState = "[green]Health:[/] " + actualToken.Health + "\n" + "[cyan]Token Type:[/] " + actualToken.GetType().Name + "\n" + "[yellow]Tokens Left:[/] " + actualPlayer.playerTokens.Count;
                
                else
                    playerState = "[red] ...  [/]";

                layout["Bottom"].Update
                (
                    new Panel
                    (
                        Align.Left
                        (
                            new Markup(playerState),
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