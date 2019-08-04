using System;
using System.Collections.Generic;
using System.Text;

namespace TreasureAdventure.Models
{
    public class Navigaion : Room
    {
        public bool GoNorth => NorthRoom != null;

        public bool GoEast => EastRoom != null;

        public bool GoWest => WestRoom != null;

        public bool GoSouth => SouthRoom != null;
    }
}
