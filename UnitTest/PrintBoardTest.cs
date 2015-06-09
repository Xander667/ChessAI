using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class PrintBoardTest
    {
        //Boards
        static ChessJudge.Judge.TileState[,] board = new ChessJudge.Judge.TileState[8, 8];

        [TestMethod]
        public void PrintStartBoard()
        {
            //Setup empty boards
            ChessJudge.Judge.SetupBoard(board);
            ChessJudge.Judge.PrintBoard(board);        
        }
    }
}
