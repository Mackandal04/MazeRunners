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

                TeleportToken teleportToken = new TeleportToken("Ultron",1,1);

                TrapDeleteToken trapDeleteToken = new TrapDeleteToken("Batman",1,1);
                
                ObstacleToken obstacleToken = new ObstacleToken("Hulk",1,1);

                game.StartGame(obstacleToken);
        }
    }
}