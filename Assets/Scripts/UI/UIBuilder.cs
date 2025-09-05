#if UNITY_5_3_OR_NEWER
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TetrisMania
{
    /// <summary>
    /// Builds runtime UI elements if none exist in the scene.
    /// </summary>
    public class UIBuilder : MonoBehaviour
    {
        [SerializeField]
        private UIController? _controller;

        private void Awake()
        {
            if (_controller == null)
            {
                _controller = FindObjectOfType<UIController>();
                if (_controller == null)
                {
                    _controller = new GameObject("UIController").AddComponent<UIController>();
                }
            }

            var canvas = FindObjectOfType<Canvas>();
            if (canvas == null)
            {
                var canvasGo = new GameObject("Canvas");
                canvas = canvasGo.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                canvasGo.AddComponent<CanvasScaler>();
                canvasGo.AddComponent<GraphicRaycaster>();
            }

            BuildScore(canvas.transform);
            BuildGameOver(canvas.transform);
            BuildRevive(canvas.transform);

            var gm = FindObjectOfType<GameManager>();
            if (gm != null)
            {
                _controller!.Initialize(gm);
            }
        }

        private void BuildScore(Transform parent)
        {
            if (_controller!.ScoreText != null)
            {
                return;
            }

            var go = new GameObject("ScoreText");
            go.transform.SetParent(parent);
            var text = go.AddComponent<TextMeshProUGUI>();
            var rect = text.rectTransform;
            rect.anchorMin = new Vector2(0.5f, 1f);
            rect.anchorMax = new Vector2(0.5f, 1f);
            rect.anchoredPosition = new Vector2(0, -20);
            _controller.ScoreText = text;
        }

        private void BuildGameOver(Transform parent)
        {
            if (_controller!.GameOverPanel != null)
            {
                return;
            }

            var panel = new GameObject("GameOverPanel");
            panel.SetActive(false);
            panel.transform.SetParent(parent);
            var rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;

            var label = new GameObject("GameOverLabel").AddComponent<TextMeshProUGUI>();
            label.text = "Game Over";
            label.transform.SetParent(panel.transform);

            var restart = CreateButton("RestartButton", panel.transform, "Restart");
            var revive = CreateButton("ReviveButton", panel.transform, "Revive");

            _controller.GameOverPanel = panel;
            _controller.RestartButton = restart;
            _controller.ReviveButton = revive;
        }

        private void BuildRevive(Transform parent)
        {
            if (_controller!.RevivePanel != null)
            {
                return;
            }

            var panel = new GameObject("RevivePanel");
            panel.SetActive(false);
            panel.transform.SetParent(parent);
            var rect = panel.AddComponent<RectTransform>();
            rect.anchorMin = new Vector2(0.5f, 0.5f);
            rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.anchoredPosition = Vector2.zero;

            var revive = CreateButton("ReviveButton", panel.transform, "Revive");

            _controller.RevivePanel = panel;
            _controller.RevivePanelButton = revive;
        }

        private Button CreateButton(string name, Transform parent, string label)
        {
            var go = new GameObject(name);
            go.transform.SetParent(parent);
            var btn = go.AddComponent<Button>();
            var txt = new GameObject("Text").AddComponent<TextMeshProUGUI>();
            txt.text = label;
            txt.transform.SetParent(go.transform);
            var rect = txt.rectTransform;
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;
            return btn;
        }
    }
}
#endif
