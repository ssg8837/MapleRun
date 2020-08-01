using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapleRun0509.Library
{
    public class EnumManager
    {
        public enum Direction
        {
            LEFT,
            RIGHT,
            UP,
            DOWN
        }
        public enum TileType
        {
            FLOOR=0,
            LtoR=1,
            RtoL=2,
            Wall=3           
        }
        public enum GameState
        {
            Menu,
            Start,
            End
        }

    }
}
