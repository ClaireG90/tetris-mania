using NUnit.Framework;
using System.Collections.Generic;
using TetrisMania;

namespace TetrisMania.Tests
{
    public class BoardGridTests
    {
        [Test]
        public void ClearsFullRowAndColumn()
        {
            var board = new BoardGrid();
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
        public void AwardsScoreForClears()
        {
            var board = new BoardGrid();

            var rowShape = new BlockShape(new bool[,] {
                { true, true, true, true, true, true, true, true }
            });
            board.TryPlacePiece(rowShape, 0, 0);
            Assert.AreEqual(100, board.Score);

            var columnShape = new BlockShape(new bool[,] {
                { true }, { true }, { true }, { true }, { true }, { true }, { true }, { true }
            });
            board.TryPlacePiece(columnShape, 0, 0);
            Assert.AreEqual(200, board.Score);
        }

        [Test]
        public void DetectsGameOverWhenNoValidPlacements()
        {
            var board = new BoardGrid();
            var spawner = new PieceSpawner();

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

