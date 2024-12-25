using System.Text;
using Spectre.Console;

namespace MazeRunners
{
    class Program
    {
        static void Main(string[]args)
        {
                Game game = new Game();

                NormalToken normalToken = new NormalToken("Jarvis",1,1);

                game.StartGame(normalToken);
        }
    }
}