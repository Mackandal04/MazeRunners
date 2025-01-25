using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MazeRunners
{
    public abstract class TrapsSkills
    {
        public abstract void ActivateSkill(Tokens token, Maze maze);
    }

    public class TeleportToBeginning : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            int high = maze.maze.GetLength(0);
            
            int width = maze.maze.GetLength(1);
            
            //Diccionario para segun el token, teletransportarlo a una casilla diferente
            Dictionary<Type,(int CordX,int CordY)> tokensTeleportDestiny = new()
            {
                //typeof es xq cada token representa un tipo diferente de objeto
                { typeof(NormalToken) ,(1,1)},
                { typeof(TeleportToken) ,(high-2,width-2)},
                { typeof(TrapDeleteToken) ,(1,width-2)},
                { typeof(ObstacleToken) ,(high-2,1)},
                { typeof(FlashToken) ,(1,1)},
                { typeof(WallDestroyerToken) ,(high-2,width-2)}
            };

            GameDisplay gameDisplay=new GameDisplay();

            gameDisplay.ShowGame(maze,"[blue]Teleporting the token[/]");
            Console.ReadKey();
            //Thread.Sleep(1000);

            //es true si el tipo del token esta en el diccionario
            if(tokensTeleportDestiny.ContainsKey(token.GetType()))
            {
                //asigna a destiny las coordenadas que le corresponde al token (int,int)
                var destiny = tokensTeleportDestiny[token.GetType()]; 

                maze.maze[token.myX,token.myY] = new FreeCell();

                token.myX = destiny.CordX;

                token.myY = destiny.CordY;

                maze.maze[token.myX, token.myY] = token;
                
                gameDisplay.ShowGame(maze,"[green]TokenTeleportedSkill successfully activated[/]");
                Console.ReadKey();
            }
        }
    }

    public class TakeDamage : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay=new GameDisplay();

            int life = token.Health;

            life -= 3;

            token.Health = life;

            gameDisplay.ShowGame(maze,token.name +"[red] takes 3 of damage[/]");
            
            Console.ReadKey();
        }
    }

    public class EliminateTokenSkill : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            token.cooldowmSkill = int.MinValue;
            
            GameDisplay gameDisplay=new GameDisplay();

            gameDisplay.ShowGame(maze,"[red] This token won't be able to use his skill no more[/]");

            if(token is FlashToken)
                gameDisplay.ShowGame(maze,"But Flash is Faster, so the trap had no effect !");
            
            Console.ReadKey();
        }
    }

    public class StuckToken : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            token.StuckTurns = 3;

            GameDisplay gameDisplay=new GameDisplay();
            
            gameDisplay.ShowGame(maze,"[bold yellow]This token is stuck for three turns ! [/]");
            
            Console.ReadKey();
        }
    }

    public class HealingToken : TrapsSkills
    {
        public override void ActivateSkill(Tokens token, Maze maze)
        {
            GameDisplay gameDisplay=new GameDisplay();

            int life = token.Health;

            life += 3;

            token.Health = life;

            gameDisplay.ShowGame(maze,token.name +"[green] receives 3 life points[/]");
            
            Console.ReadKey();
        }
    }
}