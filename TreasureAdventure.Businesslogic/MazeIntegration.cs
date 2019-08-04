using System;
using System.Collections.Generic;
using System.Linq;
using AtlasCopco.Integration.Maze;
using TreasureAdventure.Models;

namespace TreasureAdventure.Businesslogic
{
    public class MazeIntegration : IMazeIntegration
    {
        private readonly MazeLayout _mazeLayout;
        public MazeIntegration()
        {
            _mazeLayout = new MazeLayout();
        }
        

        public void BuildMaze(int size)
        {
            int RoomId = 0;
            _mazeLayout.Size = size;
            _mazeLayout.Maze = new int[size, size];
            _mazeLayout.TreasureId = 1;
            _mazeLayout.Trap = size-2;
           
            _mazeLayout.EntrenceId = size * size;
            _mazeLayout.Traps = new List<int>();
            for (int i = 0; i < _mazeLayout.Trap; i++)
            {
                int roomIdOfTrap = RanomRoomInerior(size * size);
                while (_mazeLayout.Traps.Any(y => y == roomIdOfTrap))
                {
                    roomIdOfTrap = RanomRoomInerior(size * size);
                }
                _mazeLayout.Traps.Add(roomIdOfTrap);

               

            }
            int roomIdOfTreasure = RanomRoomInerior(size * size);
            while (_mazeLayout.Traps.Any(i => i == roomIdOfTreasure))
            {
                 roomIdOfTreasure = RanomRoomInerior(size * size);
            }
            _mazeLayout.TreasureId = roomIdOfTreasure;

            for (int row = 0; row < size; row++)
            {
                char[] arr = new char[size];
                for (int col = 0; col < size; col++)
                {

                    RoomId++;
                    _mazeLayout.Maze[row, col] = RoomId;
                }
            }
           
        }

        private int RanomRoomInerior(int MaxNumnber)
        {
            var roomGenerator = new Random();
            var roomInerior = roomGenerator.Next(1, MaxNumnber);
            return roomInerior;
        }

        public bool CausesInjury(int roomId)
        {
            return _mazeLayout.Traps.Any(i => i == roomId);
        }

        public string GetDescription(int roomId)
        {
            if (GetEntranceRoom() == roomId)
                return "the entrence";
            else
                return "empty";
        }

        public int GetEntranceRoom()
        {
            return _mazeLayout.EntrenceId;
        }

        public int? GetRoom(int roomId, char direction)
        {
            for (int row = 0; row < _mazeLayout.Size; row++)
            {
                for (int col = 0; col < _mazeLayout.Size; col++)
                {
                   var roomid =  _mazeLayout.Maze[row, col];
                    if(roomid == roomId)
                    {
                        switch (direction)
                        {
                            case 'N':
                                if (row - 1 >= 0 && row - 1 <= _mazeLayout.Size-1)
                                    return _mazeLayout.Maze[row - 1, col];
                                break;
                            case 'S':
                                if (row + 1 >= 0 && row + 1 <= _mazeLayout.Size-1)
                                    return _mazeLayout.Maze[row + 1, col];
                                break;
                            case 'W':
                                if(col-1 >= 0 && col - 1 <= _mazeLayout.Size-1)
                                    return _mazeLayout.Maze[row, col-1];
                                break;
                            case 'E':
                                if (col + 1 >= 0 && col + 1 <= _mazeLayout.Size-1)
                                    return _mazeLayout.Maze[row, col + 1];
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return null;
        }

        public bool HasTreasure(int roomId)
        {
            return roomId == _mazeLayout.TreasureId;
        }
    }
}
