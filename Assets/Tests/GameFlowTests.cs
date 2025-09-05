using NUnit.Framework;
using TetrisMania;
using UnityEngine;

namespace TetrisMania.Tests
{
    public class GameFlowTests
    {
        private class FakeAdManager : IAdManager
        {
            public bool RewardedShown;
            public bool InterstitialShown;
            public bool ShowRewardedAd() { RewardedShown = true; return true; }
            public void ShowInterstitialAd() { InterstitialShown = true; }
        }

        private class FakeIapManager : IIAPManager
        {
            public bool NoAds;
            public bool IsNoAdsPurchased() => NoAds;
            public void PurchaseNoAds() { NoAds = true; }
            public void PurchaseStarterPack() { }
        }

        private GameManager CreateGame(FakeAdManager? ad = null, FakeIapManager? iap = null)
        {
            var board = new GameObject().AddComponent<BoardGrid>();
            var spawner = new GameObject().AddComponent<PieceSpawner>();
            var score = new GameObject().AddComponent<ScoreManager>();
            var ui = new GameObject().AddComponent<UIController>();
            var gm = new GameObject().AddComponent<GameManager>();
            gm.Board = board;
            gm.Spawner = spawner;
            gm.Score = score;
            gm.UI = ui;
            gm.AdManager = ad;
            gm.IapManager = iap;
            gm.NewRun();
            return gm;
        }

        [Test]
        public void GameOver_WhenNoMovesRemain()
        {
            var gm = CreateGame(new FakeAdManager(), null);
            gm.Board.DebugFillNoMovesLeft();
            gm.CheckForGameOver();
            Assert.IsTrue(gm.IsGameOver);
        }

        [Test]
        public void ReviveWithRewarded_ResetsGameOver()
        {
            var ad = new FakeAdManager();
            var gm = CreateGame(ad, null);
            gm.Board.DebugFillNoMovesLeft();
            gm.CheckForGameOver();
            Assert.IsTrue(gm.IsGameOver);
            gm.TryReviveWithAd();
            Assert.IsFalse(gm.IsGameOver);
            Assert.IsTrue(ad.RewardedShown);
        }

        [Test]
        public void NoAds_DisablesInterstitialOnGameOver()
        {
            var ad = new FakeAdManager();
            var iap = new FakeIapManager { NoAds = true };
            SaveSystem.SessionsCount = 3; // ensure gate passes
            var gm = CreateGame(ad, iap);
            gm.Board.DebugFillNoMovesLeft();
            gm.CheckForGameOver();
            Assert.IsFalse(ad.InterstitialShown);
        }
    }
}
