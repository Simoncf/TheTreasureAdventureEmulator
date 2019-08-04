using AtlasCopco.Integration.Maze;
using System;
using TreasureAdventure.Models;

namespace TreasureAdventure.Businesslogic
{
    public class MazeEmulator : IMazeEmulator
    {

        private readonly IMazeIntegration _mazeIntegration;
        private readonly MazeLayout _mazeLayout;

        public MazeEmulator(IMazeIntegration mazeIntegration)
        {
            _mazeIntegration = mazeIntegration;
            _mazeLayout = new MazeLayout();
        }

        public MazeEmulator()
        {
          
        }
        

        public void DrawRoom(int roomSize, int visitRoomId, char[,] maze)
        {
            MazeDrawer.DrawBox(roomSize, visitRoomId, maze);
        }

        public Maze.MazeProperties EmulateMaze(Player player, int roomId)
        {
            char[,] MazeVisited = new char[_mazeLayout.Size, _mazeLayout.Size];
            while (true)
            {
                Console.Clear();
                


                // Check if treasure found
                if (_mazeIntegration.HasTreasure(roomId))
                {
                    FinnishWithParty(player);
                    return Maze.MazeProperties.FoundTreasure;
                }

                // Check if room has a trap
                if (_mazeIntegration.CausesInjury(roomId))
                {
                    if (player.HealthPoint <= 1)
                    {
                        Console.Clear();
                        Console.WriteLine("Good luck next time!" + player.Name);
                        Console.WriteLine();


                        return Maze.MazeProperties.PlayerHasDied;
                    }

                    Console.WriteLine("You have been injured..");
                    Console.WriteLine("You suffered a loss of 1 HP, you now have " + player.HealthPoint + " HP left");
                    player.HealthPoint--;
                }

                // Display game information
                GetRoomInformation(roomId);
                GetPlayerInformation(player);

                // Draw a static room
                var roomProperties = GetRoomProperties(roomId);
                DrawRoom(_mazeLayout.Size, roomId, MazeVisited);

                // Allow hunter to move
                //TODO Refactor this?
                Console.WriteLine("Choose your destiny... Use keys (N, S, W, E) Exit: (Esc)");
                var menuChoice = Console.ReadKey(true).Key;
                switch (menuChoice)
                {
                    case ConsoleKey.N:
                        if (roomProperties.GoNorth)
                        {
                            player.StepsCount++;
                            Console.WriteLine("Going North!");
                            roomId = (int)roomProperties.NorthRoom;
                        }
                        else
                        {
                            Console.WriteLine("You hit the wall");
                        }
                        continue;
                    case ConsoleKey.S:
                        if (roomProperties.GoSouth)
                        {
                            player.StepsCount++;
                            Console.WriteLine("Going South!");
                            roomId = (int)roomProperties.SouthRoom;
                        }
                        else
                        {
                            Console.WriteLine("You hit the wall");
                        }
                        continue;
                    case ConsoleKey.W:
                        if (roomProperties.GoWest)
                        {
                            player.StepsCount++;
                            Console.WriteLine("Going West!");
                            roomId = (int)roomProperties.WestRoom;
                        }
                        else
                        {
                            Console.WriteLine("You hit the wall");
                        }
                        continue;
                    case ConsoleKey.E:
                        if (roomProperties.GoEast)
                        {
                            Console.WriteLine("Going East!");
                            player.StepsCount++;
                            roomId = (int)roomProperties.EastRoom;
                        }
                        else
                        {
                            Console.WriteLine("You hit the wall");
                        }
                        continue;
                    case ConsoleKey.Escape:
                        Environment.Exit(0);
                        break;
                    default:
                       //  Console.WriteLine("Invalid direction. The treasure is important, come on!");
                        continue;
                }
            }
        }

        public void FinnishWithParty(Player plater)
        {
            Console.WriteLine($"Congratulations {plater.Name}! you won with {plater.StepsCount} steps and  {plater.HealthPoint} HP ");
        }

        public void GetMenu()
        {
            Console.WriteLine("New Game (y/n)");
        }

        public void GetPlayerInformation(Player player)
        {
            if (player == null) 
            {
                throw new ArgumentNullException(nameof(player));
            }
            Console.WriteLine("Letter: v inicates if the place has been viseted");
            Console.WriteLine($"Player:{player.Name}     HP:{player.HealthPoint}      Steps:{player.StepsCount}");
            Console.WriteLine();
            Console.WriteLine();
        }

        public void GetRoomInformation(int roomId)
        {
            try
            {
                var roomDescription = _mazeIntegration.GetDescription(roomId);
                Console.WriteLine("Room is {0}", roomDescription);
                Console.WriteLine();
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine("Maze metadata is wrong, room description wasn't found!");
                Console.WriteLine(e.StackTrace.ToString());
                //TODO log e using NLog or whatever
                Console.ReadLine();
                Environment.Exit(0);
            }
        }

        public void InitEmulator()
        {

            Console.Clear();
            Console.WriteLine("Wellcome to The tressure hunt");
            GetMenu();
        }
        public Player ResetPlayer()
        {

            var player = new Player
            {
                HealthPoint = 2, //TODO Read from config
                StepsCount = 0,
                Name = ""
            };
            return player;
        }
        public void MainEmulate()
        {
            InitEmulator();

            
            while (!Console.KeyAvailable)
            {
                // Load dll dynamically
                // Temporarily use a dummy object to draw the skeleton
                // IMazeIntegration mazeGenerator;

                var menuChoice = Console.ReadKey(true).Key;

                switch (menuChoice)
                {
                    case ConsoleKey.Y:
                        Console.Clear();
                        NewGame();
                        Player player = ResetPlayer();
                        Console.WriteLine("Please enter your name ");
                        string name = Console.ReadLine();
                        player.Name = name;
                        NavigationStart(player);
                        break;
                    case ConsoleKey.N:
                        break;
                    default:
                        Console.WriteLine("Not a valid choice!");
                        continue;
                }

                GetMenu();
            }
        }

        public Maze.MazeProperties NavigationStart(Player player)
        {
            var entranceRoomId = -1;
            Console.WriteLine("Starting maze navigation.");
            Console.WriteLine("Parsing maze metadata..");
            Console.WriteLine("Getting entrance room...");
            try
            {
                entranceRoomId = _mazeIntegration.GetEntranceRoom();
            }
            catch (Exception e)
            {
                Console.WriteLine("Error, No room entrence was found!");
                Console.WriteLine(e.StackTrace.ToString());
                //TODO log e using NLog or whatever
                Console.ReadLine();
                Environment.Exit(0);
            }


            Console.WriteLine("Initialize room entrence....");

            return EmulateMaze(player, entranceRoomId);
        }

        private Navigaion GetRoomProperties(int roomId)
        {
            var roomProperties = new Navigaion
            {
                NorthRoom = _mazeIntegration.GetRoom(roomId, 'N'),
                SouthRoom = _mazeIntegration.GetRoom(roomId, 'S'),
                WestRoom = _mazeIntegration.GetRoom(roomId, 'W'),
                EastRoom = _mazeIntegration.GetRoom(roomId, 'E')
            };
            return roomProperties;
        }

        private static int GetRandomMazeSize()
        {
            Console.WriteLine("Generating Random maze size..");
            var mazeSizeGenerator = new Random();
            var size = mazeSizeGenerator.Next(2, 10);
            Console.WriteLine("Random maze size {0} is generated", size);

            return size;
        }

        public void NewGame()
        {

            _mazeLayout.Size = GetRandomMazeSize();
           

            // Build Maze
            try
            {
                Console.WriteLine("Generating maze of size {0}", _mazeLayout.Size);
                _mazeIntegration.BuildMaze(_mazeLayout.Size);
                Console.WriteLine("Maze generation succeeded!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace.ToString());

                Console.ReadLine();
                Environment.Exit(0);
            }
        }
    }
}
