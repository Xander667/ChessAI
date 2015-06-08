using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessJudge
{
    class TileStateChange
    {
        public int oldLetter;
        public int oldNumber;
        public Judge.TileState oldState;

        public int newLetter;
        public int newNumber;
        public Judge.TileState newState;

        public TileStateChange(int fl, int fn, Judge.TileState fs, int sl, int sn, Judge.TileState ss)
        {
            int oldLetter = fl;
            int oldNumber = fn;
            Judge.TileState oldState = fs;

            int newLetter = sl;
            int newNumber = sn;
            Judge.TileState newState = ss;
        }


    }
}
