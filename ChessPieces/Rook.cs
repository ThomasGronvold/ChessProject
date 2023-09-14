using System.ComponentModel.Design;
using static ChessProject.ChessBoard;

namespace ChessProject;

public class Rook : ChessPiece
{
    public Rook(PieceColor color)
        : base(PieceType.Rook, color, 5)
    {
    }

    public override List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol, bool removeIllegalMoves)
    {
        var validMoves = new List<(int, int)>();

        // Define the possible directions for a rook (vertical and horizontal).
        int[] rowDirections = { -1, 1 };
        int[] colDirections = { -1, 1 };

        // Loop through each possible direction.
        for (int i = 0; i < rowDirections.Length; i++)
        {
            int rowDir = rowDirections[i];
            int colDir = colDirections[i];

            int newRow = currentRow + rowDir;
            int newCol = currentCol + colDir;

            /* Checks the Row for valid moves */
            while (newRow >= 0 && newRow < 8)
            {
                if (board[newRow, currentCol] == null || board[newRow, currentCol] is MarkerPiece)
                {
                    validMoves.Add((newRow, currentCol));
                    newRow += rowDir;
                }
                else if (board[newRow, currentCol].color != color)
                {
                    validMoves.Add((newRow, currentCol));
                    break;
                }
                else
                {
                    break;
                }
            }

            /* Checks the Column for valid moves */
            while (newCol >= 0 && newCol < 8)
            {
                if (board[currentRow, newCol] == null || board[currentRow, newCol] is MarkerPiece)
                {
                    validMoves.Add((currentRow, newCol));
                    newCol += colDir;
                }
                else if (board[currentRow, newCol].color != color)
                {
                    validMoves.Add((currentRow, newCol));
                    break; 
                }
                else
                {
                    break;
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
        return (color == PieceColor.White) ? '♖' : '♜';
    }
}