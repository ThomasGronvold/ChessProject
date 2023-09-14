using System.Reflection;
using static ChessProject.ChessBoard;

namespace ChessProject;

public abstract class ChessPiece
{
    /* BaseClass for every chess piece. */
    public ChessBoard.PieceType type { get; }
    public ChessBoard.PieceColor color;
    public int pieceValue;
    protected bool isHighlighted;
    protected bool canBeCaptured;
    public bool hasMoved { get; private set; } /* Keeping track of castling for the king and rooks */
    public bool IsEnPassant { get; private set; } /* Only for the pawn class */

    protected ChessPiece(ChessBoard.PieceType type, ChessBoard.PieceColor color, int pieceValue)
    {
        this.type = type;
        this.color = color;
        this.pieceValue = pieceValue;
        isHighlighted = false;
        canBeCaptured = false;
        hasMoved = false;
    }

    public abstract List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol, bool removeIllegalMoves);

    public abstract char Piece();

    public void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
    }

    public void ToggleCanBeCaptured()
    {
        canBeCaptured = !canBeCaptured;
    }

    public bool CheckHighlight()
    {
        return isHighlighted;
    }

    public bool CheckCanBeCaptured()
    {
        return canBeCaptured;
    }

    /* Pawn specific method */
    public void ToggleEnPassant()
    {
        if (type == ChessBoard.PieceType.Pawn) IsEnPassant = !IsEnPassant;
    }

    public void ClearEnPassant()
    {
        IsEnPassant = false;
    }

    public void PieceHasMoved()
    {
        hasMoved = true;
    }

    public List<(int, int)> RemoveIllegalMoves(ChessPiece[,] board, List<(int, int)> validMoves, int currentRow, int currentCol)
    {
        var validMovesWithoutIllegalMoves = new List<(int, int)>();

        foreach ((int newRow, int newCol) in validMoves)
        {
            var tempBoard = (ChessPiece[,])board.Clone();

            // Move the piece to the new position on the temporary board
            tempBoard[newRow, newCol] = board[currentRow, currentCol];
            tempBoard[currentRow, currentCol] = null;

            /* Checks if the current validMoves move is legal */
            bool isMoveLegal = !IsInCheck(tempBoard);

            if (isMoveLegal)
            {
                validMovesWithoutIllegalMoves.Add((newRow, newCol));
            }
        }

        return validMovesWithoutIllegalMoves;
    }

    public bool IsInCheck(ChessPiece[,] board)
    {
        var opponentColor = (color == ChessBoard.PieceColor.White) ? ChessBoard.PieceColor.Black : ChessBoard.PieceColor.White;

        // Iterate through all opponent pieces on the board that are bishops, queens, rooks. It also checks kings, so that one king can't be on a square next to another. 
        for (int row = 0; row < board.GetLength(0); row++)
        {
            for (int col = 0; col < board.GetLength(1); col++)
            {
                ChessPiece piece = board[row, col];

                if (piece != null && piece.color == opponentColor && piece.type is PieceType.Bishop or PieceType.Queen or PieceType.Rook or PieceType.King)
                {
                    var pieceValidMoves = piece.GetValidMoves(board, row, col, false);

                    foreach (var move in pieceValidMoves)
                    {
                        if (board[move.Item1, move.Item2] != null &&
                            board[move.Item1, move.Item2].color != opponentColor &&
                            board[move.Item1, move.Item2].type == PieceType.King)
                        {
                            return true; /* The king is in check */
                        }
                    }
                }
            }
        }

        return false; /* The king is not in check */
    }
}