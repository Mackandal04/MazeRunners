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
            //flag indica que token es el q se debe mover de los tokens disponibles x el player

                string message = "Es el turno de " + player.playerTokens[flag].name; 

                maze.MoveToken(player.playerTokens[flag], maze , message);

                player.isYourTurn = false;
        }
    }
}