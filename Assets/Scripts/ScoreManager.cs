using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Handles player scoring based on cleared lines.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        private int _score;

        /// <summary>
        /// Gets the current score.
        /// </summary>
        public int Score => _score;

        /// <summary>
        /// Resets the score to zero.
        /// </summary>
        public void ResetScore()
        {
            _score = 0;
        }

        /// <summary>
        /// Adds points for the specified number of cleared lines.
        /// </summary>
        /// <param name="count">Number of lines cleared in a single move.</param>
        public void OnLinesCleared(int count)
        {
            if (count <= 0)
            {
                return;
            }

            _score += count * 100;
            if (count > 1)
            {
                _score += 50 * (count - 1);
            }
        }
    }
}
