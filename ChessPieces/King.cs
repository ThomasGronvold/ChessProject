using static ChessProject.ChessBoard;

namespace ChessProject;

public class King : ChessPiece
{
    public King(PieceColor color)
        : base(PieceType.King, color, 0)
    {
    }

    public override List<(int, int)> GetValidMoves(ChessPiece[,] board, int currentRow, int currentCol, bool removeIllegalMoves)
    {
        var validMoves = new List<(int, int)>();

        // Define all possible move directions: horizontal, vertical, and diagonal.
        int[] rowDirections = { -1, -1, -1, 0, 0, 1, 1, 1 };
        int[] colDirections = { -1, 0, 1, -1, 1, -1, 0, 1 };

        for (int dir = 0; dir < rowDirections.Length; dir++)
        {
            int newRow = currentRow + rowDirections[dir];
            int newCol = currentCol + colDirections[dir];

            if (newRow >= 0 && newRow < 8 && newCol >= 0 && newCol < 8)
            {
                ChessPiece targetPiece = board[newRow, newCol];

                if (targetPiece == null || targetPiece is MarkerPiece)
                {

                    validMoves.Add((newRow, newCol));
                }
                else if (targetPiece.color != color)
                {
                    validMoves.Add((newRow, newCol));
                }
            }
        }

        if (removeIllegalMoves)
        {
            validMoves = RemoveIllegalMoves(board, validMoves, currentRow, currentCol);
        }

        /* Castling moves */
        if (!hasMoved && color == PieceColor.White) /* If the king has not moved */
        {
            /* Castling white side */
            if (board[0, 7] != null &&
                board[0, 7].type is PieceType.Rook &&
                !board[0, 7].hasMoved && /* if the king-side rook has not moved */
                board[0, 5] == null ||
                board[0, 5] is MarkerPiece && /* If the space between the king and rook is empty */
                board[0, 6] == null ||
                board[0, 6] is MarkerPiece &&
                kingCanCastle(board, "whiteKingSide")) /* King-side castle legal moves check */
            {
                validMoves.Add((0, 6));
            }

            if (board[0, 0] != null &&
                board[0, 0].type is PieceType.Rook &&
                !board[0, 0].hasMoved && /* if the queen-side rook has not moved */
                board[0, 2] == null ||
                board[0, 2] is MarkerPiece &&
                board[0, 3] == null ||
                board[0, 3] is MarkerPiece && /* If the space between the king and rook is empty */
                kingCanCastle(board, "whiteQueenSide")) /* Queen-side castle legal moves check */
            {
                validMoves.Add((0, 2));
            }
        }
        else
        {
            if (board[7, 7] != null &&
                board[7, 7].type is PieceType.Rook &&
                !board[7, 7].hasMoved && /* if the king-side rook has not moved */
                board[7, 5] == null ||
                board[7, 5] is MarkerPiece &&
                board[7, 6] == null ||
                board[7, 6] is MarkerPiece && /* If the space between the king and rook is empty */
                kingCanCastle(board, "blackKingSide")) /* Queen-side castle legal moves check */
            {
                validMoves.Add((7, 6));
            }
            if (board[7, 0] != null &&
                board[7, 0].type is PieceType.Rook &&
                !board[7,0].hasMoved && /* if the king-side rook has not moved */
                board[7, 2] == null ||
                board[7, 2] is MarkerPiece &&
                board[7, 3] == null ||
                board[7, 3] is MarkerPiece && /* If the space between the king and rook is empty */
                kingCanCastle(board, "blackQueenCastle")) /* Queen-side castle legal moves check */
            {
                validMoves.Add((7, 6));
            }
        }
        
        return validMoves;
    }

    public override char Piece()
    {
        return (color == PieceColor.White) ? '♔' : '♚';
    }

    private bool kingCanCastle(ChessPiece[,] board, string castleSide)
    {
        List<(int, int)> castleMoves;
        var canCastle = true;

        var kingPos = color == PieceColor.White ? (0, 4) : (7, 4);

        if (castleSide == "whiteKingSide")
        {
            castleMoves = new List<(int, int)> { (0, 4), (0, 5), (0, 6) };
        }
        else if (castleSide == "whiteQueenSide")
        {
            castleMoves = new List<(int, int)> { (0, 4), (0, 3), (0, 2) };
        }
        else if (castleSide == "blackKingSide")
        {
            castleMoves = new List<(int, int)> { (0, 4), (0, 5), (0, 6) };
        }
        else
        {
            castleMoves = new List<(int, int)> { (0, 4), (0, 3), (0, 2) };
        }


        foreach ((int newRow, int newCol) in castleMoves)
        {
            var tempBoard = (ChessPiece[,])board.Clone();

            /* Copies the king from the board to the new position to check on the temporary board to check if that move will cause a check */
            tempBoard[newRow, newCol] = new King(PieceColor.White)/*board[kingPos.Item1, kingPos.Item2]*/;
            tempBoard[kingPos.Item1, kingPos.Item2] = null;

            /* Checks if the current validMoves move is legal */
            if (!IsInCheck(tempBoard))
            {
                canCastle = false;
            }
        }
        return canCastle;
    }
}