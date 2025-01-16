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
                    new Layout("Right").SplitRows( new Layout("Instruccions"),new Layout("Middle"), new Layout("Bottom")).Ratio(1)
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


                string playerState = "";

                if(actualToken != null && actualPlayer != null)
                    playerState =  actualPlayer.name + "\n" + "[green]Token's health:[/] " + actualToken.Health + "\n" + "[cyan]Token Type:[/] " + actualToken.GetType().Name + "\n" + "[blue]Token's position:[/] " + "\n" + "[blue]fila:[/] " + actualToken.myX + "\n" +"[blue]columna:[/] " + actualToken.myY +"\n"+ "[yellow]Tokens Left:[/] " + actualPlayer.playerTokens.Count;
                
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

                string instruccions = "[white bold]Muevete por el maze con las letras w,s,a,d [/]\n" +
                                    "[white bold]Para saltar su turno presiona e [/]\n" +
                                    "[white bold]Para utilizar tu habilidad presiona k [/]\n" +
                                    "[white bold]Para salir del juego presione q [/]";
                
                layout["Instruccions"].Update
                (
                    new Panel
                    (
                        Align.Center
                        (
                            new Markup(instruccions),
                            VerticalAlignment.Middle
                        )
                    )
                    .Header("[bold yellow]Instruccions[/]")
                    .RoundedBorder()
                    .BorderColor(Color.Yellow)
                    .Expand()
                );
                
                AnsiConsole.Write(layout);
            }
    }
}