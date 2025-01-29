using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public class TurnsSystem//Aqui va lo relacionado con los turnos
    {
        public void PlayerTurn(int flag,Player player, Maze maze)
        {
            GameDisplay gameDisplay = new GameDisplay();

            string message = "Es el turno de " + player.name +"\n"+ "[bold yellow]Fichas restantes:[/] " + player.playerTokens.Count; 

            maze.MoveToken(player.playerTokens[flag],player, maze , message);
        }
    }
}