    using System;
    using Spectre.Console;


    namespace MazeRunners
    {
        class Game
        {
            public void StartGame(Player playerOne, Player playerTwo)//Debe recibir un array de token q representa los tokens de cada jugador
            {
                int high = 31;

                int width = 31;
                
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

                CreateGame(maze,playerOne.playerTokens,playerTwo.playerTokens,high,width,tokens,playerOne,playerTwo);//Crea el juego por detras

                //ShowGame(maze,"Welcome to the game !"); //Muestra el tablero y el estado del juego
                //Thread.Sleep(2100);

                //ShowGame(maze,"The first player to take all his tokens to exits will be the winner");
                //Thread.Sleep(3500);

                //ShowGame(maze,"Good luck :) ");
                //Thread.Sleep(2300);

                int flag = 0;

                TurnsSystem turnsSystem = new TurnsSystem();

                while(true)
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

                // gameDisplay.ShowGame(maze,"Exit game...");
                // Thread.Sleep(1300);
            }


            void CreateGame(Maze maze,List<Tokens> playerOneTokens,List<Tokens> playerTwoTokens, int high, int width, List<Tokens>tokens,Player playerOne, Player playerTwo)
            {
                maze.MazeGenerator(1,1);//Siempre empezar en el 1-1

                while(!maze.IsAValidCell(high/2, width/2))
                {
                    maze.MazeGenerator(1,1);
                }

                ChooseToken(maze,tokens,playerOne,playerTwo);
                
                maze.AddTrapsAndObstacles(30);

                maze.AddTokens(playerOneTokens,playerTwoTokens,high,width);
            }
            
            //Metedo para q cada player seleccione sus tokens
            public void ChooseToken(Maze maze, List<Tokens>tokens, Player playerOne, Player playerTwo)
            {
                GameDisplay gameDisplay = new GameDisplay();

                while(tokens.Count>0)//mientras hayan tokens para seleccionar
                {
                    //"Es turno de " + playerOne.playerTokens.Count + ". " + playerOne.playerTokens[playerOne.playerTokens.Count-1].name
                    gameDisplay.ShowGame(maze,"Es el turno del PlayerOne");//"Es el turno de " + player.playerTokens[flag].name
                    
                    Thread.Sleep(1300);

                    gameDisplay.ShowGame(maze,"Escoge un token");
                    
                    Thread.Sleep(1300);

                    for (int i = 0; i < tokens.Count; i++)
                    {
                        gameDisplay.ShowGame(maze,"-" + (i+1)+ ". " + tokens[i].name );

                        Thread.Sleep(1300);
                    }

                    int index;

                    while(true)
                    {
                        Console.Clear();
                        
                        gameDisplay.ShowGame(maze,"Introduce el numero del token que deseas");

                        if(int.TryParse(Console.ReadLine(),out index) && index>0 && index<= tokens.Count)
                        {
                            playerOne.AddToken(tokens[index-1] );
                            tokens.RemoveAt(index-1);
                            break;
                        } 

                        else
                        {
                            gameDisplay.ShowGame(maze,"Entrada no valida, intenta otra vez");

                            Thread.Sleep(1300);
                        }
                    }

                    if(tokens.Count>0)
                    {
                        gameDisplay.ShowGame(maze,"Es turno del PlayerTwo ");
                        
                        Thread.Sleep(1300);

                        gameDisplay.ShowGame(maze,"Escoge un token");
                        
                        Thread.Sleep(1300);

                        for (int i = 0; i < tokens.Count; i++)
                        {
                            gameDisplay.ShowGame(maze,"-" + (i+1)+ ". " + tokens[i].name );

                            Thread.Sleep(1300);
                        }

                        while(true)
                        {
                            Console.Clear();
                            
                            gameDisplay.ShowGame(maze,"Introduce el numero del token que deseas");

                            if(int.TryParse(Console.ReadLine(),out index) && index>0 && index<= tokens.Count)
                            {
                                playerTwo.AddToken(tokens[index-1] );
                                tokens.RemoveAt(index-1);
                                break;
                            } 

                            else
                            {
                                gameDisplay.ShowGame(maze,"Entrada no valida, intenta otra vez");

                                Thread.Sleep(1300);
                            }
                        }
                    }
                }
            }
        }
    }