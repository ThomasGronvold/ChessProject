namespace ChessProject;

public abstract class ChessPiece
{
    /* BaseClass for every chess piece. */
    protected ChessBoard.PieceType type;
    protected ChessBoard.PieceColor color;
    protected int pieceValue;
    protected bool isHighlighted;

    protected ChessPiece(ChessBoard.PieceType type, ChessBoard.PieceColor color, int pieceValue)
    {
        this.type = type;
        this.color = color;
        this.pieceValue = pieceValue;
        isHighlighted = false;

    }

    
    public abstract void Move();

    public abstract char Piece();

    public virtual void ToggleHighlight()
    {
        isHighlighted = !isHighlighted;
    }

    public virtual bool CheckHighlight()
    {
        return isHighlighted;
    }
}
