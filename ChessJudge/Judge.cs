using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessJudge
{
    class Judge
    {
        public string name;

        //First int[0] -> A -> [1] -> B : [7] -> [H]
        //Second int[0] -> 1 -> [1] -> 2 : [7] -> [8].
        //[0,0] = A1 : [7,7] = H8.
        public TileState[,] boardState{ get; set; }
        public string AIA;
        public string AIB;

        public Judge(string nom)
        {
            name = nom;
        }

        public bool Connect(string AIOne, string AITwo)
        {
            //Verify both ai respond to handshake
            AIA = AIOne;
            AIB = AITwo;
            return true;

            //else below.
            //return false;
        }

        public enum TileState
        {
            Empty = 0,
            WhitePawn = 1,
            WhiteKnight = 2,
            WhiteBishop = 3,
            WhiteCastle = 4,
            WhiteQueen = 5,
            WhiteKing = 6,
            BlackPawn = 11,
            BlackKnight = 12,
            BlackBishop = 13,
            BlackCastle = 14,
            BlackQueen = 15,
            BlackKing = 16
        };

        /// <summary>
        /// Starts the game. 
        /// X = random first/assignment.
        /// A = AIA goes first.
        /// B = AIB goes first.
        /// Anything else = random.
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public bool StartGame(char x = 'X')
        {
            boardState = new TileState[8, 8];

            if(char.ToUpper(x).Equals('A'))
            {
                return true;
            }
            else if(char.ToUpper(x).Equals('B'))
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public TileState[,] SetupBoard(TileState[,] board)
        {
            TileState[,] currentBoard = board;
            ClearBoard(currentBoard);

            //Setup white back peices
            currentBoard[0, 0] = TileState.WhiteCastle;
            currentBoard[1, 0] = TileState.WhiteKing;
            currentBoard[2, 0] = TileState.WhiteBishop;
            currentBoard[3, 0] = TileState.WhiteQueen;
            currentBoard[4, 0] = TileState.WhiteKing;
            currentBoard[5, 0] = TileState.WhiteBishop;
            currentBoard[6, 0] = TileState.WhiteKnight;
            currentBoard[7, 0] = TileState.WhiteCastle;

            //[0,2] = [A,2] - Setup White Pawns
            currentBoard[0, 1] = TileState.WhitePawn;
            currentBoard[1, 1] = TileState.WhitePawn;
            currentBoard[2, 1] = TileState.WhitePawn;
            currentBoard[3, 1] = TileState.WhitePawn;
            currentBoard[4, 1] = TileState.WhitePawn;
            currentBoard[5, 1] = TileState.WhitePawn;
            currentBoard[6, 1] = TileState.WhitePawn;
            currentBoard[7, 1] = TileState.WhitePawn;


            //Setup back Black peices
            currentBoard[0, 7] = TileState.BlackCastle;
            currentBoard[1, 7] = TileState.BlackKing;
            currentBoard[2, 7] = TileState.BlackBishop;
            currentBoard[3, 7] = TileState.BlackQueen;
            currentBoard[4, 7] = TileState.BlackKing;
            currentBoard[5, 7] = TileState.BlackBishop;
            currentBoard[6, 7] = TileState.BlackKnight;
            currentBoard[7, 7] = TileState.BlackCastle;

            //[0,2] = [A,2] - Setup Black Pawns
            currentBoard[0, 6] = TileState.BlackPawn;
            currentBoard[1, 6] = TileState.BlackPawn;
            currentBoard[2, 6] = TileState.BlackPawn;
            currentBoard[3, 6] = TileState.BlackPawn;
            currentBoard[4, 6] = TileState.BlackPawn;
            currentBoard[5, 6] = TileState.BlackPawn;
            currentBoard[6, 6] = TileState.BlackPawn;
            currentBoard[7, 6] = TileState.BlackPawn;

            return currentBoard;
        }

        //Clear board of all pieces.
        public TileState[,] ClearBoard(TileState[,] board)
        {
            TileState[,] currentBoard = board;
            
            for(int i = 0; i < 8; i++)
            {
                for (int y = 0; y < 8; y++)
                {
                    currentBoard[i, y] = TileState.Empty;
                }
            }

            return currentBoard;
        }
    }
}
