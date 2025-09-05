using System;
using System.Collections.Generic;
using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Generates and maintains the current set of offered block shapes.
    /// </summary>
    public class PieceSpawner : MonoBehaviour
    {
        private readonly System.Random _random = new System.Random();
        private readonly List<BlockShape> _shapeSet = new List<BlockShape>();
        private readonly List<BlockShape> _currentOffer = new List<BlockShape>();

        /// <summary>
        /// Gets the shapes currently offered to the player.
        /// </summary>
        public IReadOnlyList<BlockShape> CurrentOffer => _currentOffer;

        /// <summary>
        /// Gets a value indicating whether the current offer has no shapes left.
        /// </summary>
        public bool OfferEmpty => _currentOffer.Count == 0;

        private void Awake()
        {
            // Curated basic shapes
            _shapeSet.Add(new BlockShape(new bool[,] {{ true }})); // single block for tests
            _shapeSet.Add(new BlockShape(new bool[,] {
                { true, true },
                { true, true }
            }));
            _shapeSet.Add(new BlockShape(new bool[,] {
                { true, true, true },
                { false, true, false }
            }));
            _shapeSet.Add(new BlockShape(new bool[,] {
                { true, false },
                { true, false },
                { true, true }
            }));
            _shapeSet.Add(new BlockShape(new bool[,] {
                { true, true, true, true }
            }));
        }

        /// <summary>
        /// Generates a new offer of three shapes.
        /// </summary>
        public void GenerateOffer()
        {
            _currentOffer.Clear();
            for (var i = 0; i < 3; i++)
            {
                var index = _random.Next(_shapeSet.Count);
                _currentOffer.Add(_shapeSet[index]);
            }
        }

        /// <summary>
        /// Consumes the shape at the specified offer index.
        /// </summary>
        /// <param name="index">Index of the shape to remove.</param>
        public void ConsumeShape(int index)
        {
            if (index < 0 || index >= _currentOffer.Count)
            {
                return;
            }

            _currentOffer.RemoveAt(index);
            if (OfferEmpty)
            {
                GenerateOffer();
            }
        }

        /// <summary>
        /// Returns true if any shape from the current offer fits on the provided board grid.
        /// Convenience method used by tests.
        /// </summary>
        public bool AnyShapeFits(BoardGrid board)
        {
            return board.HasAnyValidPlacement(_currentOffer);
        }
    }
}
