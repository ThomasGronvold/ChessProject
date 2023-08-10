using System.Linq;
using static ChessProject.ChessBoard;

namespace ChessProject;

public class ChessBoard
{
    /* Board Fields */
    private readonly ChessPiece[,] _board; /* A two-dimensional array representing the chess board. */
    private int boardSize = 8;
    private readonly int rows;
    private readonly int cols;

    /* Chess Piece Fields */
    private readonly bool _playersPieceColor;
    private readonly PieceColor _playerColor;
    private readonly PieceColor _opponentColor;
    //private ChessPiece selectedPiece;

    /* Mechanic Fields */
    private List<(int, int)> validMoves;

    public ChessBoard(bool playersPieceColor)
    {
        /* Board Constructors */
        _board = new ChessPiece[boardSize, boardSize];
        rows = _board.GetLength(0);
        cols = _board.GetLength(1);

        /* Chess Piece Constructors */
        _playersPieceColor = playersPieceColor;
        _playerColor = _playersPieceColor ? PieceColor.White : PieceColor.Black;
        _opponentColor = _playersPieceColor ? PieceColor.Black : PieceColor.White;

        /* Function Constructors */
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
                    isNull && _board[row, col].CheckHighlight() ?  ConsoleColor.DarkYellow : /* Checks if the piece is the selected piece (to be moved) */
                    isNull && _board[row, col].CheckCanBeCaptured() ? ConsoleColor.Cyan : /* Checks if the current piece can be captured by the selected piece */
                    (row + col) % 2 == 0 ? ConsoleColor.DarkRed : ConsoleColor.Black; /* If there is no piece, the color of the square will alternate between darkRed/Black */

                Console.Write(isNull ? " " + _board[row, col].Piece() + " " : "   "); /* Looks into the array for pieces, then print them. */
            }
            Console.ResetColor();
            Console.WriteLine(" " + (char)('1' + row));
        }
        Console.Write(" a  b  c  d  e  f  g  h");
        Console.WriteLine();
    }

    public void ToggleValidMovesAndHighlight(int ChosenRowCord, int ChosenColCord)
    { 
        MarkValidMoves(ChosenRowCord, ChosenColCord);
        _board[ChosenRowCord, ChosenColCord].ToggleHighlight();
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
            if (_board[row, col] == null)
            {
                _board[row, col] = new MarkerPiece();
            }
            else if (_board[row, col].type == PieceType.Marker)
            {
                _board[row, col] = null;
            }

            if (_board[row, col] != null && _board[row, col].color == _opponentColor)
            {
                _board[row, col].ToggleCanBeCaptured();
            }
        }
    }

    public bool IsValidChosenPiece(int selectedRow, int selectedCol)
    {
        if (_board[selectedRow, selectedCol] == null) return false;
        var validChosenPiece = _board[selectedRow, selectedCol].GetValidMoves(_board, selectedRow, selectedCol);
        if (validChosenPiece.Count == 0 || validChosenPiece == null) return false;
        return true;
    }

    public bool IsValidMove(int selectedRow, int selectedCol)
    {
        return validMoves.Contains((selectedRow, selectedCol));
    }

    public void MovePiece(int pieceRow, int pieceCol, int moveRow, int moveCol)
    {
        _board[moveRow, moveCol] = new Pawn(PieceColor.White);
        _board[pieceRow, pieceCol] = null;
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