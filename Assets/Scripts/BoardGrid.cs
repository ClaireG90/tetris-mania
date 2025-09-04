using System;
using System.Collections.Generic;

namespace TetrisMania
{
    /// <summary>
    /// Represents a block shape composed of a grid of cells.
    /// </summary>
    public class BlockShape
    {
        /// <summary>
        /// Gets the shape cells where <c>true</c> indicates an occupied block.
        /// </summary>
        public bool[,] Cells { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockShape"/> class.
        /// </summary>
        /// <param name="cells">Two dimensional array describing the shape.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="cells"/> is null.</exception>
        public BlockShape(bool[,] cells)
        {
            Cells = cells ?? throw new ArgumentNullException(nameof(cells));
        }
    }

    /// <summary>
    /// Manages the main 8x8 board grid, handling block placement and line clears.
    /// </summary>
    public class BoardGrid
    {
        /// <summary>
        /// Size of the board on each axis.
        /// </summary>
        public const int Size = 8;

        private readonly bool[,] _grid = new bool[Size, Size];
        private readonly PlacementValidator _validator = new PlacementValidator();

        /// <summary>
        /// Raised whenever one or more lines are cleared.
        /// </summary>
        public event Action<int>? LinesCleared;

        /// <summary>
        /// Gets the player's score.
        /// </summary>
        public int Score { get; private set; }

        /// <summary>
        /// Attempts to place a shape at the specified board coordinates.
        /// </summary>
        /// <param name="shape">Shape to place.</param>
        /// <param name="x">X-coordinate of the top-left position.</param>
        /// <param name="y">Y-coordinate of the top-left position.</param>
        /// <returns><c>true</c> if placement succeeded; otherwise, <c>false</c>.</returns>
        public bool TryPlacePiece(BlockShape shape, int x, int y)
        {
            if (shape == null)
            {
                return false;
            }

            if (!_validator.CanPlace(_grid, shape, x, y))
            {
                return false;
            }

            PlaceShape(shape, x, y);
            var cleared = ClearLines();
            if (cleared > 0)
            {
                Score += cleared * 100;
                LinesCleared?.Invoke(cleared);
            }

            return true;
        }

        /// <summary>
        /// Determines whether any of the provided shapes can be placed on the board.
        /// </summary>
        /// <param name="shapes">Collection of shapes to test.</param>
        /// <returns><c>true</c> if any shape fits; otherwise, <c>false</c>.</returns>
        public bool HasAnyValidPlacement(IEnumerable<BlockShape> shapes)
        {
            if (shapes == null)
            {
                return false;
            }

            foreach (var shape in shapes)
            {
                if (shape == null)
                {
                    continue;
                }

                for (var y = 0; y < Size; y++)
                {
                    for (var x = 0; x < Size; x++)
                    {
                        if (_validator.CanPlace(_grid, shape, x, y))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Returns whether a specific cell is occupied.
        /// </summary>
        /// <param name="x">X-coordinate.</param>
        /// <param name="y">Y-coordinate.</param>
        /// <returns><c>true</c> if the cell contains a block; otherwise, <c>false</c>.</returns>
        public bool IsCellOccupied(int x, int y)
        {
            return _grid[y, x];
        }

        private void PlaceShape(BlockShape shape, int startX, int startY)
        {
            for (var y = 0; y < shape.Cells.GetLength(0); y++)
            {
                for (var x = 0; x < shape.Cells.GetLength(1); x++)
                {
                    if (shape.Cells[y, x])
                    {
                        _grid[startY + y, startX + x] = true;
                    }
                }
            }
        }

        private int ClearLines()
        {
            var cleared = 0;

            // Clear full rows
            for (var y = 0; y < Size; y++)
            {
                var full = true;
                for (var x = 0; x < Size; x++)
                {
                    if (!_grid[y, x])
                    {
                        full = false;
                        break;
                    }
                }

                if (full)
                {
                    cleared++;
                    for (var x = 0; x < Size; x++)
                    {
                        _grid[y, x] = false;
                    }
                }
            }

            // Clear full columns
            for (var x = 0; x < Size; x++)
            {
                var full = true;
                for (var y = 0; y < Size; y++)
                {
                    if (!_grid[y, x])
                    {
                        full = false;
                        break;
                    }
                }

                if (full)
                {
                    cleared++;
                    for (var y = 0; y < Size; y++)
                    {
                        _grid[y, x] = false;
                    }
                }
            }

            return cleared;
        }
    }
}

