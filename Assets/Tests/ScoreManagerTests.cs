using NUnit.Framework;
using TetrisMania;

namespace TetrisMania.Tests
{
    public class ScoreManagerTests
    {
        [Test]
        public void AwardsPointsForSingleLine()
        {
            var manager = new ScoreManager();
            manager.OnLinesCleared(1);
            Assert.AreEqual(100, manager.Score);
        }

        [Test]
        public void AwardsBonusForMultipleLines()
        {
            var manager = new ScoreManager();
            manager.OnLinesCleared(2);
            Assert.AreEqual(250, manager.Score);

            manager.Reset();
            manager.OnLinesCleared(3);
            Assert.AreEqual(400, manager.Score);

            manager.Reset();
            manager.OnLinesCleared(4);
            Assert.AreEqual(550, manager.Score);
        }

        [Test]
        public void IgnoresNonPositiveInput()
        {
            var manager = new ScoreManager();
            manager.OnLinesCleared(0);
            manager.OnLinesCleared(-1);
            Assert.AreEqual(0, manager.Score);
        }
    }
}
