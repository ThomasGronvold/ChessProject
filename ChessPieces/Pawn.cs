using System.Text.RegularExpressions;
using static ChessProject.ChessBoard;

namespace ChessProject;

public class Pawn : ChessPiece
{
    public Pawn(PieceColor color)
        : base(PieceType.Pawn, color, 1)
    {
    }

    public override List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol, bool removeIllegalMoves)
    {
        var validMoves = new List<(int, int)>();
        int direction = (this.color == PieceColor.White ? 1 : -1);

        //Single Forward Move: A pawn can move one square forward if the square in front of it is empty.
        var nextRow = currentRow + direction;

        if (nextRow >= 0 && nextRow <= 7 && (board[nextRow, currentCol] == null) || (board[nextRow, currentCol] is MarkerPiece))
        {
            if (!validMoves.Contains((nextRow, currentCol))) validMoves.Add((nextRow, currentCol));

            //Double Forward Move: A pawn can move two squares forward from its starting position if both the squares in front of it are empty.
            int doublePawnMove = currentRow + 2 * direction;

            if ((currentRow == 1 && direction == 1 || currentRow == 6 && direction == -1) &&
                board[doublePawnMove, currentCol] is null or MarkerPiece)
            {
                if (!validMoves.Contains((doublePawnMove, currentCol))) validMoves.Add((doublePawnMove, currentCol));
            }
        }

        //Capture Diagonally: A pawn can capture an opponent's piece diagonally one square forward and to the left or right.
        int leftCol = currentCol - 1;
        int rightCol = currentCol + 1;

        if (nextRow >= 0 && nextRow <= 7)
        {
            if (leftCol >= 0 && (board[nextRow, leftCol] != null) && (board[nextRow, leftCol].color != this.color))
            {
                if (!validMoves.Contains((nextRow, leftCol))) validMoves.Add((nextRow, leftCol));
            }

            if (rightCol <= 7 && (board[nextRow, rightCol] != null) && (board[nextRow, rightCol].color != this.color))
            {
                if (!validMoves.Contains((nextRow, rightCol))) validMoves.Add((nextRow, rightCol));
            }
        }

        //En Passant: a pawn can capture an opponent's pawn that has just moved two squares forward from its starting position.
        int[] outOfBoundChecks = { -1, 1 };

        foreach (var colVariation in outOfBoundChecks)
        {
            var colToCheck = currentCol + colVariation;

            if (currentCol - 1 >= 0 && currentCol + 1 < 8 && board[currentRow, colToCheck] != null)
            {
                if (board[currentRow, colToCheck] is MarkerPiece || board[currentRow, colToCheck].IsEnPassant)
                {
                    if (!validMoves.Contains((nextRow, colToCheck))) validMoves.Add((nextRow, colToCheck));
                    break;
                }
            }
        }

        if (removeIllegalMoves)
        {
            validMoves = RemoveIllegalMoves(board, validMoves, currentRow, currentCol);
        }

        return validMoves;
    }

    public override char Piece()
    {
        return (color == PieceColor.White) ? '♙' : '♟';
    }
}