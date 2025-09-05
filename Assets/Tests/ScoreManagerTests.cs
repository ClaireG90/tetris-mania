using NUnit.Framework;
using TetrisMania;
using UnityEngine;

namespace TetrisMania.Tests
{
    public class ScoreManagerTests
    {
        [Test]
        public void AwardsBasePoints_ForSingleLine()
        {
            var manager = new GameObject().AddComponent<ScoreManager>();
            manager.OnLinesCleared(1);
            Assert.AreEqual(100, manager.Score);
        }

        [Test]
        public void AwardsComboBonus_ForMultipleLines()
        {
            var manager = new GameObject().AddComponent<ScoreManager>();
            manager.OnLinesCleared(2);
            Assert.AreEqual(250, manager.Score);

            manager.ResetScore();
            manager.OnLinesCleared(3);
            Assert.AreEqual(400, manager.Score);
        }
    }
}
