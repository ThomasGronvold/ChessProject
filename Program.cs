using System.Data;
using Microsoft.VisualBasic;
using System;

namespace ChessProject
{

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            bool gameRunning = true;
            var letterRange = GetRange(97, 8); /* The numbers represents the letters a to h in ASCII form */
            var numberRange = GetRange(49, 8); /* The numbers represents the numbers 1 to 8 in ASCII form */

            /* Player side: True = White, False = Black */
            var chessBoard = new ChessBoard(true);

            while (gameRunning)
            {
                var choosePiece = Console.ReadLine();
                int ChosenRowCord = GetCommandInt(numberRange, choosePiece[1]);
                int ChosenColCord = GetCommandInt(letterRange, choosePiece[0]);
                

                chessBoard.HighlightPiece(ChosenRowCord, ChosenColCord);

                chessBoard.UpdateBoard();
                

                //var moveCord = Console.ReadLine();

                //if (choosePiece.Length != 2 ||
                //    moveCord.Length != 2 ||
                //    !letterRange.Contains(choosePiece[0]) ||
                //    !letterRange.Contains(moveCord[0]) ||
                //    !numberRange.Contains(choosePiece[1]) ||
                //    !numberRange.Contains(choosePiece[1])) continue;



                chessBoard.MovePiece(ChosenRowCord, ChosenColCord);
            }
        }

        static int GetCommandInt(char[] array, char userInput)
        {
            return Array.IndexOf(array, userInput);
        }

        static char[] GetRange(int start, int end)
        {
            return Enumerable.Range(start, end).Select(x => (char)x).ToArray();
        }
    }
}


//Console.WriteLine(@"
//8  [♜][♞][♝][♛][♚][♝][♞][♜]
//7  [♟][♟][♟][♟][♟][♟][♟][♟]    
//6  [ ][ ][ ][ ][ ][ ][ ][ ]
//5  [ ][ ][ ][ ][ ][ ][ ][ ]
//4  [ ][ ][ ][ ][ ][ ][ ][ ]
//3  [ ][ ][ ][ ][ ][ ][ ][ ]
//2  [♙][♙][♙][♙][♙][♙][♙][♙]
//1  [♖][♘][♗][♚][♛][♗][♘][♖]
//
//    a  b  c  d  e  f  g  h
//");