using System;
using System.Collections.Generic;

/// <summary>
/// Manages an 8x8 grid of blocks, handles placement and clearing of lines.
/// </summary>
public class BoardGrid
{
    public const int Size = 8;
    private readonly bool[,] _cells = new bool[Size, Size];

    /// <summary>
    /// Invoked when one or more lines are cleared.
    /// </summary>
    public event Action<int> LinesCleared;

    /// <summary>
    /// Total score accumulated.
    /// </summary>
    public int Score { get; private set; }

    /// <summary>
    /// Returns whether the specified cell is occupied.
    /// </summary>
    /// <param name="x">X coordinate.</param>
    /// <param name="y">Y coordinate.</param>
    public bool GetCell(int x, int y)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size)
        {
            throw new ArgumentOutOfRangeException();
        }

        return _cells[x, y];
    }

    /// <summary>
    /// Sets a cell to the provided value without triggering line clears.
    /// Intended for setup or tests.
    /// </summary>
    public void SetCell(int x, int y, bool occupied)
    {
        if (x < 0 || x >= Size || y < 0 || y >= Size)
        {
            throw new ArgumentOutOfRangeException();
        }

        _cells[x, y] = occupied;
    }

    /// <summary>
    /// Places a piece on the grid and clears any full lines.
    /// </summary>
    /// <param name="piece">Piece shape represented as a 2D boolean array.</param>
    /// <param name="x">Left position.</param>
    /// <param name="y">Top position.</param>
    public void PlacePiece(bool[,] piece, int x, int y)
    {
        if (piece == null)
        {
            throw new ArgumentNullException(nameof(piece));
        }

        int height = piece.GetLength(0);
        int width = piece.GetLength(1);

        for (int r = 0; r < height; r++)
        {
            for (int c = 0; c < width; c++)
            {
                if (!piece[r, c])
                {
                    continue;
                }

                _cells[x + c, y + r] = true;
            }
        }

        int cleared = ClearLines();
        if (cleared > 0)
        {
            Score += cleared * 10;
            LinesCleared?.Invoke(cleared);
        }
    }

    private int ClearLines()
    {
        List<int> rowsToClear = new List<int>();
        List<int> colsToClear = new List<int>();

        for (int r = 0; r < Size; r++)
        {
            bool full = true;
            for (int c = 0; c < Size; c++)
            {
                if (!_cells[c, r])
                {
                    full = false;
                    break;
                }
            }

            if (full)
            {
                rowsToClear.Add(r);
            }
        }

        for (int c = 0; c < Size; c++)
        {
            bool full = true;
            for (int r = 0; r < Size; r++)
            {
                if (!_cells[c, r])
                {
                    full = false;
                    break;
                }
            }

            if (full)
            {
                colsToClear.Add(c);
            }
        }

        foreach (int r in rowsToClear)
        {
            for (int c = 0; c < Size; c++)
            {
                _cells[c, r] = false;
            }
        }

        foreach (int c in colsToClear)
        {
            for (int r = 0; r < Size; r++)
            {
                _cells[c, r] = false;
            }
        }

        return rowsToClear.Count + colsToClear.Count;
    }
}

