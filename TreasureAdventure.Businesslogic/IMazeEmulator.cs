using System;
using System.Collections.Generic;
using System.Text;
using TreasureAdventure.Models;

namespace TreasureAdventure.Businesslogic
{
    public interface IMazeEmulator
    {
      
        void InitEmulator();

        void MainEmulate();

        Maze.MazeProperties NavigationStart(Player player);

        void GetRoomInformation(int roomId);

        Maze.MazeProperties EmulateMaze (Player player, int roomId);

        void DrawRoom(int roomSize, int visitRoomId, char[,] maze);

        void NewGame();

        void GetMenu();

        void GetPlayerInformation(Player player);

        void FinnishWithParty(Player plater);

       

    }
}
