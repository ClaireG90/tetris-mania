using System;
using UnityEngine;

namespace TetrisMania
{
    /// <summary>
    /// Coordinates core game flow including start, game over and restart.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        private BoardGrid _board = null!;
        private PieceSpawner _spawner = null!;
        private ScoreManager _scoreManager = null!;
        private IAdManager? _adManager;
        private bool _gameOver;
        private bool _reviveUsed;

        /// <summary>
        /// Gets the current board grid.
        /// </summary>
        public BoardGrid Board => _board;

        /// <summary>
        /// Gets the piece spawner.
        /// </summary>
        public PieceSpawner Spawner => _spawner;

        /// <summary>
        /// Gets the score manager.
        /// </summary>
        public ScoreManager ScoreManager => _scoreManager;

        /// <summary>
        /// Gets a value indicating whether the game is over.
        /// </summary>
        public bool IsGameOver => _gameOver;

        /// <summary>
        /// Gets a value indicating whether the game over panel is visible.
        /// </summary>
        public bool GameOverPanelVisible { get; private set; }

        /// <summary>
        /// Initializes this instance with required dependencies.
        /// </summary>
        /// <param name="board">Board grid component.</param>
        /// <param name="spawner">Piece spawner component.</param>
        /// <param name="scoreManager">Score manager component.</param>
        /// <param name="adManager">Ad manager instance.</param>
        public void Initialize(BoardGrid board, PieceSpawner spawner, ScoreManager scoreManager, IAdManager adManager)
        {
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _spawner = spawner ?? throw new ArgumentNullException(nameof(spawner));
            _scoreManager = scoreManager ?? throw new ArgumentNullException(nameof(scoreManager));
            _adManager = adManager ?? throw new ArgumentNullException(nameof(adManager));
            _board.LinesCleared += _scoreManager.OnLinesCleared;
        }

        /// <summary>
        /// Starts a new game.
        /// </summary>
        public void StartGame()
        {
            if (_board == null || _scoreManager == null)
            {
                return;
            }

            _board.ResetGrid();
            _scoreManager.Reset();
            _gameOver = false;
            _reviveUsed = false;
            GameOverPanelVisible = false;
        }

        /// <summary>
        /// Checks for game over and displays the panel when no moves remain.
        /// </summary>
        public void CheckGameOver()
        {
            if (_board == null || _spawner == null)
            {
                return;
            }

            if (!_board.HasAnyValidPlacement(_spawner.Shapes))
            {
                _gameOver = true;
                GameOverPanelVisible = true;
            }
        }

        /// <summary>
        /// Restarts the game from scratch.
        /// </summary>
        public void Restart()
        {
            StartGame();
        }

        /// <summary>
        /// Attempts to revive the player via a rewarded ad.
        /// </summary>
        /// <returns><c>true</c> if the game resumed; otherwise, <c>false</c>.</returns>
        public bool ReviveWithAd()
        {
            if (!_gameOver || _reviveUsed)
            {
                return false;
            }

            if (_adManager != null && _adManager.ShowRewardedAd())
            {
                _gameOver = false;
                GameOverPanelVisible = false;
                _reviveUsed = true;
                return true;
            }

            return false;
        }
    }
}
