using System.Text;
using Spectre.Console;

namespace MazeRunners
{
    class Program
    {
        static void Main(string[]args)
        {
                Game game = new Game();

                UsefulMethods usefulMethods = new UsefulMethods();

                string playerOneName = usefulMethods.NoNullName("Introduzca el nombre del primer jugador");

                string playerTwoName = usefulMethods.NoNullName("Introduzca el nombre del primer jugador");

                //Creando los players
                Player playerOne = new Player(playerOneName);

                Player playerTwo = new Player(playerTwoName);

                //iniciando el juego
                game.StartGame(playerOne,playerTwo);
        }
    }
}