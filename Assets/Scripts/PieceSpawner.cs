using System;
using System.Collections.Generic;

namespace TetrisMania
{
    /// <summary>
    /// Provides random block shapes for gameplay.
    /// </summary>
    public class PieceSpawner
    {
        private readonly Random _random = new Random();
        private readonly List<BlockShape> _shapes;

        /// <summary>
        /// Initializes a new instance of the <see cref="PieceSpawner"/> class.
        /// </summary>
        public PieceSpawner()
        {
            _shapes = new List<BlockShape>
            {
                // T shape
                new BlockShape(new bool[,]
                {
                    { true, true, true },
                    { false, true, false }
                }),
                // L shape
                new BlockShape(new bool[,]
                {
                    { true, false },
                    { true, false },
                    { true, true }
                }),
                // Square
                new BlockShape(new bool[,]
                {
                    { true, true },
                    { true, true }
                }),
                // Line
                new BlockShape(new bool[,]
                {
                    { true, true, true, true }
                })
            };
        }

        /// <summary>
        /// Gets the available shapes.
        /// </summary>
        public IReadOnlyList<BlockShape> Shapes => _shapes;

        /// <summary>
        /// Returns a random block shape.
        /// </summary>
        /// <returns>A randomly selected <see cref="BlockShape"/>.</returns>
        public BlockShape GetRandomShape()
        {
            var index = _random.Next(_shapes.Count);
            return _shapes[index];
        }
    }
}

