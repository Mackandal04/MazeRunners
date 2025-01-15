using System.Text;
using Spectre.Console;

namespace MazeRunners
{
    class Program
    {
        static void Main(string[]args)
        {
                Game game = new Game();

                //Creando los players
                Player playerOne = new Player("Mauro");

                Player playerTwo = new Player("Mackandal");

                //iniciando el juego
                game.StartGame(playerOne,playerTwo);
        }
    }
}