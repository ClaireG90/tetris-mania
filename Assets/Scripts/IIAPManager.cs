using System;

namespace TetrisMania
{
    /// <summary>
    /// Interface for in-app purchase functionality.
    /// </summary>
    public interface IIAPManager
    {
        /// <summary>
        /// Gets a value indicating whether the no-ads purchase was made.
        /// </summary>
        bool HasNoAds { get; }
    }
}
