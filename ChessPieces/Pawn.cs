using System.Drawing;
using static ChessProject.ChessBoard;

namespace ChessProject;

public class Pawn : ChessPiece
{
    public Pawn(PieceColor color)
        : base(PieceType.Pawn, color, 1)
    {
    }

    public override List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol)
    {
        var validMoves = new List<(int, int)>();

        if (currentCol != 7 && board[(currentRow + 1), (currentCol + 1)] != null && board[(currentRow + 1), (currentCol + 1)].color != this.color)
        {
            validMoves.Add((currentRow + 1, currentCol + 1));
        }

        if (currentCol != 0 && board[(currentRow + 1), (currentCol - 1)] != null && board[(currentRow + 1), (currentCol - 1)].color != this.color)
        {
            validMoves.Add((currentRow + 1, currentCol - 1));
        }

        if (currentRow == 1 && (board[currentRow + 1, currentCol] is null || board[currentRow + 1, currentCol] is MarkerPiece) && (board[currentRow + 2, currentCol] is null || board[currentRow + 2, currentCol] is MarkerPiece))
        {
            validMoves.Add((currentRow + 1, currentCol));
            validMoves.Add((currentRow + 2, currentCol));
        }
        else if (board[currentRow + 1, currentCol] is null || board[currentRow + 1, currentCol] is MarkerPiece)
        {
            validMoves.Add((currentRow + 1, currentCol));
        }

        return validMoves;
    }

    public override char Piece()
    {
        return (color == PieceColor.White) ? '♙' : '♟';
    }
}