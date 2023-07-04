using System.Linq;

namespace ChessProject;

public class ChessBoard
{
    private readonly ChessPiece[,] _board;
    private readonly int rows;
    private readonly int cols;
    private readonly bool _playersPieceColor;

    public ChessBoard(bool playersPieceColor)
    {
        _board = new ChessPiece[8, 8];
        rows = _board.GetLength(0);
        cols = _board.GetLength(1);
        _playersPieceColor = playersPieceColor;
        InitializePieces();
        //InitializeBoard();
        UpdateBoard();
    }

    private void InitializePieces()
    {
        /* Init player Pawns */
        var playerColor = _playersPieceColor ? PieceColor.White : PieceColor.Black;
        var opponentColor = _playersPieceColor ? PieceColor.Black : PieceColor.White;

        for (int col = 0; col < 8; col++) _board[1, col] = new Pawn(opponentColor);
        for (int col = 0; col < 8; col++) _board[6, col] = new Pawn(playerColor);
    }

    private void InitializeBoard()
    {
        for (int i = 0; i < rows / 2; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                (_board[i, j], _board[rows - 1 - i, j]) = (_board[rows - 1 - i, j], _board[i, j]);
            }
        }

        //for (int row = 0; row < rows; row++)
        //{
        //    Console.WriteLine();

        //    for (int col = 0; col < cols; col++)
        //    {
        //        Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.DarkRed : ConsoleColor.Black;

        //        /* Looks into the array for pieces that match Pawns, then print them. */
        //        if (_board[row, col] is Pawn)
        //        {
        //            Console.Write(" " + _board[row, col].Piece() + " ");
        //        }
        //        else
        //        {
        //            Console.Write("   ");
        //        }
        //    }
        //}
        //Console.ResetColor();
        //Console.WriteLine();
    }

    public void UpdateBoard()
    {
        //for (int i = 0; i < rows / 2; i++)
        //{
        //    for (int j = 0; j < cols; j++)
        //    {
        //        (_board[i, j], _board[rows - 1 - i, j]) = (_board[rows - 1 - i, j], _board[i, j]);
        //    }
        //}

        Console.Clear();
        for (int row = 0; row < rows; row++)
        {
            Console.WriteLine();

            for (int col = 0; col < cols; col++)
            {
                if (_board[row, col] != null && _board[row, col].CheckHighlight())
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.DarkRed : ConsoleColor.Black;
                }


                /* Looks into the array for pieces that match Pawns, then print them. */
                if (_board[row, col] != null)
                {
                    Console.Write(" " + _board[row, col].Piece() + " ");
                }
                else
                {
                    Console.Write("   ");
                }
            }
        }
        Console.ResetColor();
        Console.WriteLine();
    }


    public void MovePiece(int row, int col)
    {
        Console.WriteLine($"Col: {col} + row: {row}");
    }

    public void HighlightPiece(int row, int col)
    {
        _board[row, col].ToggleHighlight();
    }

    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King
    }

    public enum PieceColor
    {
        White,
        Black
    }
}

