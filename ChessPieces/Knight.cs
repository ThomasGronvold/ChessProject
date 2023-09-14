using static ChessProject.ChessBoard;

namespace ChessProject;

public class Knight : ChessPiece
{
    public Knight(PieceColor color)
        : base(PieceType.Knight, color, 3)
    {
    }

    public override List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol, bool removeIllegalMoves)
    {
        var validMoves = new List<(int, int)>();

        // Define all possible knight moves.
        int[] rowDirections = { -2, -1, 1, 2, 2, 1, -1, -2 };
        int[] colDirections = { 1, 2, 2, 1, -1, -2, -2, -1 };

        // Loop through all possible knight moves.
        for (int i = 0; i < rowDirections.Length; i++)
        {
            int newRow = currentRow + rowDirections[i];
            int newCol = currentCol + colDirections[i];

            // Check if the new position is within the chessboard bounds (0-7).
            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                ChessPiece targetPiece = board[newRow, newCol];

                // Check if the target square is empty, contains a marker piece, or an opponent's piece.
                if (targetPiece == null || targetPiece is MarkerPiece || targetPiece.color != color)
                {
                    validMoves.Add((newRow, newCol));
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
        return (color == PieceColor.White) ? '♘' : '♞';
    }
}