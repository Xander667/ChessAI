using System;
using System.Collections.Generic;
using System.IO;
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
        public TileState[,] proposedState;
        public string AIA;
        public string AIB;
        public string White;
        public string Black;
        public int Turn = 0;
        public bool GameOver;
        public TurnState currentTurn;
        string logFilePath;
        StringBuilder log;

        public Judge(string nom)
        {
            name = nom;
        }

        public bool Connect(string AIOne, string AITwo)
        {
            //Verify both ai respond to handshake
            AIA = AIOne;
            AIB = AITwo;

            logFilePath = Directory.GetCurrentDirectory();
            log.Append("Connecting AI1: " + AIOne);
            log.AppendLine("Connecting AI2: " + AITwo);
            File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", log.ToString());
            log.Clear();

            return true;

            //else below.
            //return false;
        }

        public enum TurnState
        {
            White = 0,
            Black = 1
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
            //New game variables.
            GameOver = false;
            currentTurn = TurnState.White;
            boardState = new TileState[8, 8];
            proposedState = new TileState[8, 8];

            log.AppendLine("Setting Up Board...");
            SetupBoard(boardState);
            SetWhitePlayer(x);

            log.AppendLine("StartingGame");

            File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", log.ToString());
            log.Clear();


            while(!GameOver)
            {
                Turn++;
                log.AppendLine("Starting Turn: " + Turn.ToString());

                if(currentTurn == TurnState.White)
                {
                    log.AppendLine("Player Turn: White");
                    log.AppendLine("Current Board State:");
                    PrintBoard(log, boardState);
                    proposedState = GetMove(boardState, White);

                    log.AppendLine("Proposed Move:");
                    PrintBoard(log, proposedState);
                    File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", log.ToString());
                    log.Clear();
                }
                else
                {
                    log.AppendLine("Player Turn: Black");
                    log.AppendLine("Current Board State:");
                    PrintBoard(log, boardState);
                    proposedState = GetMove(boardState, Black);

                    log.AppendLine("Proposed Move:");
                    PrintBoard(log, proposedState);
                    File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", log.ToString());
                    log.Clear();
                }

                log.AppendLine("Verifying Move Validity");
                GameOver = ValidateMove(boardState, proposedState);
                File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", log.ToString());
                log.Clear();

                //Swap turns
                if (currentTurn == TurnState.White)
                {
                    currentTurn = TurnState.Black;
                }
                else
                {
                    currentTurn = TurnState.White;
                }
            }

            return true;
        }

        public void PrintBoard(StringBuilder logFile, TileState[,] boardToPrint)
        {
            log.AppendLine("  A B C D E F G H");
            for(int i=0; i<8; i++)
            {
                //Append a row
                log.AppendLine("1  " + boardToPrint[0, i].ToString() + " " + boardToPrint[1, i].ToString() + " " + boardToPrint[2, i].ToString() + " " + boardToPrint[3, i].ToString()
                    + boardToPrint[4, i].ToString() + " " + boardToPrint[5, i].ToString() + " " + boardToPrint[6, i].ToString() + " " + boardToPrint[7, i].ToString());
            }
            File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", log.ToString());
            log.Clear();
        }

        public TileState[,] GetMove(TileState[,] board, String AI)
        {
            TileState[,] proposedTurn = new TileState[8,8];
            if(AI == AIA)
            {
                //Call AI1 API
            }
            else
            {
                //CALL AI2 API
            }

            return proposedTurn; 
        }

        public bool ValidateMove(TileState[,] boardBefore, TileState[,] boardAfter)
        {
            
            return false;
        }

        public void SetWhitePlayer(char x)
        {
            if (char.ToUpper(x).Equals('A'))
            {
                White = AIA;
                Black = AIB;
            }
            else if (char.ToUpper(x).Equals('B'))
            {
                White = AIB;
                Black = AIA;
            }
            else
            {   
                Random rng = new Random();
                int first = rng.Next(0, 1);

                if (first == 0)
                {
                    White = AIA;
                    Black = AIB;
                }
                else
                {
                    White = AIB;
                    Black = AIA;
                }
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
