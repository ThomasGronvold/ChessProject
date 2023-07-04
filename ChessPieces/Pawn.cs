using System.Drawing;
using static ChessProject.ChessBoard;

namespace ChessProject;

public class Pawn : ChessPiece
{
    public Pawn(PieceColor color) 
        : base(PieceType.Pawn, color, 1)
    {
    }

    public override char Piece()
    {
        return (color == PieceColor.White) ? '♙' : '♟';
    }

    public override void Move()
    {
        throw new NotImplementedException();
    }
}