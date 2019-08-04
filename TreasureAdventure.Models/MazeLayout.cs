using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureAdventure.Models
{
    public  class MazeLayout
    {
        public int[,] Maze { get; set; }

        public int TreasureId { get; set; }

        public  int Trap { get; set; }

        public int EntrenceId { get; set; }

        public List<int> Traps { get; set; }

        public int Size { get; set; }
    }
}
