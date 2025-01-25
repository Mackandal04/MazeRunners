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
                
                bool isPlayerOneturn = true;

                GameDisplay gameDisplay=new GameDisplay();

                List<Tokens> tokens = new List<Tokens> //Lista con todos los tokens del juego, para seleccionar
                {
                    new NormalToken("Jarvis",1,1),
                    new TeleportToken("Ultron",high-2,width-2),
                    new TrapDeleteToken("Batman",1,width-2),
                    new ObstacleToken("Hulk",high-2,1),
                    new FlashToken("Flash",1,1),
                    new WallDestroyerToken("Truck",high-2,width-2)
                };

                Maze maze = new Maze(high,high);

                CreateGame(maze,high,width,tokens,playerOne,playerTwo);//Crea el juego por detras

                int flag = 0;

                TurnsSystem turnsSystem = new TurnsSystem();

                while(playerOne.playerTokens.Count>0 && playerTwo.playerTokens.Count>0)//&& playerTwo.playerTokens.Count>0
                {
                    if(isPlayerOneturn)
                    {
                        turnsSystem.PlayerTurn(flag,playerOne,maze);//flag
                        
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


            void CreateGame(Maze maze, int high, int width, List<Tokens>tokens,Player playerOne, Player playerTwo)
            {
                GameDisplay gameDisplay = new GameDisplay();

                maze.MazeGenerator(1,1);//Siempre empezar en el 1-1

                while(!maze.IsAValidCell(high/2, width/2))
                {
                    maze.MazeGenerator(1,1);
                }

                gameDisplay.ShowGame(maze,"Welcome to the game !"); //Muestra el tablero y el estado del juego
                
                Console.ReadKey();

                gameDisplay.ShowGame(maze,"The first player to get all his tokens to the middle of the board(the exit) will be the winner");
                
                Console.ReadKey();

                gameDisplay.ShowGame(maze,"Good luck :) ");
                
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
                    

                    gameDisplay.ShowGame(maze,"Escoge un token");
                    
                    Console.ReadKey();
                    

                    for (int i = 0; i < tokens.Count; i++)
                    {
                        stringBuilderOne.Append("\n" + "-" + (i+1)+ ". " + tokens[i].name );
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
                            gameDisplay.ShowGame(maze,"Entrada no valida, intenta otra vez");

                            Console.ReadKey();
                            

                            gameDisplay.ShowGame(maze,stringBuilderOne.ToString());
                        }
                    }

                    if(tokens.Count>0)
                    {
                        gameDisplay.ShowGame(maze,"Es turno de "  + "\n" + playerOne.name);
                        
                        Console.ReadKey();
                        

                        gameDisplay.ShowGame(maze,"Escoge un token");
                        
                        Console.ReadKey();
                        

                        for (int i = 0; i < tokens.Count; i++)
                        {
                            stringBuilderTwo.Append("\n" + "-" + (i+1)+ ". " + tokens[i].name );
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
                                gameDisplay.ShowGame(maze,"Entrada no valida, intenta otra vez");

                                Console.ReadKey();

                                gameDisplay.ShowGame(maze,stringBuilderTwo.ToString());
                            }
                        }
                    }
                }
            }
        }
    }