using NUnit.Framework;
using System.Collections.Generic;
using TetrisMania;
using UnityEngine;

namespace TetrisMania.Tests
{
    public class BoardGridTests
    {
        [Test]
        public void ClearsFullRowAndColumn()
        {
            var board = new GameObject().AddComponent<BoardGrid>();
            var clearedTotal = 0;
            board.LinesCleared += c => clearedTotal += c;

            var rowShape = new BlockShape(new bool[,] {
                { true, true, true, true, true, true, true, true }
            });

            Assert.IsTrue(board.TryPlacePiece(rowShape, 0, 0));
            Assert.IsFalse(board.IsCellOccupied(0, 0));
            Assert.AreEqual(1, clearedTotal);

            var columnShape = new BlockShape(new bool[,] {
                { true }, { true }, { true }, { true }, { true }, { true }, { true }, { true }
            });

            Assert.IsTrue(board.TryPlacePiece(columnShape, 0, 0));
            Assert.IsFalse(board.IsCellOccupied(0, 0));
            Assert.AreEqual(2, clearedTotal);
        }

        [Test]
        public void DetectsGameOverWhenNoValidPlacements()
        {
            var board = new GameObject().AddComponent<BoardGrid>();
            var spawner = new GameObject().AddComponent<PieceSpawner>();

            var almostFull = new bool[BoardGrid.Size, BoardGrid.Size];
            for (var y = 0; y < BoardGrid.Size; y++)
            {
                for (var x = 0; x < BoardGrid.Size; x++)
                {
                    almostFull[y, x] = x != y; // leave a diagonal of empty cells
                }
            }
            var shape = new BlockShape(almostFull);
            board.TryPlacePiece(shape, 0, 0);

            Assert.IsFalse(board.HasAnyValidPlacement(spawner.Shapes));
        }
    }
}

