    using System;
    using Spectre.Console;


    namespace MazeRunners
    {
        class Game
        {
            public void StartGame(NormalToken normalToken)//Debe recibir un array de token q representa los tokens de cada jugador
            {
                Maze maze = new Maze(7, 7); //Tope35X31

                CreateGame(maze,normalToken);//Crea el juego por detras

                ShowGame(maze); //Muestra el tablero y el estado del juego

                LetsPlay(normalToken,maze); //Permite jugar
            }


            void CreateGame(Maze maze, NormalToken normalToken)
            {
                maze.MazeGenerator(1,1);//Siempre empezar en el 1-1

                while(!maze.IsAValidMaze())
                {
                    maze.MazeGenerator(1,1);
                }
                
                maze.AddTrapsAndObstacles(10);

                maze.AddTokens(normalToken);
            }

            void ShowGame(Maze maze)
            {
                UsefulMethods useful = new UsefulMethods();

                var layout = new Layout("Root").SplitColumns
                (
                    new Layout("Left"),
                    new Layout("Right").SplitRows(new Layout("Top"), new Layout("Bottom"))
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
                    ).Expand()
                );

                layout["Top"].Update
                (
                    new Panel
                    (
                        Align.Center
                        (
                            new Markup("[blue]Menu[/]"),
                            VerticalAlignment.Middle
                        )
                    ).Expand()
                );

                layout["Bottom"].Update
                (
                    new Panel
                    (
                        Align.Center
                        (
                            new Markup("[blue]PiecesStade[/]"),
                            VerticalAlignment.Middle
                        )
                    ).Expand()
                );

                AnsiConsole.Write(layout);
            }

            void LetsPlay(NormalToken normalToken, Maze maze)
            {  
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

                while(true)
                {    
                    System.Console.WriteLine("Muevete por el maze con las letras w,s,a,d ");

                    char letter = Console.ReadKey().KeyChar;

                    if(letter == 'e')
                        break;
                        
                    else if(mazeDirec.ContainsKey(letter))
                    {
                        (int CordX,int CordY) = mazeDirec[letter];
                        
                        int newCordX = normalToken.myX + CordX;

                        int newCordY = normalToken.myY + CordY;

    //                        normalToken.TokenMove(newCordX,newCordY, maze.maze);
                        if(usefulMethods.isAValidMove(newCordX,newCordY,maze.maze))
                        {
                            maze.maze[normalToken.myX,normalToken.myY] = new FreeCell();//Haz libre la celda en que estba el token

                            normalToken.myX = newCordX;

                            normalToken.myY = newCordY;

                            maze.maze[newCordX,newCordY] = normalToken;//Poner al token en su nueva posicion

                            ShowGame(maze);

                            if(normalToken.myX==n-2 && normalToken.myY == m-1)
                            {
                                System.Console.WriteLine("You have completed the maze");
                                break;
                            }
                        }

                        else
                            System.Console.WriteLine("No valid move");
                    }
                }
            }
    }
}