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

        // Define all possible bishop move directions, row and col arrays are "connected" on index (diagonals).
        int[] rowDirections = { -1, -1, 1, 1 };
        int[] colDirections = { -1, 1, -1, 1 };

        for (int dir = 0; dir < rowDirections.Length; dir++)
        {
            int rowDir = rowDirections[dir];
            int colDir = colDirections[dir];

            int newRow = currentRow + rowDir;
            int newCol = currentCol + colDir;

            while (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                ChessPiece targetPiece = board[newRow, newCol];

                if (targetPiece == null || targetPiece is MarkerPiece)
                {
                    validMoves.Add((newRow, newCol));
                }
                else
                {
                    if (targetPiece.color != color)
                    {
                        validMoves.Add((newRow, newCol));
                    }
                    break;
                }

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