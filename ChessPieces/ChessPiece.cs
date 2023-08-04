namespace ChessProject;

public abstract class ChessPiece
{
    /* BaseClass for every chess piece. */
    public ChessBoard.PieceType type { get; }
    public ChessBoard.PieceColor color;
    protected int pieceValue;
    protected bool isHighlighted;
    protected bool canBeCaptured;

    protected ChessPiece(ChessBoard.PieceType type, ChessBoard.PieceColor color, int pieceValue)
    {
        this.type = type;
        this.color = color;
        this.pieceValue = pieceValue;
        isHighlighted = false;
        canBeCaptured = false;
    }

    public abstract List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol);

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
}