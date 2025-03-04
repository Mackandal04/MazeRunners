using System;
using System.Text;

using Spectre.Console;

namespace MazeRunners
{
    public class Maze
    {
        public Cell[,] maze {get;}

        static int high = 35;
        static int width = 35;

        public Maze() //Constructor del tablero tablero
        {
            maze = new Cell[high, width];

            for (int i = 0; i < high; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    maze[i,j] = new Wall(); //Iniciamos todas las casillas con bloques xq es mas facil e intuitivo
                                           //ir abriendo caminos que ir poniendo paredes
                }
            }
        }


        public void MazeGenerator(int CordX, int CordY)//Metodo que genera un tablero valido
        {
            maze[CordX,CordY] = new FreeCell();

            (int, int)[] direcArray = {(0,2),(0,-2),(2,0),(-2,0)};

            Random random = new Random();

            FisherYates(direcArray, random);//reoganizamos el array

            foreach (var(dx,dy) in direcArray)
            {
                int newX = CordX + dx;
                int newY = CordY + dy;

                //Revisar si la nueva posicion esta dentro de los limites y es un Wall
                if(newX>0 && newY >0 && newX< high-1 && newY < width-1 && maze[newX,newY] is Wall)
                {
                    maze[CordX + dx/2, CordY + dy/2] = new FreeCell();

                    MazeGenerator(newX,newY);
                }
            }
        }

        void FisherYates((int,int)[]array, Random random)
        {
            //Metodo de Fisher Yates para organizar un array de forma random
            int n = array.Length;

            (int,int) temp = (0,0);

            for (int i = 0; i < n; i++)
            {
                int j = random.Next(i,n);

                temp = array[i];

                array[i] = array[j];

                array[j] = temp;
            }
        }

        public void AddTrapsAndObstacles(int tableObject)
        {
            //Annadir verificacion, debe ser una entrada par

            int traps = tableObject/2;

            int obstacles = tableObject-traps;
            
            Random random = new Random();

            bool isTrap = true;

            int counter = 0;

            TrapCell[]trapArray = new TrapCell[]
            {
                new TeleportTrap(),
                new DamageTrap(),
                new InvalidateTokenSkillTrap(),
                new StuckTrap(),
                new HealthTrap()
            };

            ObstaclesOrtraps(counter,traps,isTrap,random,trapArray);

            isTrap = false;

            ObstaclesOrtraps(counter,obstacles,isTrap,random,trapArray);
        }
            void ObstaclesOrtraps(int counter,int tableObject,bool IsTrap, Random random, TrapCell[]trapArray)
            {
                int maxCount = high*width;

                for (int j = 0; j < tableObject; j++)
                {
                    while(counter<maxCount)
                    {
                        int CordX = random.Next(2,high-1);

                        int CordY = random.Next(2,width-1);
    
                        if(maze[CordX,CordY] is FreeCell && maze[CordX,CordY] is not Tokens ) // && !(CordX==1 && CordY==1) !(CordX==high-2 && CordY==width-1 )
                        {
                            if(IsTrap)
                            {
                                TrapCell randomTrap = trapArray[random.Next(trapArray.Length)];

                                maze[CordX,CordY] = randomTrap;

                                if(IsAValidCell(high/2,width/2)) //le paso las coordenadas del centro del maze
                                    break;
                                
                                maze[CordX,CordY] = new FreeCell();
                            }

                            else
                            {
                                maze[CordX,CordY] = new ObstaclesCell();

                                if(IsAValidCell(high/2,width/2))
                                    break;

                                maze[CordX,CordY] = new FreeCell();
                            }
                        }

                        else
                            counter++;
                    }
                }
            }


        public bool IsAValidCell(int centerX, int centerY)
        {
            (int,int)[] tokensPositions = new (int, int)[]{(1,1), (high-2,width-2), (1,width-2), (high-2,1)};

            foreach (var item in tokensPositions)
            {
                bool[,]arrive = new bool[high,width]; //Mascara para saber si ya visite una casilla

                if(!DFS(item.Item1,item.Item2,centerX,centerY,arrive))
                {
                    return false;
                }                
            }

            return true;
        }
            public bool DFS(int CordX, int CordY, int centerX, int centerY, bool[,]arrive )//Devuelve true si en su actual estado el laberinto es valido
            {
                if(CordX == centerX && CordY == centerY)
                {
                    return true;
                }

                arrive[CordX,CordY] = true; 
                
                (int, int)[] direc = {(0,1), (0,-1), (1,0), (-1,0)};

                foreach(var item in direc)
                {
                    int newDirecX = CordX+ item.Item1;

                    int newDirecY = CordY+item.Item2;

                    if(ValidCheck(arrive,newDirecX,newDirecY))//Si la nueva casilla es valida, no es muro trampa u obstaculo y no ha sido visitada antes 
                    {
                        if(DFS(newDirecX,newDirecY,centerX,centerY,arrive))
                            return true;
                    }
                }
                return false;
            }
        
        bool ValidCheck(bool[,] arrive,int newDirecX, int newDirecY)
        {
            if(newDirecX>0 && newDirecY>0 && newDirecX<high && newDirecY<width && !arrive[newDirecX,newDirecY] && !(maze[newDirecX,newDirecY] is Wall)&& !(maze[newDirecX,newDirecY] is ObstaclesCell))// && !(maze[newDirecX,newDirecY] is TrapCell) 
                return true;
                
            return false;
        }

        
        public string[,] ConcatMazeWithStringMaze()//Devuelve la matriz de string enlazada al maze
        {
            string[,] stringMaze = new string[high,width];

            for (int i = 0; i < high; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    WriteComponentToString(stringMaze,i,j);
                }
            }

            return stringMaze;
        }

        void WriteComponentToString(string[,] stringMaze,int x, int y) //Metodo que toma cada componente del maze y lo lleva como string a su
        {                                                              //componente correspondiente en el stringMaze
            if(maze[x,y] is FreeCell)
                stringMaze[x,y] = "[cyan]░░[/]";
            

            else if(maze[x,y] is Wall)
                stringMaze[x,y] = "[Silver]██[/]"; //Silver
                
            else if(maze[x,y] is TeleportTrap)
                stringMaze[x,y] = "[blue]██[/]";//[cyan]░░ //[Green]██//🪤 || TT || color-██ //Recordar ponerle el mismo color que el pasillo a las trampas
            
            else if(maze[x,y] is DamageTrap)                                                  //Al final borrar esto y quedarme con TrapCell
                stringMaze[x,y] = "[Red]██[/]"; //[cyan]░░//[Red]██//🪤 || TT || color-██
            
            else if(maze[x,y] is InvalidateTokenSkillTrap)
                stringMaze[x,y] = "[Yellow]██[/]"; //[cyan]░░//[Yellow]██//🪤 || TT || color-██
            
            else if(maze[x,y] is StuckTrap)
                stringMaze[x,y] = "[Purple]██[/]";//[cyan]░░ //[Purple]██//🪤 || TT || color-██

            else if(maze[x,y] is HealthTrap)
                stringMaze[x,y] = "[Green]██[/]";//[cyan]░░ //[Purple]██//🪤 || TT || color-██

            else if(maze[x,y] is ExitCell)
                stringMaze[x,y] = "[white]██[/]";
            
            else if(maze[x,y] is ObstaclesCell)
                stringMaze[x,y] = "[black]██[/]"; // OO || color-██ //CadetBlue_1
            
            else if(maze[x,y] is NormalToken)
                stringMaze[x,y] = "[cyan]⚡[/]"; //⇯░||☬░||⚡||⧖░||⚜️ ||⇶░
            
            else if(maze[x,y] is TeleportToken)
                stringMaze[x,y] = "[cyan]☬░[/]";

            else if(maze[x,y] is FlashToken)
                stringMaze[x,y] = "[Red]⧖░[/]";

            else if(maze[x,y] is WallDestroyerToken)
                stringMaze[x,y] = "[Red]KK[/]";

            else if(maze[x,y] is TrapDeleteToken)
                stringMaze[x,y] = "[cyan]⇶░[/]";

            else if (maze[x,y] is ObstacleToken)
                stringMaze[x,y] = "[cyan]⇯░[/]";
        }

        public void AddTokens(List<Tokens> playerOneTokens,List<Tokens> playerTwoTokens, int high, int width)
        {
            int centerX = high/2;

            int centerY = width/2;
            
            maze[centerX,centerY] = new ExitCell();

            for (int i = 0; i < playerOneTokens.Count; i++)
            {
                maze[playerOneTokens[i].myX,playerOneTokens[i].myY] = playerOneTokens[i];
            }

            for (int j = 0; j < playerTwoTokens.Count; j++)
            {
                maze[playerTwoTokens[j].myX,playerTwoTokens[j].myY] = playerTwoTokens[j];
            }
        }

        public void MoveToken(Tokens token,Player player, Maze maze, string message)
            {
                GameDisplay gameDisplay=new GameDisplay();

                if(token is FlashToken)
                    token.TurnsLeft = 16;//efecto pasivo de un flashToken

                else
                    token.TurnsLeft = 8;

                UsefulMethods usefulMethods = new UsefulMethods();

                int n = maze.maze.GetLength(0);

                int m = maze.maze.GetLength(1);

                //Diccionario que asocia la letra con su direccion correspondiente
                Dictionary<char,(int CordX, int CordY)> mazeDirec = new Dictionary<char, (int CordX, int CordY)>
                {
                    {'w',(-1,0)},
                    {'s',(1,0)},
                    {'a',(0,-1)},
                    {'d',(0,1)},
                };

                while(token.TurnsLeft>0)
                {    
                    Console.Clear();//Refrescar la pantalla

                    gameDisplay.ShowGame(maze,message,token,player);

                    char letter = Console.ReadKey().KeyChar;
                    
                    if(letter== 'q')
                    {
                        gameDisplay.ShowGame(maze,"[bold yellow]Saliendo del juego...[/]");
                        
                        Console.ReadKey();

                        gameDisplay.ShowGame(maze,"[bold yellow]Que tengan un buen dia ;) [/]");

                        Console.ReadKey();

                        Console.Clear();

                        //Cierra la el programa manualmente, 0 indica que termino correctamente
                        Environment.Exit(0);
                    }
                    
                    if(letter == 'h')
                    {
                        RulesDisplay rulesDisplay = new RulesDisplay();

                        Console.Clear();

                        rulesDisplay.ShowRules();

                        Console.ReadKey();

                        Console.Clear();
                    }
    
                    if(letter=='k')
                    {
                        if(token.Health<=4)
                        {
                            token.invalidateSkill = 1;

                            gameDisplay.ShowGame(maze,token.name + "[bold yellow] se encuentra muy debil como para utilizar su habilidad[/]");
                                
                            Console.ReadKey();
                        }


                        else if(token.invalidateSkill!=0)
                        {
                            gameDisplay.ShowGame(maze,"[bold yellow]Esta ficha no podra utilizar su habilidad durante...[/]");

                            Console.ReadKey();

                            gameDisplay.ShowGame(maze,token.invalidateSkill + " turnos");

                            Console.ReadKey();
                        }

                        else
                        {
                            token.TokenSkill(maze);
                            continue;
                        }
                    }
                        
                    if(mazeDirec.ContainsKey(letter))
                    {
                        (int CordX,int CordY) = mazeDirec[letter];
                        
                        int newCordX = token.myX + CordX;

                        int newCordY = token.myY + CordY;

                        if(token.StuckTurns>0)
                        {
                            gameDisplay.ShowGame(maze,"[bold yellow]Su ficha esta atrapada por " + token.StuckTurns + " turnos ![/]");
                            
                            token.StuckTurns--;

                            token.TurnsLeft--;
                            
                            Console.ReadKey();
                            
                            continue;
                        }

                        if(usefulMethods.isAValidMove(newCordX,newCordY,maze.maze))
                        {
                            maze.maze[token.myX,token.myY] = new FreeCell();//Haz libre la celda en que estba el token

                            token.myX = newCordX;

                            token.myY = newCordY;

                            if(maze.maze[newCordX,newCordY] is ExitCell)
                            {
                                gameDisplay.ShowGame(maze,"[green] Su ficha ha llegado a la salida !!![/]");

                                Console.ReadKey();

                                maze.maze[token.myX,token.myY] = new FreeCell();
                                
                                maze.maze[newCordX,newCordY] = new ExitCell();

                                player.RemoveToken(token);

                                if(player.playerTokens.Count==0)
                                {
                                    gameDisplay.ShowGame(maze,"[green]El ganador es...[/]");

                                    Console.ReadKey();

                                    gameDisplay.ShowGame(maze,player.name + "!!!!!!!!");
                                    
                                    Console.ReadKey();

                                    return;
                                }

                                break;

                            }

                            else if(maze.maze[newCordX,newCordY] is TrapCell trap)
                            {
                                gameDisplay.ShowGame(maze,"[bold yellow]Trampa activada[/]");

                                Console.ReadKey();

                                if(maze.maze[newCordX,newCordY] is TeleportTrap)
                                {
                                    maze.maze[newCordX,newCordY] = new FreeCell();
                                }

                                trap.ActivateTrapSkill(token,maze);
                            }
                            
                            else if(maze.maze[newCordX,newCordY] is not TeleportTrap && maze.maze[newCordX,newCordY] is not ExitCell)
                            {
                                maze.maze[newCordX,newCordY] = token;//Poner al token en su nueva posicion
                            }

                            if(token.cooldowmSkill <10)
                                token.cooldowmSkill++;

                            token.TurnsLeft--;

                            if(token.invalidateSkill>0)
                                token.invalidateSkill--;
                        }

                        else
                        {
                            gameDisplay.ShowGame(maze,"[red] Ese no es un movimiento valido[/]");
                            
                            Console.ReadKey();
                        }
                    }
                }
            }
    }
}