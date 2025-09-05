using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Simple analytics helper that logs events.
    /// </summary>
    public static class AnalyticsStub
    {
        public static void RunStarted() => Debug.Log("analytics_run_started");
        public static void GameOver(int score, int lines) => Debug.Log($"analytics_game_over {score} {lines}");
        public static void ReviveShown() => Debug.Log("analytics_revive_shown");
        public static void ReviveSuccess() => Debug.Log("analytics_revive_success");
        public static void InterstitialShown() => Debug.Log("analytics_interstitial_shown");
        public static void PurchaseNoAds() => Debug.Log("analytics_purchase_noads");
        public static void PurchaseStarter() => Debug.Log("analytics_purchase_starter");
    }
}
