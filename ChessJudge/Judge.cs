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
        public int[,] boardState{ get; set; }
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
            boardState = new int[8,8];

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

        public int[,] SetupBoard(int[,] board, string white)
        {
            int[,] currentBoard = board;

            if (white.ToUpper().Equals("AIA"))
            {

            }


            return currentBoard;
        }
    }
}
