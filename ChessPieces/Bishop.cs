using static ChessProject.ChessBoard;

namespace ChessProject;

public class Bishop : ChessPiece
{
    public Bishop(PieceColor color)
        : base(PieceType.Bishop, color, 3)
    {
    }

    public override List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol)
    {
        var validMoves = new List<(int, int)>();

        // Define all possible bishop move directions (diagonals).
        int[] rowDirections = { -1, -1, 1, 1 };
        int[] colDirections = { -1, 1, -1, 1 };

        // Loop through each possible diagonal direction.
        for (int dir = 0; dir < rowDirections.Length; dir++)
        {
            int rowDir = rowDirections[dir];
            int colDir = colDirections[dir];

            int newRow = currentRow + rowDir;
            int newCol = currentCol + colDir;

            // Keep moving along the diagonal until we go out of bounds or hit a piece.
            while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                ChessPiece currentIndex = board[newRow, newCol];

                // If the target square is empty or contains an opponent's piece, it's a valid move.
                if (board[newRow, newCol] == null || board[newRow, newCol] is MarkerPiece || board[newRow, newCol].color != color)
                {
                    validMoves.Add((newRow, newCol));
                }

                // If the target square is not empty, stop moving in this direction.
                if (board[newRow, newCol] != null)
                {
                    break;
                }

                // Move to the next square along the diagonal.
                newRow += rowDir;
                newCol += colDir;
            }
        }

        return validMoves;
    }


    public override char Piece()
    {
        return (color == PieceColor.White) ? '♗' : '♝';
    }
}