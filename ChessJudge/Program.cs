using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessJudge
{
    class Program
    {
        static void Main(string[] args)
        {
            //Create judge
            Judge john = new Judge("John");

            //Connect to AI Bots
            john.Connect("1", "2");

            //Test some moves are valid;
            Judge.TileState moveFromState = Judge.TileState.WhiteCastle;
            Judge.TileState moveToState = Judge.TileState.Empty;

            //White Castle A,1 -> Empty Space A,6
            TileStateChange MoveFrom = new TileStateChange(0,0, moveFromState, Judge.TileState.Empty);
            TileStateChange MoveTo = new TileStateChange(0,6, moveToState, Judge.TileState.Empty);

            //Boards
            Judge.TileState[,] beforeBoard = new Judge.TileState[8,8];

            bool valid = john.ValidateMove()
            //Connect Judge to AIs
        }
    }
}