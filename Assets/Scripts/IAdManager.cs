namespace TetrisMania
{
    /// <summary>
    /// Interface for ad management.
    /// </summary>
    public interface IAdManager
    {
        /// <summary>
        /// Displays a rewarded ad and returns true if completed successfully.
        /// </summary>
        bool ShowRewardedAd();

        /// <summary>
        /// Displays an interstitial ad.
        /// </summary>
        void ShowInterstitialAd();
    }
}
