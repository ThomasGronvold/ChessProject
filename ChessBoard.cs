using System.Linq;

namespace ChessProject;

public class ChessBoard
{
    private readonly ChessPiece[,] _board;
    private readonly int rows;
    private readonly int cols;
    private readonly bool _playersPieceColor;
    private List<(int, int)> validMoves;

    public ChessBoard(bool playersPieceColor)
    {
        _board = new ChessPiece[8, 8];
        rows = _board.GetLength(0);
        cols = _board.GetLength(1);
        _playersPieceColor = playersPieceColor;
        InitializePieces();
        ReverseRows(_board);
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

    public void UpdateBoard()
    {
        Console.Clear();

        for (int row = rows - 1; row >= 0; row--)
        {
            Console.WriteLine();
            for (int col = 0; col < cols; col++)
            {
                /* Sets the background color of a square */
                if (_board[row, col] != null && _board[row, col].CheckHighlight())
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.DarkRed : ConsoleColor.Black;
                }

                /* Looks into the array for pieces that match pieces, then print them. */
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

    private void ReverseRows<T>(T[,] array)
    {
        for (int row = 0; row < rows / 2; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Swap elements in the row
                (array[row, col], array[rows - row - 1, col]) = (array[rows - row - 1, col], array[row, col]);
            }
        }
    }

    public void MarkValidMoves(int selectedRow, int selectedCol)
    {
        var selectedPiece = _board[selectedRow, selectedCol];
        validMoves = selectedPiece.GetValidMoves(_board, selectedRow, selectedCol);

        int originalCursorTop = Console.CursorTop;
        int originalCursorLeft = Console.CursorLeft;

        foreach ((int row, int col) in validMoves)
        {
            if (_board[row, col] != null)
            {
                Console.SetCursorPosition(col, row);
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write(" " + _board[row, col].Piece() + " ");

            }
            else
            {
                _board[row, col] = _board[row, col] == null ? new MarkerPiece() : null;
            }
            Console.SetCursorPosition(originalCursorLeft, originalCursorTop);
        }
    }

    public bool IsValidMove(int selectedRow, int selectedCol)
    {
        return validMoves.Contains((selectedRow, selectedCol));
    }


    public void MovePiece(int pieceRow, int pieceCol, int moveRow, int moveCol)
    {
        _board[moveRow, moveCol] = new Pawn(PieceColor.White); /*_board[pieceRow, moveCol];*/
        _board[pieceRow, pieceCol] = null;
        UpdateBoard();
    }

    public void HighlightPiece(int row, int col)
    {
        if (_board[row, col] != null) _board[row, col].ToggleHighlight();
    }

    public enum PieceType
    {
        Pawn,
        Knight,
        Bishop,
        Rook,
        Queen,
        King,
        Marker
    }

    public enum PieceColor
    {
        White,
        Black,
        None
    }
}

/* Se på null, lage en null metode? */


//private void InitializeBoard()
//{
//    for (int i = 0; i < rows / 2; i++)
//    {
//        for (int j = 0; j < cols; j++)
//        {
//            (_board[i, j], _board[rows - 1 - i, j]) = (_board[rows - 1 - i, j], _board[i, j]);
//        }
//    }

//    for (int row = 0; row < rows; row++)
//    {
//        Console.WriteLine();

//        for (int col = 0; col < cols; col++)
//        {
//            Console.BackgroundColor = (row + col) % 2 == 0 ? ConsoleColor.DarkRed : ConsoleColor.Black;

//            /* Looks into the array for pieces that match Pawns, then print them. */
//            if (_board[row, col] is Pawn)
//            {
//                Console.Write(" " + _board[row, col].Piece() + " ");
//            }
//            else
//            {
//                Console.Write("   ");
//            }
//        }
//    }
//    Console.ResetColor();
//    Console.WriteLine();
//}

