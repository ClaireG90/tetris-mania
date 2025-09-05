namespace TetrisMania
{
    /// <summary>
    /// Placeholder in-app purchase manager.
    /// </summary>
    public class IAPManager : IIAPManager
    {
        private bool _noAds;

        /// <inheritdoc />
        public bool IsNoAdsPurchased() => _noAds;

        /// <inheritdoc />
        public void PurchaseNoAds()
        {
            _noAds = true;
        }

        /// <inheritdoc />
        public void PurchaseStarterPack()
        {
        }
    }
}
