using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Spectre.Console;

namespace MazeRunners
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
    public class ObstaclesCell : Cell
    {
        public override void Show()
        {
            AnsiConsole.Markup("xx");
            //AnsiConsole.Markup("🪨");
        }
    }
}