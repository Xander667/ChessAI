using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class ValidateMoveTests
    {
        //Variables needed

        //Test some moves are valid;
        public static ChessJudge.Judge.TileState moveFromState = ChessJudge.Judge.TileState.WhiteCastle;
        public static ChessJudge.Judge.TileState moveToState = ChessJudge.Judge.TileState.Empty;

        //Boards
        ChessJudge.Judge.TileState[,] beforeBoard = new ChessJudge.Judge.TileState[8, 8];
        ChessJudge.Judge.TileState[,] afterBoard = new ChessJudge.Judge.TileState[8, 8];

        [TestMethod]
        public void ValidateCastleMoves()
        {
            bool result = false;

            //Setup empty boards
            ChessJudge.Judge.SetupBoard(beforeBoard);
            ChessJudge.Judge.SetupBoard(afterBoard);

            //Add moves to before board. WC in A1, BP in A6
            beforeBoard[0, 0] = ChessJudge.Judge.TileState.WhiteCastle;
            beforeBoard[0, 5] = ChessJudge.Judge.TileState.BlackPawn;

            //Add moves to after board. E in A1, WC in A6.
            afterBoard[0, 0] = ChessJudge.Judge.TileState.Empty;
            afterBoard[0, 5] = ChessJudge.Judge.TileState.WhiteCastle;

            result = ChessJudge.Judge.ValidateMove(beforeBoard, afterBoard);
        }
    }
}
