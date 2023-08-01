namespace ChessProject;

public abstract class ChessPiece
{
    /* BaseClass for every chess piece. */
    protected ChessBoard.PieceType type;
    public ChessBoard.PieceColor color;
    protected int pieceValue;
    protected bool isHighlighted;

    protected ChessPiece(ChessBoard.PieceType type, ChessBoard.PieceColor color, int pieceValue)
    {
        this.type = type;
        this.color = color;
        this.pieceValue = pieceValue;
        isHighlighted = false;
    }

    public abstract List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol);

    public abstract char Piece();

    public void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
    }

    public bool CheckHighlight()
    {
        return isHighlighted;
    }
}
