using System;

namespace TetrisMania
{
    /// <summary>
    /// Placeholder ad manager for rewarded and interstitial ads.
    /// </summary>
    public class AdManager : IAdManager
    {
        private readonly IIAPManager _iapManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdManager"/> class.
        /// </summary>
        /// <param name="iapManager">IAP system used to check for no-ads purchases.</param>
        /// <exception cref="ArgumentNullException">Thrown when <paramref name="iapManager"/> is null.</exception>
        public AdManager(IIAPManager iapManager)
        {
            _iapManager = iapManager ?? throw new ArgumentNullException(nameof(iapManager));
        }

        /// <summary>
        /// Simulates displaying a rewarded ad.
        /// </summary>
        /// <returns><c>true</c> to indicate the ad was "watched" successfully.</returns>
        public bool ShowRewardedAd()
        {
            return true;
        }

        /// <summary>
        /// Simulates displaying an interstitial ad.
        /// </summary>
        /// <returns><c>true</c> if the ad is shown; otherwise, <c>false</c>.</returns>
        public bool ShowInterstitialAd()
        {
            if (_iapManager.HasNoAds)
            {
                return false;
            }

            return true;
        }
    }
}
