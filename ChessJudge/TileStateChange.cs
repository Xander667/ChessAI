using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessJudge
{
    class TileStateChange
    {
        public int Letter;
        public int Number;
        public Judge.TileState oldState;
        public Judge.TileState newState;

        public TileStateChange(int l, int n, Judge.TileState fs, Judge.TileState ss)
        {
            int Letter = l;
            int Number = n;
            Judge.TileState oldState = fs;            
            Judge.TileState newState = ss;
        }


    }
}
