using System;
using System.Collections.Generic;
using Editor.TicTacToe.Scripts.Domain;

namespace Editor.TicTacToe.Scripts.Application {
    ///<summary>
    /// The GameController class orchestrates the gameplay in the game.
    /// It manages the state of the game, players' turns, game events.
    /// </summary>
    public class GameController : IGameController, IGameEvents, IDisposable {
        private readonly Dictionary<PlayerSymbol, IPlayer> _playerBySymbol;
        private readonly Board _board;
        private readonly IPlayerMoveStrategy _manualPlayerMoveStrategy;
        private readonly IPlayerMoveStrategy _automatedPlayerMoveStrategy;

        private PlayerSymbol _currentPlayer;
        private bool _isGameStarted;
        private bool _restartRequested;

        public event Action GameStarted;

        public event Action<PlayerSymbol> TurnChanged;

        public event Action<Win> GameWon;

        public event Action GameDraw;

        public event Action<PlayerSymbol, PlayerMode> PlayerModeChanged;

        public event Action BeforeRestart;

        public GameController(IPlayer playerX, IPlayer playerO, Board board,
            IPlayerMoveStrategy manualPlayerMoveStrategy, IPlayerMoveStrategy automatedPlayerMoveStrategy) {
            _playerBySymbol = new Dictionary<PlayerSymbol, IPlayer> {
                { PlayerSymbol.X, playerX },
                { PlayerSymbol.O, playerO }
            };
            _board = board;
            _manualPlayerMoveStrategy = manualPlayerMoveStrategy;
            _automatedPlayerMoveStrategy = automatedPlayerMoveStrategy;
            _currentPlayer = PlayerSymbol.X;
            _board.CellUpdated += OnCellUpdated;
        }

        private void OnCellUpdated(BoardPosition position, PlayerSymbol playerSymbol) {
            if (_board.TryGetWinner(out var win)) {
                _isGameStarted = false;
                GameWon?.Invoke(win);
                return;
            }

            if (_board.IsFull) {
                _isGameStarted = false;
                GameDraw?.Invoke();
                return;
            }

            PlayNextPlayer();
        }

        private void PlayNextPlayer() {
            _currentPlayer = _currentPlayer switch {
                PlayerSymbol.O => PlayerSymbol.X,
                PlayerSymbol.X => PlayerSymbol.O,
                _ => throw new ArgumentOutOfRangeException()
            };

            TurnChanged?.Invoke(_currentPlayer);

            _playerBySymbol[_currentPlayer].MakeMove(_board, OnPlayerMoved);
        }

        public void Start() {
            _currentPlayer = PlayerSymbol.X;
            _isGameStarted = true;

            GameStarted?.Invoke();
            TurnChanged?.Invoke(_currentPlayer);

            _playerBySymbol[_currentPlayer].MakeMove(_board, OnPlayerMoved);
        }

        public void Restart() {
            _playerBySymbol[_currentPlayer].CancelMove();
            BeforeRestart?.Invoke();
            _board.Reset();

            Start();
        }

        public void TogglePlayerMode(PlayerSymbol playerSymbol) {
            if (_isGameStarted) {
                return;
            }

            var player = _playerBySymbol[playerSymbol];

            var (newMode, newStrategy) = player.PlayerMode == PlayerMode.Auto
                ? (PlayerMode.Manual, _manualPlayerMoveStrategy)
                : (PlayerMode.Auto, _automatedPlayerMoveStrategy);

            player.SetMode(newMode);
            player.SetStrategy(newStrategy);

            PlayerModeChanged?.Invoke(playerSymbol, newMode);
        }

        private void OnPlayerMoved(BoardPosition position) {
            _board.UpdateCell(position, _currentPlayer);
        }

        public void Dispose() {
            _board.CellUpdated -= OnCellUpdated;
        }
    }
}