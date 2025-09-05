using System;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Provides random block shapes for gameplay.
    /// </summary>
    public class PieceSpawner : MonoBehaviour
    {
        private readonly Random _random = new Random();
        private readonly List<BlockShape> _shapes = new List<BlockShape>();

        private void Awake()
        {
            _shapes.AddRange(new List<BlockShape>
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
            });
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

