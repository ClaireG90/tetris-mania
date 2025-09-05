using System;

namespace TetrisMania
{
    /// <summary>
    /// Represents a block shape composed of occupied cells.
    /// </summary>
    public class BlockShape
    {
        /// <summary>
        /// Gets the grid of cells composing this shape. <c>true</c> indicates an occupied cell.
        /// </summary>
        public bool[,] Cells { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="BlockShape"/> class.
        /// </summary>
        /// <param name="cells">Grid describing the shape.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="cells"/> is null.</exception>
        public BlockShape(bool[,] cells)
        {
            Cells = cells ?? throw new ArgumentNullException(nameof(cells));
        }
    }
}
