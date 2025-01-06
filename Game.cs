    using System;
    using Spectre.Console;


    namespace MazeRunners
    {
        class Game
        {
            public bool keepGoing;
            public void StartGame(Player playerOne, Player playerTwo)//Debe recibir un array de token q representa los tokens de cada jugador
            {
                
                int high = 31;

                int width = 31;
                
                bool isPlayerOneturn = true;

                
                NormalToken normalToken = new NormalToken("Jarvis",1,1); //1 1

                TeleportToken teleportToken = new TeleportToken("Ultron",high-2,width-2);//-2-2

                TrapDeleteToken trapDeleteToken = new TrapDeleteToken("Batman",1,width-2);
                
                ObstacleToken obstacleToken = new ObstacleToken("Hulk",high-2,1);
                
                playerOne.AddToken(normalToken);

                playerOne.AddToken(teleportToken);

                playerTwo.AddToken(obstacleToken);

                playerTwo.AddToken(trapDeleteToken);

                Maze maze = new Maze(high,high); //Tope39X45

                CreateGame(maze,playerOne.playerTokens,playerTwo.playerTokens,high,width);//Crea el juego por detras

                ShowGame(maze,"Welcome to the game !"); //Muestra el tablero y el estado del juego
                Thread.Sleep(2100);

                ShowGame(maze,"The first player to take all his tokens to exits will be the winner");
                Thread.Sleep(3500);

                ShowGame(maze,"Good luck :) ");
                Thread.Sleep(2300);

                int flag = 0;

                TurnsSystem turnsSystem = new TurnsSystem();

                bool keepGoing = true;

                while(keepGoing)
                {
                    if(isPlayerOneturn)
                    {
                        turnsSystem.PlayerTurn(flag,playerOne,maze);
                        
                        isPlayerOneturn = false;
                    }

                    else
                    {
                        turnsSystem.PlayerTurn(flag,playerTwo,maze);
                        
                        isPlayerOneturn = true;
                        
                        flag++;
                    }

                    if(flag>1)
                        flag=0;
                }
            }


            void CreateGame(Maze maze,List<Tokens> playerOneTokens,List<Tokens> playerTwoTokens, int high, int width)
            {
                maze.MazeGenerator(1,1);//Siempre empezar en el 1-1

                while(!maze.IsAValidCell(high/2, width/2))
                {
                    maze.MazeGenerator(1,1);
                }
                
                maze.AddTrapsAndObstacles(30);

                maze.AddTokens(playerOneTokens,playerTwoTokens,high,width);
            }

            public void ShowGame(Maze maze, string message)
            {
                UsefulMethods useful = new UsefulMethods();

                var layout = new Layout("Root").SplitColumns
                (
                    new Layout("Left").Ratio(3),
                    new Layout("Right").SplitRows(new Layout("Top"),new Layout("Middle"), new Layout("Bottom")).Ratio(1)
                );

                layout["Left"].Update
                (
                    new Panel
                    (
                        Align.Center
                        (
                            new Markup(useful.FormatMatrix(maze.ConcatMazeWithStringMaze())),
                            VerticalAlignment.Middle
                        )
                    )
                    .Header("[green bold]Board[/]")
                    .Expand()
                    .RoundedBorder()
                    .BorderColor(Color.Blue)//BlueViolet //CadetBlue y _1 //Chartreuse1
                );

                layout["Top"].Update
                (
                    new Panel
                    (
                        Align.Center
                        (
                            new Markup("[blue bold]Menu[/]"),
                            VerticalAlignment.Middle
                        )
                    )
                    .Header("[bold yellow]Options[/]")
                    .RoundedBorder()
                    .BorderColor(Color.Yellow)
                    .Expand()
                );

                layout["Bottom"].Update
                (
                    new Panel
                    (
                        Align.Left
                        (
                            new Markup("[green]Points:[/]0\n[red]Health:[/]7\n[cyan]Skill:[/] NormalToken"),
                            VerticalAlignment.Top
                        )
                    )
                    .Header("[bold cyan]Player's Stade[/]")
                    .RoundedBorder()
                    .BorderColor(Color.Cyan1)
                    .Expand()
                );

                layout["Middle"].Update
                (
                    new Panel
                    (
                        Align.Left
                        (
                            new Markup("[white bold]" + message + "[/]"),
                            VerticalAlignment.Top
                        )
                    )
                    .Header("[bold magenta]Message[/]")
                    .RoundedBorder()
                    .BorderColor(Color.Magenta1)
                    .Expand()
                );

                AnsiConsole.Write(layout);
            }

            public void MoveToken(Tokens token, Maze maze, string message)
            {  
                int count = 3;

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

                while(count>0)
                {    
                    Console.Clear();//Refrescar la pantalla

                    ShowGame(maze,message);

                    System.Console.WriteLine("Muevete por el maze con las letras w,s,a,d ");
                    System.Console.WriteLine("Para salir del juego presiona e ");
                    System.Console.WriteLine("Para utilizar tu habilidad presiona k");
                    // string a = token.Health.ToString();
                    // System.Console.WriteLine(a);

                    char letter = Console.ReadKey().KeyChar;

                    if(letter == 'e')
                    {
                        break;
                    }
                        

                    if(letter=='k')
                    {
                        token.TokenSkill(maze);
                        continue;
                    }
                        
                    if(mazeDirec.ContainsKey(letter))
                    {
                        (int CordX,int CordY) = mazeDirec[letter];
                        
                        int newCordX = token.myX + CordX;

                        int newCordY = token.myY + CordY;

                        if(token.StuckTurns>0)
                        {
                            ShowGame(maze,"You are stuck");
                            
                            token.StuckTurns--;
                            
                            Thread.Sleep(1000);
                            
                            continue;
                        }

                        if(usefulMethods.isAValidMove(newCordX,newCordY,maze.maze))
                        {
                            maze.maze[token.myX,token.myY] = new FreeCell();//Haz libre la celda en que estba el token

                            token.myX = newCordX;

                            token.myY = newCordY;

                            
                            if(maze.maze[newCordX,newCordY] is TrapCell trap)
                            {
                                ShowGame(maze,"Trap Activated");

                                Thread.Sleep(1500);

                                trap.ActivateTrapSkill(token,maze);
                            }
                            
                            if(maze.maze[newCordX,newCordY] is not TeleportTrap)
                            {
                                maze.maze[newCordX,newCordY] = token;//Poner al token en su nueva posicion
                            }

                            if(maze.maze[newCordX,newCordY] is TeleportTrap)
                            {
                                maze.maze[newCordX,newCordY] = new FreeCell();
                            }

                            token.cooldowmSkill++;

                            if(token.Health<=0)
                            {
                                ShowGame(maze,"Your token is dead, you lose :( ");
                                
                                break;
                            }

                            count--;
                        }

                        else
                        {
                            ShowGame(maze," it is not a valid move");
                            
                            Thread.Sleep(1300);//Esto hace una pausa momentanea para q se lea el mensaje
                        }
                    }
                }
            }

            public bool EndGame(bool keepGoing)
            {
                return keepGoing = false;
            }
        }
    }