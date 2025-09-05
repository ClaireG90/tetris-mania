using NUnit.Framework;
using TetrisMania;

namespace TetrisMania.Tests
{
    public class GameFlowTests
    {
        private class FakeAdManager : IAdManager
        {
            public bool RewardedShown { get; private set; }
            public bool InterstitialShown { get; private set; }

            public bool ShowRewardedAd()
            {
                RewardedShown = true;
                return true;
            }

            public bool ShowInterstitialAd()
            {
                InterstitialShown = true;
                return true;
            }
        }

        private class FakeIAPManager : IIAPManager
        {
            public bool HasNoAds { get; set; }
        }

        [Test]
        public void GameOver_Fires_WhenNoMovesRemain()
        {
            var adManager = new FakeAdManager();
            var gameManager = new GameManager(adManager);

            var almostFull = new bool[BoardGrid.Size, BoardGrid.Size];
            for (var y = 0; y < BoardGrid.Size; y++)
            {
                for (var x = 0; x < BoardGrid.Size; x++)
                {
                    almostFull[y, x] = x != y;
                }
            }

            var shape = new BlockShape(almostFull);
            Assert.IsTrue(gameManager.Board.TryPlacePiece(shape, 0, 0));

            gameManager.CheckGameOver();

            Assert.IsTrue(gameManager.IsGameOver);
        }

        [Test]
        public void Revive_WithRewarded_ResumesPlay()
        {
            var adManager = new FakeAdManager();
            var gameManager = new GameManager(adManager);

            var almostFull = new bool[BoardGrid.Size, BoardGrid.Size];
            for (var y = 0; y < BoardGrid.Size; y++)
            {
                for (var x = 0; x < BoardGrid.Size; x++)
                {
                    almostFull[y, x] = x != y;
                }
            }

            var shape = new BlockShape(almostFull);
            Assert.IsTrue(gameManager.Board.TryPlacePiece(shape, 0, 0));
            gameManager.CheckGameOver();
            Assert.IsTrue(gameManager.IsGameOver);

            Assert.IsTrue(gameManager.ReviveWithAd());
            Assert.IsFalse(gameManager.IsGameOver);
            Assert.IsTrue(adManager.RewardedShown);
        }

        [Test]
        public void NoAdsPurchase_DisablesInterstitials()
        {
            var iapManager = new FakeIAPManager { HasNoAds = true };
            var adManager = new AdManager(iapManager);
            var gameManager = new GameManager(adManager);

            var almostFull = new bool[BoardGrid.Size, BoardGrid.Size];
            for (var y = 0; y < BoardGrid.Size; y++)
            {
                for (var x = 0; x < BoardGrid.Size; x++)
                {
                    almostFull[y, x] = x != y;
                }
            }

            var shape = new BlockShape(almostFull);
            Assert.IsTrue(gameManager.Board.TryPlacePiece(shape, 0, 0));
            gameManager.CheckGameOver();
            Assert.IsTrue(gameManager.IsGameOver);

            Assert.IsFalse(adManager.ShowInterstitialAd());
        }
    }
}
