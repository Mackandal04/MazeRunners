    using System;
using System.Text;
using Spectre.Console;


    namespace MazeRunners
    {
        class Game
        {
            public void StartGame(Player playerOne, Player playerTwo)//Debe recibir un array de token q representa los tokens de cada jugador
            {
                int high = 35;

                int width = 35;
                
                GameDisplay gameDisplay=new GameDisplay();

                List<Tokens> tokens = new List<Tokens> //Lista con todos los tokens del juego, para seleccionar
                {
                    new NormalToken("Jarvis","[cyan]⚡[/]",1,1),
                    new TeleportToken("Nightcrawler","[cyan]☬░[/]",high-2,width-2),
                    new TrapDeleteToken("Batman","[cyan]⇶░[/]",1,width-2),
                    new ObstacleToken("Quasimodo","[cyan]⇯░[/]",high-2,1),
                    new FlashToken("Francesco Bernoulli","[Red]⧖░[/]",1,1),
                    new WallDestroyerToken("Optimus Prime","[Red]KK[/]",high-2,width-2)
                };

                Maze maze = new Maze(high,high);

                CreateGame(maze,high,width,tokens,playerOne,playerTwo);//Crea el juego por detras

                bool isPlayerOneturn = true;

                TurnsSystem turnsSystem = new TurnsSystem();

                while(playerOne.playerTokens.Count>0 && playerTwo.playerTokens.Count>0)//Mientras ambos tengan al menos un token para jugar
                {
                    if(isPlayerOneturn)
                    {
                        StringBuilder stringBuilderOne = new StringBuilder();
                        
                        gameDisplay.ShowGame(maze,"Es el turno de " + playerOne.name);

                        Console.ReadKey();

                        gameDisplay.ShowGame(maze,"Que token desea utilizar ?");

                        Console.ReadKey();

                        for (int i = 0; i < playerOne.playerTokens.Count; i++)
                        {
                            stringBuilderOne.Append("\n" + "-" + (i+1)+ ". " + playerOne.playerTokens[i].name);
                        }

                        stringBuilderOne.AppendLine();

                        gameDisplay.ShowGame(maze,stringBuilderOne.ToString());

                        int tokenIndex;

                        if(int.TryParse(Console.ReadLine(),out tokenIndex) && tokenIndex >=0 && tokenIndex <= playerOne.playerTokens.Count)
                        {
                            turnsSystem.PlayerTurn(tokenIndex-1,playerOne,maze);

                            isPlayerOneturn = false;
                        }

                        else
                        {
                            gameDisplay.ShowGame(maze,"[bold yellow]Entrada no valida, por favor intente otra vez[/]");

                            Console.ReadKey();
                        }
                    }

                    else
                    {
                        StringBuilder stringBuilderTwo = new StringBuilder();

                        gameDisplay.ShowGame(maze,"Es el turno de " + playerTwo.name);

                        gameDisplay.ShowGame(maze,"Que token desea utilizar ?");

                        Console.ReadKey();

                        for (int i = 0; i < playerTwo.playerTokens.Count; i++)
                        {
                            stringBuilderTwo.Append("\n" + "-" + (i+1)+ ". " + playerTwo.playerTokens[i].name);
                        }

                        stringBuilderTwo.AppendLine();

                        gameDisplay.ShowGame(maze,stringBuilderTwo.ToString());

                        int tokenIndex;

                        if(int.TryParse(Console.ReadLine(),out tokenIndex) && tokenIndex >=0 && tokenIndex <= playerTwo.playerTokens.Count)
                        {
                            turnsSystem.PlayerTurn(tokenIndex-1,playerTwo,maze);

                            isPlayerOneturn = true;
                        }

                        else
                        {
                            gameDisplay.ShowGame(maze,"[bold yellow]Entrada no valida,por favor intente otra vez[/]");

                            Console.ReadKey();
                        }
                    }
                }
            }

            void CreateGame(Maze maze, int high, int width, List<Tokens>tokens,Player playerOne, Player playerTwo)
            {
                GameDisplay gameDisplay = new GameDisplay();

                maze.MazeGenerator(1,1);//Siempre empezar en el 1-1

                while(!maze.IsAValidCell(high/2, width/2))
                {
                    maze.MazeGenerator(1,1);
                }

                gameDisplay.ShowGame(maze,"[bold yellow]Bienvenidos a Maze Runners ![/]"); //Muestra el tablero y el estado del juego
                
                Console.ReadKey();

                gameDisplay.ShowGame(maze,"[bold yellow]El primer jugador en llevar todas sus fichas a la casilla de salida sera el ganador[/]" + "\n" + "[bold yellow]La casilla de salida se encuentra representada por un tunel gris en el medio del laberinto que veran a continuacion[/]" + "\n" + "[bold yellow]Les deseo exitos a todos ![/]");
                
                Console.ReadKey();

                gameDisplay.ShowGame(maze,"[bold yellow]Buena suerte :) [/]");
                
                Console.ReadKey();

                ChooseToken(maze,tokens,playerOne,playerTwo);
                
                maze.AddTrapsAndObstacles(80);

                maze.AddTokens(playerOne.playerTokens,playerTwo.playerTokens,high,width);
            }
            
            //Metedo para q cada player seleccione sus tokens
            public void ChooseToken(Maze maze, List<Tokens>tokens, Player playerOne, Player playerTwo)
            {
                int high = maze.maze.GetLength(0);

                int width = maze.maze.GetLength(1);
                
                UsefulMethods usefulMethods = new UsefulMethods();

                GameDisplay gameDisplay = new GameDisplay();

                List<(int,int)> positions = new List<(int, int)>
                {
                    (1,1),
                    (high-2,width-2),
                    (1,width-2),
                    (high-2,1)
                };

                while(tokens.Count>2)//mientras hayan tokens para seleccionar
                {
                    StringBuilder stringBuilderOne = new StringBuilder();

                    StringBuilder stringBuilderTwo = new StringBuilder();

                    gameDisplay.ShowGame(maze,"Es el turno de " + "\n" + playerOne.name);
                    
                    Console.ReadKey();
                    

                    gameDisplay.ShowGame(maze,"Con que ficha desea jugar ?");
                    
                    Console.ReadKey();

                    for (int i = 0; i < tokens.Count; i++)
                    {
                        stringBuilderOne.Append("\n" +"\n"+ "-" + (i+1)+ ". " + tokens[i].name );
                    }
                    stringBuilderOne.AppendLine();

                    gameDisplay.ShowGame(maze,stringBuilderOne.ToString());

                    int index;

                    while(true)
                    {
                        if(int.TryParse(Console.ReadLine(),out index) && index>0 && index<= tokens.Count)
                        {
                            playerOne.AddToken(tokens[index-1] );
                            
                            usefulMethods.TokensPositions(maze,positions,tokens[index-1]);
                            
                            tokens.RemoveAt(index-1);
                            
                            break;
                        } 

                        else
                        {
                            gameDisplay.ShowGame(maze,"[bold yellow]Entrada no valida,por favor intente otra vez[/]");

                            Console.ReadKey();
                            

                            gameDisplay.ShowGame(maze,stringBuilderOne.ToString());
                        }
                    }

                    if(tokens.Count>0)
                    {
                        gameDisplay.ShowGame(maze,"Es el turno de "  + "\n" + playerTwo.name);
                        
                        Console.ReadKey();
                        

                        gameDisplay.ShowGame(maze,"Con que ficha desea jugar ?");
                        
                        Console.ReadKey();
                        

                        for (int i = 0; i < tokens.Count; i++)
                        {
                            stringBuilderTwo.Append("\n" +"\n"+ "-" + (i+1)+ ". " + tokens[i].name );
                        }
                        stringBuilderTwo.AppendLine();

                        gameDisplay.ShowGame(maze,stringBuilderTwo.ToString());

                        while(true)
                        {
                            if(int.TryParse(Console.ReadLine(),out index) && index>0 && index<= tokens.Count)
                            {                            
                                playerTwo.AddToken(tokens[index-1] );
                                
                                usefulMethods.TokensPositions(maze,positions,tokens[index-1]);
                                
                                tokens.RemoveAt(index-1);
                                
                                break;
                            } 

                            else
                            {
                                gameDisplay.ShowGame(maze,"[bold yellow]Entrada no valida,por favor intente otra vez[/]");

                                Console.ReadKey();

                                gameDisplay.ShowGame(maze,stringBuilderTwo.ToString());
                            }
                        }
                    }
                }
            }
        }
    }