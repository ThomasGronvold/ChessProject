using System.Linq;

namespace ChessProject;

public class ChessBoard
{
    private readonly ChessPiece[,] _board;
    private readonly int rows;
    private readonly int cols;
    private readonly bool _playersPieceColor;
    private readonly PieceColor _playerColor;
    private readonly PieceColor _opponentColor;
    private List<(int, int)> validMoves;
    private int boardSize = 8;

    public ChessBoard(bool playersPieceColor)
    {
        _board = new ChessPiece[boardSize, boardSize];
        rows = _board.GetLength(0);
        cols = _board.GetLength(1);
        _playersPieceColor = playersPieceColor;
        _playerColor = _playersPieceColor ? PieceColor.White : PieceColor.Black;
        _opponentColor = _playersPieceColor ? PieceColor.Black : PieceColor.White;
        InitializePieces();
        ReverseRows(_board);
        UpdateBoard();
    }

    private void InitializePieces()
    {
        /* Init player Pawns */
        for (int col = 0; col < 8; col++) _board[1, col] = new Pawn(_opponentColor);
        for (int col = 0; col < 8; col++) _board[6, col] = new Pawn(_playerColor);

        /* For testing */
        _board[5, 1] = new Pawn(_opponentColor);
    }

    public void UpdateBoard()
    {
        Console.Clear();
        for (int row = rows - 1; row >= 0; row--)
        {
            for (int col = 0; col < cols; col++)
            {
                var isNull = _board[row, col] != null; /* Checks if current square in the array is empty or has a piece in it */

                Console.BackgroundColor =
                    isNull && _board[row, col].isHighlighted ? ConsoleColor.DarkYellow : /* Checks if the piece is the selected piece (to be moved) */
                    isNull && _board[row, col].canBeCaptured ? ConsoleColor.Cyan : /* Checks if the current piece can be captured by the selected piece */
                    (row + col) % 2 == 0 ? ConsoleColor.DarkRed : ConsoleColor.Black; /* If there is no piece, the color of the square will alternate between darkRed/Black */

                Console.Write(isNull ? " " + _board[row, col].Piece() + " " : "   "); /* Looks into the array for pieces, then print them. */
            }
            Console.ResetColor();
            Console.WriteLine(" " + (char)('1' + row));
        }
        Console.Write(" a  b  c  d  e  f  g  h");
        Console.WriteLine();
    }

    private void ReverseRows<T>(T[,] array)
    {
        for (int row = 0; row < rows / 2; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                // Swap elements in the row (Swaps the board around)
                (array[row, col], array[rows - row - 1, col]) = (array[rows - row - 1, col], array[row, col]);
            }
        }
    }

    public void MarkValidMoves(int selectedRow, int selectedCol)
    {
        var selectedPiece = _board[selectedRow, selectedCol];
        validMoves = selectedPiece.GetValidMoves(_board, selectedRow, selectedCol);

        foreach ((int row, int col) in validMoves)
        {
            var currentPiece = _board[row, col];

            if (currentPiece?.type == PieceType.Marker) _board[row, col] = null;
            else
            {
                _board[row, col] = currentPiece ?? new MarkerPiece();
                currentPiece?.ToggleCanBeCaptured();
            }
        }
    }

    public bool IsValidMove(int selectedRow, int selectedCol)
    {
        return validMoves.Contains((selectedRow, selectedCol));
    }

    public void MovePiece(int pieceRow, int pieceCol, int moveRow, int moveCol)
    {
        _board[moveRow, moveCol] = new Pawn(PieceColor.White);
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