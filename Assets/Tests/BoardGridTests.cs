using NUnit.Framework;
using TetrisMania;
using UnityEngine;

namespace TetrisMania.Tests
{
    public class BoardGridTests
    {
        [Test]
        public void PlacesShape_WhenValid()
        {
            var board = new GameObject().AddComponent<BoardGrid>();
            var shape = new BlockShape(new bool[,] {{ true }});
            Assert.IsTrue(board.TryPlacePiece(shape, 0, 0));
            Assert.IsTrue(board.IsCellOccupied(0, 0));
        }

        [Test]
        public void DoesNotPlace_WhenInvalid()
        {
            var board = new GameObject().AddComponent<BoardGrid>();
            var shape = new BlockShape(new bool[,] { { true, true } });
            Assert.IsFalse(board.TryPlacePiece(shape, BoardGrid.Size - 1, 0));
            Assert.IsFalse(board.IsCellOccupied(BoardGrid.Size - 1, 0));
        }

        [Test]
        public void ClearsRowsAndColumns_Correctly()
        {
            var board = new GameObject().AddComponent<BoardGrid>();
            var cleared = 0;
            board.LinesCleared += c => cleared += c;

            var rowShape = new BlockShape(new bool[,] { { true, true, true, true, true, true, true, true } });
            Assert.IsTrue(board.TryPlacePiece(rowShape, 0, 0));
            Assert.AreEqual(1, cleared);
            Assert.IsFalse(board.IsCellOccupied(0, 0));

            var columnShape = new BlockShape(new bool[,] {
                { true }, { true }, { true }, { true }, { true }, { true }, { true }, { true }
            });
            Assert.IsTrue(board.TryPlacePiece(columnShape, 0, 0));
            Assert.AreEqual(2, cleared);
            Assert.IsFalse(board.IsCellOccupied(0, 0));
        }
    }
}
