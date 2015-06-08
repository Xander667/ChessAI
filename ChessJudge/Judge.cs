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
        string Winner;

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

        public TurnState Swap(TurnState state)
        {
            if(state == TurnState.White)
            {
                return TurnState.Black;
            }
            else
            {
                return TurnState.White;
            }
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
                currentTurn = Swap(currentTurn);
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
            //Get all the differences between boards.
            List<TileStateChange> changes = getChangesInBoards(boardBefore, boardAfter);
            if(changes.Count > 1)
            {
                log.AppendLine("Game Over. Too many changes in move.");
                log.AppendLine("Winner: " + Swap(currentTurn).ToString());
                log.AppendLine("List of suggested changes.");

                foreach(TileStateChange change in changes)
                {
                    log.AppendLine(change.Letter.ToString() + "," + change.Number.ToString() + " :" + change.oldState.ToString() + "  -->  " + change.newState.ToString());
                }

                File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", log.ToString());
                log.Clear();
                Winner = currentTurn.ToString();
                return true;
            }

            TileStateChange MoveFrom, MoveTo;
            MoveFrom = changes.First(row => row.newState == TileState.Empty);
            MoveTo = changes.First(row => row != MoveFrom);

            if(MoveFrom.oldState == TileState.Empty)
            {
                log.AppendLine("Pawn moved horizontal or backwards.");
                PrintBadMove(log, MoveFrom, MoveTo);
                return false;
            }
            else if(MoveFrom.oldState == TileState.WhitePawn)
            {
                //Letter must be one ahead.
                //Number must be equal +/- 1
                if ((MoveTo.Letter != MoveFrom.Letter + 1) || (MoveFrom.Number > MoveTo.Number + 1) || (MoveFrom.Number < MoveTo.Number -1))
                {
                    log.AppendLine("Pawn moved horizontal or backwards.");
                    PrintBadMove(log, MoveFrom, MoveTo);
                    return false;
                }
            }
            else if(MoveFrom.oldState == TileState.BlackPawn)
            {
                //Letter must be one behind.
                //Number must be equal +/- 1
                if ((MoveTo.Letter != MoveFrom.Letter - 1) || (MoveFrom.Number > MoveTo.Number + 1) || (MoveFrom.Number < MoveTo.Number - 1))
                {
                    log.AppendLine("Pawn moved horizontal or backwards.");
                    PrintBadMove(log, MoveFrom, MoveTo);
                    return false;
                }
            }
            else if((MoveFrom.oldState == TileState.WhiteKing) || (MoveFrom.oldState  == TileState.BlackKing))
            {
                //Letter must be one ahead or behind
                //Number must be one ahead or behind
                if (((MoveTo.Number != MoveFrom.Number - 1) && (MoveTo.Number != MoveFrom.Number + 1)) || ((MoveTo.Letter != MoveFrom.Letter + 1) && (MoveTo.Letter != MoveFrom.Letter + 1)))
                {
                    log.AppendLine("King moved more than one space.");
                    PrintBadMove(log, MoveFrom, MoveTo);
                    return false;
                }
            }
            else if ((MoveFrom.oldState == TileState.WhiteCastle) || (MoveFrom.oldState == TileState.BlackCastle))
            {
                if (MoveFrom.Number == MoveTo.Number)
                {
                    //Peice moved right
                    if (MoveFrom.Letter < MoveTo.Letter)
                    {
                        for (int i = MoveFrom.Letter + 1; i < MoveTo.Letter; i++)
                        {
                            //Verify each tile between is empty.
                            if (boardBefore[i, MoveFrom.Number] != TileState.Empty)
                            {
                                log.AppendLine("Castle is moving through a unit!");
                                log.AppendLine("Unit: " + boardBefore[i, MoveFrom.Number].ToString());
                                log.AppendLine("Position: " + i + "," + MoveFrom.Number);

                                PrintBadMove(log, MoveFrom, MoveTo);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //Peice moved left
                        for (int i = MoveFrom.Letter - 1; i > MoveTo.Letter; i--)
                        {
                            //Verify each tile between is empty.
                            if (boardBefore[i, MoveFrom.Number] != TileState.Empty)
                            {
                                log.AppendLine("Castle is moving through a unit!");
                                log.AppendLine("Unit: " + boardBefore[i, MoveFrom.Number].ToString());
                                log.AppendLine("Position: " + i + "," + MoveFrom.Number);

                                PrintBadMove(log, MoveFrom, MoveTo);
                                return false;
                            }
                        }
                    }
                }
                else if(MoveTo.Letter == MoveFrom.Letter)
                {
                    //Peice moved up
                    if (MoveFrom.Number < MoveTo.Number)
                    {
                        for (int i = MoveFrom.Number + 1; i < MoveTo.Number; i++)
                        {
                            //Verify each tile between is empty.
                            if (boardBefore[i, MoveFrom.Letter] != TileState.Empty)
                            {
                                log.AppendLine("Castle is moving through a unit!");
                                log.AppendLine("Unit: " + boardBefore[i, MoveFrom.Letter].ToString());
                                log.AppendLine("Position: " + i + "," + MoveFrom.Letter);

                                PrintBadMove(log, MoveFrom, MoveTo);
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //Peice moved down
                        for (int i = MoveFrom.Number - 1; i > MoveTo.Number; i--)
                        {
                            //Verify each tile between is empty.
                            if (boardBefore[i, MoveFrom.Letter] != TileState.Empty)
                            {
                                log.AppendLine("Castle is moving through a unit!");
                                log.AppendLine("Unit: " + boardBefore[i, MoveFrom.Letter].ToString());
                                log.AppendLine("Position: " + i + "," + MoveFrom.Letter);

                                PrintBadMove(log, MoveFrom, MoveTo);
                                return false;
                            }
                        }
                    }
                }
                else if ((MoveTo.Number != MoveFrom.Number) && (MoveTo.Letter != MoveFrom.Letter))  //Either letter or number must be equal, only one axis can be different.
                {
                    log.AppendLine("Castle is moving on both axis!");
                    PrintBadMove(log, MoveFrom, MoveTo);
                    return false;
                }
            }

            //Verify not suicide || stalemate || win

            File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", log.ToString());
            log.Clear();

            //If moving peice is anything but a knight we need to make sure they didn't move through another peice.
            if(MoveFrom.oldState != TileState.BlackKnight || MoveFrom.oldState != TileState.WhiteKnight)
            {
              

            }

            return false;
        }

        public void PrintBadMove(StringBuilder logger, TileStateChange moveFrom, TileStateChange moveTo)
        {
            logger.AppendLine("Chess Peice: " + moveFrom.oldState);
            logger.AppendLine("Old Location: " + moveFrom.Letter.ToString() + "," + moveFrom.Number.ToString());
            logger.AppendLine("New Location: " + moveTo.Letter.ToString() + "," + moveTo.Number.ToString());
            logger.AppendLine("New Location previous state: " + moveTo.oldState);
            File.AppendAllText(logFilePath + "log" + DateTime.Now.ToString() + ".txt", logger.ToString());
            logger.Clear();
        }


        public List<TileStateChange> getChangesInBoards(TileState[,] boardBefore, TileState[,] boardAfter)
        {
            List<TileStateChange> changesInBoards = new List<TileStateChange>();

            //Loop through a baord comparing to the other board
            for (int i = 0; i < 7; i++ )
            {
                for(int j = 0; j < 7; j++)
                {
                    if(boardBefore[i,j] != boardAfter[i,j])
                    {
                        //We found a change :D
                        TileStateChange changedTile = new TileStateChange(i, j, boardBefore[i, j], boardAfter[i, j]);
                        changesInBoards.Add(changedTile);
                    }
                }
            }

            return changesInBoards;
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
