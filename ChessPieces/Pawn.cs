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

/*
        Pawn
        Cant make illegal moves (move makes king check)
        if enemy pawn moved 2 and landed next you your own pawn, you can en passant
        if enemy piece is on diagonal infront of pawn, it can attack
        if on start row, can move 2 forward unless blocked
        else can move 1 forward unless blocked
*/