using static ChessProject.ChessBoard;

namespace ChessProject;

public class Queen : ChessPiece
{
    public Queen(PieceColor color)
        : base(PieceType.Queen, color, 9)
    {
    }

    public override List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol, bool removeIllegalMoves)
    {
        var validMoves = new List<(int, int)>();

        // Define all possible queen moves, row and col are connected on index.
        int[] rowDirections = { -1, -1, -1, 0, 1, 1, 1, 0 };
        int[] colDirections = { -1, 0, 1, 1, 1, 0, -1, -1 };

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

        if (removeIllegalMoves)
        {
            validMoves = RemoveIllegalMoves(board, validMoves, currentRow, currentCol);
        }

        return validMoves;
    }


    public override char Piece()
    {
        return (color == PieceColor.White) ? '♕' : '♛';
    }
}