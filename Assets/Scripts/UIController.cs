using TMPro;
using UnityEngine;
#if UNITY_5_3_OR_NEWER
using UnityEngine.UI;
#endif

namespace TetrisMania
{
    /// <summary>
    /// Basic UI control for score and panels.
    /// </summary>
    public class UIController : MonoBehaviour
    {
        public TextMeshProUGUI ScoreText = null!;
        public GameObject GameOverPanel = null!;
        public GameObject RevivePanel = null!;
#if UNITY_5_3_OR_NEWER
        public Button RestartButton = null!;
        public Button ReviveButton = null!;
        public Button RevivePanelButton = null!;
#endif

        /// <summary>
        /// Sets the score label.
        /// </summary>
        public void SetScore(int value)
        {
            if (ScoreText != null)
            {
                ScoreText.text = value.ToString();
            }
        }

        /// <summary>
        /// Shows or hides the game over panel.
        /// </summary>
        public void ShowGameOver(bool show)
        {
            if (GameOverPanel != null)
            {
                GameOverPanel.SetActive(show);
            }
        }

        /// <summary>
        /// Shows or hides the revive panel.
        /// </summary>
        public void ShowRevive(bool show)
        {
            if (RevivePanel != null)
            {
                RevivePanel.SetActive(show);
            }
        }

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Links button callbacks to the provided game manager.
        /// </summary>
        public void Initialize(GameManager manager)
        {
            if (RestartButton != null)
            {
                RestartButton.onClick.RemoveAllListeners();
                RestartButton.onClick.AddListener(manager.NewRun);
            }

            if (ReviveButton != null)
            {
                ReviveButton.onClick.RemoveAllListeners();
                ReviveButton.onClick.AddListener(manager.TryReviveWithAd);
            }

            if (RevivePanelButton != null)
            {
                RevivePanelButton.onClick.RemoveAllListeners();
                RevivePanelButton.onClick.AddListener(manager.TryReviveWithAd);
            }
        }
#endif
    }
}
