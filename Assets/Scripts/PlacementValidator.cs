using System;
using System.Collections.Generic;

/// <summary>
/// Validates whether pieces can be placed on the board.
/// </summary>
public class PlacementValidator
{
    /// <summary>
    /// Checks if a piece can be placed at the given position.
    /// </summary>
    /// <param name="board">Board to evaluate.</param>
    /// <param name="piece">Piece shape.</param>
    /// <param name="x">Left position.</param>
    /// <param name="y">Top position.</param>
    public bool CanPlace(BoardGrid board, bool[,] piece, int x, int y)
    {
        if (board == null || piece == null)
        {
            return false;
        }

        int height = piece.GetLength(0);
        int width = piece.GetLength(1);

        if (x < 0 || y < 0 || x + width > BoardGrid.Size || y + height > BoardGrid.Size)
        {
            return false;
        }

        for (int r = 0; r < height; r++)
        {
            for (int c = 0; c < width; c++)
            {
                if (!piece[r, c])
                {
                    continue;
                }

                if (board.GetCell(x + c, y + r))
                {
                    return false;
                }
            }
        }

        return true;
    }

    /// <summary>
    /// Determines if any piece from the list can fit on the board.
    /// </summary>
    /// <param name="board">Board to evaluate.</param>
    /// <param name="pieces">Pieces to test.</param>
    public bool HasAnyValidPlacement(BoardGrid board, IEnumerable<bool[,]> pieces)
    {
        if (board == null || pieces == null)
        {
            return false;
        }

        foreach (var piece in pieces)
        {
            int height = piece.GetLength(0);
            int width = piece.GetLength(1);
            for (int y = 0; y <= BoardGrid.Size - height; y++)
            {
                for (int x = 0; x <= BoardGrid.Size - width; x++)
                {
                    if (CanPlace(board, piece, x, y))
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}

