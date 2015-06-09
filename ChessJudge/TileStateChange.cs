using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessJudge
{
    public class TileStateChange
    {
        public int Letter;
        public int Number;
        public Judge.TileState oldState;
        public Judge.TileState newState;

        public TileStateChange(int l, int n, Judge.TileState fs, Judge.TileState ss)
        {
            Letter = l;
            Number = n;
            oldState = fs;            
            newState = ss;
        }

    }
}
