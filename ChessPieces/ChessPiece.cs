namespace ChessProject;

public abstract class ChessPiece
{
    /* BaseClass for every chess piece. */
    public ChessBoard.PieceType type { get; private set; }
    public ChessBoard.PieceColor color { get; private set; }
    protected int pieceValue;
    public bool isHighlighted { get; private set; }
    public bool canBeCaptured { get; private set; }

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
}