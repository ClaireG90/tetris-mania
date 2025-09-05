using System;

namespace TetrisMania
{
    /// <summary>
    /// Placeholder in-app purchase manager.
    /// </summary>
    public class IAPManager : IIAPManager
    {
        /// <summary>
        /// Gets a value indicating whether the player owns the no-ads pack.
        /// </summary>
        public bool HasNoAds { get; private set; }

        /// <summary>
        /// Simulates purchasing the no-ads pack.
        /// </summary>
        public void PurchaseNoAds()
        {
            HasNoAds = true;
        }

        /// <summary>
        /// Simulates purchasing the starter pack.
        /// </summary>
        public void PurchaseStarterPack()
        {
        }
    }
}
