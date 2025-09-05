using TMPro;
using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Basic UI control for score and panels.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        public TextMeshProUGUI scoreText = null!;
        public GameObject gameOverPanel = null!;
        public GameObject revivePanel = null!;

        /// <summary>
        /// Sets the score label.
        /// </summary>
        public void SetScore(int value)
        {
            if (scoreText != null)
            {
                scoreText.text = value.ToString();
            }
        }

        /// <summary>
        /// Shows or hides the game over panel.
        /// </summary>
        public void ShowGameOver(bool show)
        {
            if (gameOverPanel != null)
            {
                gameOverPanel.SetActive(show);
            }
        }

        /// <summary>
        /// Shows or hides the revive panel.
        /// </summary>
        public void ShowRevive(bool show)
        {
            if (revivePanel != null)
            {
                revivePanel.SetActive(show);
            }
        }
    }
}
