namespace ChessProject;

public class MarkerPiece : ChessPiece
{
    public MarkerPiece() : base(ChessBoard.PieceType.Marker, ChessBoard.PieceColor.None, 0)
    {
    }

    public override List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol, bool removeIllegalMoves)
    {
        return null!;
    }

    public override char Piece()
    {
        return 'o';
    }
}