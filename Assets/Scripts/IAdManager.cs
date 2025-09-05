using System;

namespace TetrisMania
{
    /// <summary>
    /// Interface for ad management.
    /// </summary>
    public interface IAdManager
    {
        /// <summary>
        /// Displays a rewarded ad.
        /// </summary>
        /// <returns>True if the ad was shown successfully.</returns>
        bool ShowRewardedAd();

        /// <summary>
        /// Displays an interstitial ad if allowed.
        /// </summary>
        /// <returns>True when an interstitial ad is shown.</returns>
        bool ShowInterstitialAd();
    }
}
