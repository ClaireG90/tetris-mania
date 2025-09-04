using System;

namespace TetrisMania
{
    /// <summary>
    /// Validates whether a block shape can be placed on the board grid.
    /// </summary>
    public class PlacementValidator
    {
        /// <summary>
        /// Determines if the specified shape fits at the given coordinates.
        /// </summary>
        /// <param name="grid">Current grid state.</param>
        /// <param name="shape">Shape to test.</param>
        /// <param name="startX">X-coordinate of the top-left placement position.</param>
        /// <param name="startY">Y-coordinate of the top-left placement position.</param>
        /// <returns><c>true</c> if the shape fits without overlap or going out of bounds; otherwise, <c>false</c>.</returns>
        public bool CanPlace(bool[,] grid, BlockShape shape, int startX, int startY)
        {
            if (grid == null || shape == null)
            {
                return false;
            }

            var size = grid.GetLength(0);
            for (var y = 0; y < shape.Cells.GetLength(0); y++)
            {
                for (var x = 0; x < shape.Cells.GetLength(1); x++)
                {
                    if (!shape.Cells[y, x])
                    {
                        continue;
                    }

                    var gx = startX + x;
                    var gy = startY + y;

                    if (gx < 0 || gy < 0 || gx >= size || gy >= size)
                    {
                        return false;
                    }

                    if (grid[gy, gx])
                    {
                        return false;
                    }
                }
            }

            return true;
        }
    }
}

