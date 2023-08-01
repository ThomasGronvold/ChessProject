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
                /* Much repetition, No error handeling, index out of range,   */

                Console.Write("Piece To Move Cord: ");
                var choosePiece = Console.ReadLine();
                int ChosenRowCord = GetCommandInt(numberRange, choosePiece[1]);
                int ChosenColCord = GetCommandInt(letterRange, choosePiece[0]);

                /* Creates MarkerPieces and highlight selected piece in the 2dArray */
                chessBoard.HighlightPiece(ChosenRowCord, ChosenColCord);
                chessBoard.MarkValidMoves(ChosenRowCord, ChosenColCord);
                
                chessBoard.UpdateBoard();

                /* Removes markerpieces and highlight */
                chessBoard.HighlightPiece(ChosenRowCord, ChosenColCord);
                chessBoard.MarkValidMoves(ChosenRowCord, ChosenColCord);

                /* This Next <-------------- */
                int moveRowCord;
                int moveColCord;
                do
                {
                    Console.Write("Where To Move Cord: ");
                    var moveCord = Console.ReadLine(); 
                    moveRowCord = GetCommandInt(numberRange, moveCord[1]);
                    moveColCord = GetCommandInt(letterRange, moveCord[0]);
                } while (!chessBoard.IsValidMove(moveRowCord, moveColCord));
                
                chessBoard.MovePiece(ChosenRowCord, ChosenColCord, moveRowCord, moveColCord);
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

//if (choosePiece.Length != 2 ||
//    moveCord.Length != 2 ||
//    !letterRange.Contains(choosePiece[0]) ||
//    !letterRange.Contains(moveCord[0]) ||
//    !numberRange.Contains(choosePiece[1]) ||
//    !numberRange.Contains(choosePiece[1])) continue;