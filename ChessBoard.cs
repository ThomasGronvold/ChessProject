using System.Linq;
using System.Runtime.InteropServices.JavaScript;
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
    private readonly PieceColor _white;
    private readonly PieceColor _black;

    /* Mechanic Fields */
    private List<(int, int)> validMoves;

    public ChessBoard()
    {
        /* Board Constructors */
        _board = new ChessPiece[boardSize, boardSize];
        rows = _board.GetLength(0);
        cols = _board.GetLength(1);

        /* Chess Piece Constructors */
        _white = PieceColor.White;
        _black = PieceColor.Black;

        /* Function Constructors */
        InitializePieces();
        ReverseRows(_board);
        UpdateBoard();
    }

    private void InitializePieces()
    {
        /* Init Pawns */
        for (int col = 0; col < 8; col++) _board[1, col] = new Pawn(_black);
        for (int col = 0; col < 8; col++) _board[6, col] = new Pawn(_white);
        /* Init Rooks */
        _board[7, 0] = new Rook(_white);
        _board[7, 7] = new Rook(_white);
        _board[0, 0] = new Rook(_black);
        _board[0, 7] = new Rook(_black);
        /* Init Knights */
        //_board[7, 1] = new Knight(_white);
        //_board[7, 6] = new Knight(_white);
        //_board[0, 1] = new Knight(_black);
        //_board[0, 6] = new Knight(_black);
        ///* Init Bishops */
        //_board[7, 2] = new Bishop(_white);
        //_board[7, 5] = new Bishop(_white);
        //_board[0, 2] = new Bishop(_black);
        //_board[0, 5] = new Bishop(_black);
        ///* Init Queens */
        //_board[7, 3] = new Queen(_white);
        //_board[0, 3] = new Queen(_black);
        /* Init Kings */
        _board[7, 4] = new King(_white);
        _board[0, 4] = new King(_black);

        /* For testing */
        //_board[1, 1] = new Pawn(_white);
        //_board[0, 1] = null;

        //_board[4, 3] = new King(_white);
        //_board[4, 5] = new King(_black);

        /* Testing leagal moves */

        //_board[7, 7] = new King(_white);
        //_board[7, 5] = new King(_black);
        //_board[5, 5] = new Knight(_white);
        //_board[5, 0] = new Queen(_black);
        //_board[7, 2] = new Queen(_black);
        //_board[0, 4] = new Queen(_white);w


        //_board[4, 4] = new Bishop(_white);
        //_board[5, 0] = new Rook(_white);
        //_board[5, 5] = new Rook(_black);
        //_board[3, 6] = new Pawn(_white);
        //_board[4, 1] = new Pawn(_black);
        //_board[3, 1] = new Pawn(_black);
        //_board[4, 3] = new Pawn(_black);
    }

    public void UpdateBoard()
    {
        Console.Clear();
        for (int row = rows - 1; row >= 0; row--)
        {
            for (int col = 0; col < cols; col++)
            {
                var piece = _board[row, col];
                var isNull = piece != null; /* Checks if current square in the array is empty or has a piece in it */

                Console.BackgroundColor =
                    isNull && piece.CheckHighlight() ? ConsoleColor.DarkYellow : /* Checks if the piece is the selected piece (to be moved) */
                    isNull && piece.CheckCanBeCaptured() ? ConsoleColor.Cyan : /* Checks if the current piece can be captured by the selected piece */
                    (row + col) % 2 == 0 ? ConsoleColor.DarkBlue : ConsoleColor.DarkCyan; /* If there is no piece, the color of the square will alternate between darkRed/Black */

                /* Makes the black pieces appear black and not white */
                Console.ForegroundColor = piece != null && piece.color == PieceColor.Black ? ConsoleColor.Black : ConsoleColor.White;

                Console.Write(isNull ? " " + piece.Piece() + " " : "   "); /* Looks into the array for pieces, then print them. */
            }
            Console.ResetColor();
            Console.WriteLine(" " + (char)('1' + row));
        }
        Console.Write(" a  b  c  d  e  f  g  h");
        Console.WriteLine();
    }

    public void ToggleValidMovesAndHighlight(int ChosenRowCord, int ChosenColCord, bool turnOrder)
    {
        MarkValidMoves(ChosenRowCord, ChosenColCord, turnOrder);
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

    public void MarkValidMoves(int selectedRow, int selectedCol, bool turnOrder)
    {
        var selectedPiece = _board[selectedRow, selectedCol];
        validMoves = selectedPiece.GetValidMoves(_board, selectedRow, selectedCol, true);


        foreach ((int row, int col) in validMoves)
        {
            if (_board[row, col] == null)
            {
                _board[row, col] = new MarkerPiece();
            }
            //else if (_board[row, col].type == PieceType.Marker)
            //{
            //    _board[row, col] = null;
            //}
            else if (_board[row, col].color == (turnOrder ? _black : _white))
            {
                _board[row, col].ToggleCanBeCaptured();
            }
        }
    }

    public bool IsValidChosenPiece(int selectedRow, int selectedCol, bool turnOrder)
    {
        if (_board[selectedRow, selectedCol] == null) return false;

        var checkChosenPieceNotOpponent = _board[selectedRow, selectedCol].color == PieceColor.White;
        if (checkChosenPieceNotOpponent != turnOrder) return false;

        var validChosenPiece = _board[selectedRow, selectedCol].GetValidMoves(_board, selectedRow, selectedCol, true);
        if (validChosenPiece.Count == 0) return false;
        return true;
    }

    public bool IsValidMove(int selectedRow, int selectedCol)
    {
        return validMoves.Contains((selectedRow, selectedCol));
    }

    public void MovePiece(int pieceRow, int pieceCol, int moveRow, int moveCol, bool turnOrder)
    {
        var color = turnOrder ? PieceColor.White : PieceColor.Black;
        var currentPiecePos = _board[pieceRow, pieceCol];

        /* Creates a new chessPiece at the chosen location */
        if (currentPiecePos.type is PieceType.Pawn)
        {
            if (moveRow == 7)
            {
                var validPromotionOptions = new string[] { "1", "2", "3", "4" };
                string choice;

                Console.WriteLine("Promotion options: '1'=Queen, '2'=Rook, '3'=Bishop, '4'=Knight");
                do
                {
                    Console.Write("Choice -> ");
                    choice = Console.ReadLine();

                } while (!validPromotionOptions.Contains(choice));

                _board[moveRow, moveCol] = (
                    choice == "1" ? new Queen(color) :
                    choice == "2" ? new Rook(color) :
                    choice == "3" ? new Bishop(color) :
                    new Knight(color)
                    );
            }
            else
            {
                _board[moveRow, moveCol] = new Pawn(color);
            }
        }
        else if (currentPiecePos.type is PieceType.King && !currentPiecePos.hasMoved && moveCol == 6 || moveCol == 2)
        {
            Console.WriteLine("");
            /* King castling white side */
            if (moveRow == 0 && moveCol == 6)
            {
                _board[0, 5] = new Rook(_white);
                _board[0, 6] = _board[pieceRow, pieceCol];
                _board[0, 7] = null;
            }
            else if (moveRow == 0 && moveCol == 2)
            {
                _board[0, 3] = new Rook(_white);
                _board[0, 2] = _board[pieceRow, pieceCol];
                _board[0, 0] = null;
            }
            /* King castling black side */
            if (moveRow == 7 && moveCol == 6)
            {
                _board[7, 5] = new Rook(_black);
                _board[7, 6] = _board[pieceRow, pieceCol];
                _board[7, 7] = null;
            }
            else if (moveRow == 7 && moveCol == 2)
            {
                _board[7, 3] = new Rook(_black);
                _board[7, 2] = _board[pieceRow, pieceCol];
                _board[7, 0] = null;
            }
        }
        else
        {
            _board[moveRow, moveCol] = _board[pieceRow, pieceCol];
        }

        if (currentPiecePos.type is PieceType.King or PieceType.Rook && !currentPiecePos.hasMoved)
        {
            _board[pieceRow, pieceCol].PieceHasMoved();
        }

        if (_board[moveRow, moveCol] is Pawn &&
            moveRow - pieceRow == 2 ||
            moveRow - pieceRow == -2)
        {
            _board[moveRow, moveCol].ToggleEnPassant();
        }

        // En passant capture
        if (pieceCol - 1 >= 0 && _board[pieceRow, pieceCol - 1] != null && _board[pieceRow, pieceCol - 1].IsEnPassant)
        {
            if ((moveRow == pieceRow - 1 && moveCol == pieceCol - 1) || // Left capture for white
                (moveRow == pieceRow + 1 && moveCol == pieceCol - 1))   // Left capture for black
            {
                _board[pieceRow, pieceCol - 1] = null;
            }
        }

        if (pieceCol + 1 < 8 && _board[pieceRow, pieceCol + 1] != null && _board[pieceRow, pieceCol + 1].IsEnPassant)
        {
            if ((moveRow == pieceRow - 1 && moveCol == pieceCol + 1) || // Right capture for white
                (moveRow == pieceRow + 1 && moveCol == pieceCol + 1))   // Right capture for black
            {
                _board[pieceRow, pieceCol + 1] = null;
            }
        }

        _board[pieceRow, pieceCol] = null;

        ClearEnPassantFlags(turnOrder ? PieceColor.Black : PieceColor.White);
    }


    public void ClearMarkerPieces()
    {
        for (int row = 7; row >= 0; row--)
        {
            for (int col = 0; col < cols; col++)
            {
                var piece = _board[row, col];
                if (piece != null && _board[row, col].type is PieceType.Marker) _board[row, col] = null;
            }
        }
    }

    private void ClearEnPassantFlags(PieceColor color)
    {
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                var piece = _board[row, col];
                if (piece != null && piece.IsEnPassant && piece.color == color)
                {
                    _board[row, col].ClearEnPassant();
                }
            }
        }
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