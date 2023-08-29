using System.Data;
using Microsoft.VisualBasic;
using System;
using System.Text.RegularExpressions;

namespace ChessProject
{

    internal abstract class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;


            bool gameRunning = true;
            var letterRange = GetRange(97, 8); /* The numbers in range represents the letters a to h in ASCII form */
            var numberRange = GetRange(49, 8); /* The numbers in range represents the numbers 1 to 8 in ASCII form */


            var chessBoard = new ChessBoard(true); /* Player side: True = White, False = Black */
            var turnOrder = true;  /* Player side: True = White, False = Black */
            Regex legalBoardMoves = new Regex("^[a-h][1-8]$");

            while (gameRunning)
            {
                bool pieceSelected = false;

                while (!pieceSelected)
                {
                    int chosenRowCord = -1, chosenColCord = -1, moveRowCord = -1, moveColCord = -1;
                    string choosePiece;

                    do
                    {
                        Console.Write($"{(turnOrder ? "White" : "Black")} To Move: ");
                        choosePiece = Console.ReadLine();
                        if (choosePiece.Length != 2) continue;
                        chosenRowCord = GetCommandInt(numberRange, choosePiece[1]);
                        chosenColCord = GetCommandInt(letterRange, choosePiece[0]);

                    } while (!legalBoardMoves.IsMatch(choosePiece) ||
                             !chessBoard.IsValidChosenPiece(chosenRowCord, chosenColCord, turnOrder));

                    pieceSelected = true;
                    chessBoard.ToggleValidMovesAndHighlight(chosenRowCord, chosenColCord, turnOrder);
                    chessBoard.UpdateBoard();

                    do
                    {
                        Console.Write("('back' to select a new piece) Where To Move Cord: ");
                        var moveCord = Console.ReadLine();
                        if (moveCord.ToLower() == "back")
                        {
                            pieceSelected = false;
                            break;
                        }
                        if (moveCord.Length != 2) continue;
                        moveRowCord = GetCommandInt(numberRange, moveCord[1]);
                        moveColCord = GetCommandInt(letterRange, moveCord[0]);
                    } while (moveRowCord == null ||
                             moveColCord == null ||
                             !chessBoard.IsValidMove(moveRowCord, moveColCord));

                    if (!pieceSelected)
                    {
                        chessBoard.ToggleValidMovesAndHighlight(chosenRowCord, chosenColCord, turnOrder);
                        chessBoard.UpdateBoard();
                        continue;
                    }

                    chessBoard.ToggleValidMovesAndHighlight(chosenRowCord, chosenColCord, turnOrder);
                    chessBoard.MovePiece(chosenRowCord, chosenColCord, moveRowCord, moveColCord, turnOrder);
                    chessBoard.UpdateBoard();
                    turnOrder = !turnOrder;
                }
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