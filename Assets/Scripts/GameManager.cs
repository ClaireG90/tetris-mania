using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Coordinates core game flow including new runs, game over and revives.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public BoardGrid Board = null!;
        public PieceSpawner Spawner = null!;
        public ScoreManager Score = null!;
        public UIController UI = null!;
        public IAdManager? AdManager;
        public IIAPManager? IapManager;

        /// <summary>
        /// Gets a value indicating whether the game is over.
        /// </summary>
        public bool IsGameOver { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the revive has already been used this run.
        /// </summary>
        public bool ReviveUsed { get; private set; }

        private void HandleLinesCleared(int count)
        {
            Score.OnLinesCleared(count);
            UI.SetScore(Score.Score);
        }

        /// <summary>
        /// Starts a new run by clearing state and generating the first offer.
        /// </summary>
        public void NewRun()
        {
            Board.LinesCleared -= HandleLinesCleared;
            Board.LinesCleared += HandleLinesCleared;

            Board.ClearAll();
            Score.ResetScore();
            Spawner.GenerateOffer();
            IsGameOver = false;
            ReviveUsed = false;
            UI.SetScore(0);
            UI.ShowGameOver(false);
            UI.ShowRevive(false);
            UI.Initialize(this);

            SaveSystem.SessionsCount = SaveSystem.SessionsCount + 1;
            AnalyticsStub.RunStarted();
        }

        /// <summary>
        /// Checks if no valid moves remain and triggers game over.
        /// </summary>
        public void CheckForGameOver()
        {
            if (IsGameOver)
            {
                return;
            }

            if (!Board.HasAnyValidPlacement(Spawner.CurrentOffer))
            {
                IsGameOver = true;
                UI.ShowGameOver(true);
                AnalyticsStub.GameOver(Score.Score, 0);
                MaybeShowInterstitialOnGameOver();
            }
        }

        /// <summary>
        /// Attempts to revive the current run via a rewarded ad.
        /// </summary>
        public void TryReviveWithAd()
        {
            if (!IsGameOver || ReviveUsed || AdManager == null)
            {
                return;
            }

            AnalyticsStub.ReviveShown();
            if (AdManager.ShowRewardedAd())
            {
                ReviveUsed = true;
                IsGameOver = false;
                Spawner.GenerateOffer();
                UI.ShowGameOver(false);
                UI.ShowRevive(false);
                AnalyticsStub.ReviveSuccess();
            }
        }

        /// <summary>
        /// Shows an interstitial ad on game over unless disabled by a purchase or session gate.
        /// </summary>
        public void MaybeShowInterstitialOnGameOver()
        {
            if (SaveSystem.SessionsCount < 3)
            {
                return;
            }

            if (IapManager != null && IapManager.IsNoAdsPurchased())
            {
                return;
            }

            AdManager?.ShowInterstitialAd();
            AnalyticsStub.InterstitialShown();
        }
    }
}
