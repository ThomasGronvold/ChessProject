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
            var letterRange = GetRange(97, 8); /* The numbers in range represents the letters a to h in ASCII form */
            var numberRange = GetRange(49, 8); /* The numbers in range represents the numbers 1 to 8 in ASCII form */

            /* Player side: True = White, False = Black */
            var chessBoard = new ChessBoard(true);

            while (gameRunning)
            {
                int chosenRowCord = -1;
                int chosenColCord = -1;
                int moveRowCord;
                int moveColCord;

                do
                {
                    Console.Write("Piece To Move Cord: ");
                    var choosePiece = Console.ReadLine();
                    if (choosePiece.Length == 2)
                    {
                        chosenRowCord = GetCommandInt(numberRange, choosePiece[1]);
                        chosenColCord = GetCommandInt(letterRange, choosePiece[0]);
                    }
                } while (chosenRowCord == -1 || chosenColCord == -1 || !chessBoard.IsValidChosenPiece(chosenRowCord, chosenColCord));

                chessBoard.ToggleValidMovesAndHighlight(chosenRowCord, chosenColCord);
                chessBoard.UpdateBoard();

                do
                {
                    Console.Write("Where To Move Cord: ");
                    var moveCord = Console.ReadLine();
                    moveRowCord = GetCommandInt(numberRange, moveCord[1]);
                    moveColCord = GetCommandInt(letterRange, moveCord[0]);
                } while (moveRowCord == null || moveColCord == null || !chessBoard.IsValidMove(moveRowCord, moveColCord));

                chessBoard.ToggleValidMovesAndHighlight(chosenRowCord, chosenColCord);
                chessBoard.MovePiece(chosenRowCord, chosenColCord, moveRowCord, moveColCord);
                chessBoard.UpdateBoard();
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