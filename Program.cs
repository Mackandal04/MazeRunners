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
                Player playerOne = new Player();

                Player playerTwo = new Player();

                //iniciando el juego
                game.StartGame(playerOne,playerTwo);
        }
    }
}