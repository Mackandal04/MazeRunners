    using System;
    using Spectre.Console;


    namespace MazeRunners
    {
        class Game
        {
            public void StartGame(Tokens token)//Debe recibir un array de token q representa los tokens de cada jugador
            {
                Maze maze = new Maze(31,33); //Tope39X45

                CreateGame(maze,token);//Crea el juego por detras

                ShowGame(maze); //Muestra el tablero y el estado del juego

                LetsPlay(token,maze); //Permite jugar
            }


            void CreateGame(Maze maze, Tokens token)
            {
                maze.MazeGenerator(1,1);//Siempre empezar en el 1-1

                while(!maze.IsAValidMaze())
                {
                    maze.MazeGenerator(1,1);
                }
                
                maze.AddTrapsAndObstacles(30);

                maze.AddTokens(token);
            }

            void ShowGame(Maze maze)
            {
                UsefulMethods useful = new UsefulMethods();

                var layout = new Layout("Root").SplitColumns
                (
                    new Layout("Left").Ratio(3),
                    new Layout("Right").SplitRows(new Layout("Top"), new Layout("Bottom")).Ratio(1)
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
                    .BorderColor(Color.Blue)
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

                AnsiConsole.Write(layout);
            }

            void LetsPlay(Tokens token, Maze maze)
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
                    Console.Clear();//Refrescar la pantalla

                    ShowGame(maze);

                    System.Console.WriteLine("Muevete por el maze con las letras w,s,a,d ");
                    System.Console.WriteLine("Para salir del juego presiona e ");
                    System.Console.WriteLine("Para utilizar tu habilidad presiona k");

                    char letter = Console.ReadKey().KeyChar;

                    if(letter == 'e')
                        break;

                    if(letter=='k')
                    {
                        token.TokenSkill(maze);
                    }
                        
                    else if(mazeDirec.ContainsKey(letter))
                    {
                        (int CordX,int CordY) = mazeDirec[letter];
                        
                        int newCordX = token.myX + CordX;

                        int newCordY = token.myY + CordY;

                        if(usefulMethods.isAValidMove(newCordX,newCordY,maze.maze))
                        {
                            maze.maze[token.myX,token.myY] = new FreeCell();//Haz libre la celda en que estba el token

                            token.myX = newCordX;

                            token.myY = newCordY;

                            maze.maze[newCordX,newCordY] = token;//Poner al token en su nueva posicion

                            token.cooldowmSkill++;

                            if(token.myX==n-2 && token.myY == m-1)
                            {
                                Console.Clear();
                                ShowGame(maze);
                                System.Console.WriteLine("You have completed the maze");
                                break;
                            }
                        }

                        else
                        {
                            System.Console.WriteLine(" it is not a valid move");
                            Thread.Sleep(1500);//Esto hace una pausa momentanea para q se lea el mensaje
                        }
                    }
                }
            }
    }
}