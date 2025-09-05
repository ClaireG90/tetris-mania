using System;

namespace TetrisMania
{
    /// <summary>
    /// Coordinates core game flow including start, game over and restart.
    /// </summary>
    public class GameManager
    {
        private readonly IAdManager _adManager;
        private BoardGrid _board = new BoardGrid();
        private PieceSpawner _spawner = new PieceSpawner();
        private readonly ScoreManager _scoreManager = new ScoreManager();
        private bool _gameOver;
        private bool _reviveUsed;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameManager"/> class.
        /// </summary>
        /// <param name="adManager">Ad system used for revives.</param>
        public GameManager(IAdManager adManager)
        {
            _adManager = adManager ?? throw new ArgumentNullException(nameof(adManager));
            _board.LinesCleared += _scoreManager.OnLinesCleared;
        }

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
        /// Starts a new game.
        /// </summary>
        public void StartGame()
        {
            _board = new BoardGrid();
            _board.LinesCleared += _scoreManager.OnLinesCleared;
            _spawner = new PieceSpawner();
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

            if (_adManager.ShowRewardedAd())
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
