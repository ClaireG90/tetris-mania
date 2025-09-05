using System;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Manages the main 8x8 board grid, handling block placement and line clears.
    /// </summary>
    public class BoardGrid : MonoBehaviour
    {
        /// <summary>
        /// Size of the board on each axis.
        /// </summary>
        public const int Size = 8;

        private bool[,] _grid = null!;
        private readonly PlacementValidator _validator = new PlacementValidator();

        /// <summary>
        /// Raised whenever one or more lines are cleared.
        /// </summary>
        public event Action<int>? LinesCleared;

        private void Awake()
        {
            _grid = new bool[Size, Size];
        }

        /// <summary>
        /// Returns whether a specific cell is occupied.
        /// </summary>
        public bool IsCellOccupied(int x, int y) => _grid[y, x];

        /// <summary>
        /// Gets a copy of the current board state.
        /// </summary>
        public bool[,] GetSnapshot()
        {
            var copy = new bool[Size, Size];
            Array.Copy(_grid, copy, _grid.Length);
            return copy;
        }

        /// <summary>
        /// Restores the board state from a snapshot.
        /// </summary>
        public void SetSnapshot(bool[,] snapshot)
        {
            if (snapshot == null || snapshot.GetLength(0) != Size || snapshot.GetLength(1) != Size)
            {
                return;
            }

            Array.Copy(snapshot, _grid, _grid.Length);
        }

        /// <summary>
        /// Attempts to place a shape at the specified coordinates.
        /// </summary>
        public bool TryPlacePiece(BlockShape shape, int x, int y)
        {
            if (shape == null || !_validator.CanPlace(_grid, shape, x, y))
            {
                return false;
            }

            PlaceShape(shape, x, y);
            var cleared = ClearLines();
            if (cleared > 0)
            {
                LinesCleared?.Invoke(cleared);
            }

            return true;
        }

        /// <summary>
        /// Determines whether any of the provided shapes can be placed on the board.
        /// </summary>
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
        /// Fills the entire grid to leave no valid placements. Used for tests.
        /// </summary>
        public void DebugFillNoMovesLeft()
        {
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    _grid[y, x] = true;
                }
            }
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

            // rows
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

            // columns
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.gray;
            for (var y = 0; y < Size; y++)
            {
                for (var x = 0; x < Size; x++)
                {
                    Gizmos.DrawWireCube(new Vector3(x, -y, 0), Vector3.one);
                }
            }
        }
    }
}
