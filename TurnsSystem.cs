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

            if(player.playerTokens.Count==0)
            {
                gameDisplay.ShowGame(maze,"El ganador es " + player.name);
                Console.ReadKey();
                //Thread.Sleep(1300);

                return;
            }

            if(player.playerTokens.Count==1)
            {
                flag = 0;
            }

            //flag indica que token es el q se debe mover de los tokens disponibles x el player

            string message = "Es el turno de " + player.name +"\n"+ "[yellow]Tokens Left:[/] " + player.playerTokens.Count; 

            maze.MoveToken(player.playerTokens[flag],player, maze , message);

            player.isYourTurn = false;
        }
    }
}