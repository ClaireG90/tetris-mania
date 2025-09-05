using UnityEngine;

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
        public AdManager(IIAPManager iapManager)
        {
            _iapManager = iapManager;
        }

        /// <inheritdoc />
        public bool ShowRewardedAd()
        {
            Debug.Log("rewarded ad");
            return true;
        }

        /// <inheritdoc />
        public void ShowInterstitialAd()
        {
            if (_iapManager.IsNoAdsPurchased())
            {
                return;
            }

            Debug.Log("interstitial ad");
        }
    }
}
