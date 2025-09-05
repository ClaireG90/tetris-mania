namespace TetrisMania
{
    /// <summary>
    /// Interface for in-app purchases.
    /// </summary>
    public interface IIAPManager
    {
        /// <summary>
        /// Returns whether the no-ads pack has been purchased.
        /// </summary>
        bool IsNoAdsPurchased();

        /// <summary>
        /// Performs the no-ads purchase.
        /// </summary>
        void PurchaseNoAds();

        /// <summary>
        /// Performs the starter pack purchase.
        /// </summary>
        void PurchaseStarterPack();
    }
}
