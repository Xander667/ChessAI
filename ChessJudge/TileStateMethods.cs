using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ChessJudge;
using System.Threading.Tasks;

namespace ChessJudge
{
    static class TileStateMethods
    {
        public static string printShortForm(this Judge.TileState state)
        {
            string logFilePath = Directory.GetCurrentDirectory();
            StringBuilder log = new StringBuilder();
            switch (state)
            {
                case Judge.TileState.Empty:
                    return " E ";
                case Judge.TileState.BlackPawn:
                    return" BP"; 
                case Judge.TileState.BlackBishop:
                    return" BB"; 
                case Judge.TileState.BlackKnight:
                    return" BK"; 
                case Judge.TileState.BlackCastle:
                    return" BC"; 
                case Judge.TileState.BlackKing:
                    return" BX"; 
                case Judge.TileState.BlackQueen:
                    return" BQ"; 
                case Judge.TileState.WhitePawn:
                    return" WP"; 
                case Judge.TileState.WhiteBishop:
                    return" WB"; 
                case Judge.TileState.WhiteKnight:
                    return" WK"; 
                case Judge.TileState.WhiteCastle:
                    return" WC"; 
                case Judge.TileState.WhiteKing:
                    return" WX";
                case Judge.TileState.WhiteQueen:
                    return" WQ"; 
                default:
                    return" X"; 
            }
        }    
    }
}
