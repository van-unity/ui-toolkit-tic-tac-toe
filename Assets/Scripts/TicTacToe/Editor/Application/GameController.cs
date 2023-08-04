using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;

namespace TicTacToe.Editor.Application {
    public class GameController : IGameController, IGameEventsProvider {
        private const int POPUPS_DELAY_MS = 750;
        private readonly Dictionary<Symbol, IPlayer> _playerBySymbol;
        private readonly Board _board;
        private readonly IGameSettings _gameSettings;
        private readonly IMoveStrategy _manualMoveStrategy;
        private readonly IMoveStrategy _automatedMoveStrategy;
        private readonly IPopupManager _popupManager;

        private Symbol _currentPlayer;
        private bool _isGameStarted;

        public event Action GameStarted;
        public event Action<Symbol> TurnChanged;
        public event Action<Win> GameWon;
        public event Action GameDraw;
        public event Action<Symbol, PlayerMode> PlayerModeChanged;
        public event Action BeforeRestart;

        public GameController(IPlayer playerX, IPlayer playerO, Board board, IGameSettings gameSettings,
            IMoveStrategy manualMoveStrategy, IMoveStrategy automatedMoveStrategy, IPopupManager popupManager) {
            _playerBySymbol = new Dictionary<Symbol, IPlayer> {
                { Symbol.X, playerX },
                { Symbol.O, playerO }
            };
            _board = board;
            _gameSettings = gameSettings;
            _manualMoveStrategy = manualMoveStrategy;
            _automatedMoveStrategy = automatedMoveStrategy;
            _popupManager = popupManager;
            _currentPlayer = Symbol.X;
            _board.CellUpdated += BoardOnCellUpdated;
        }

        private void BoardOnCellUpdated(BoardPosition position, Symbol symbol) {
            if (_board.HasWinner(out var win)) {
                GameWon?.Invoke(win);
                _isGameStarted = false;
                WaitAndShowWinPopup(win.Symbol);
                return;
            }

            if (_board.IsFull) {
                GameDraw?.Invoke();
                _isGameStarted = false;
                WaitAndShowDrawPopup();
                return;
            }

            PlayNextPlayer();
        }

        private void PlayNextPlayer() {
            _currentPlayer = _currentPlayer switch {
                Symbol.O => Symbol.X,
                Symbol.X => Symbol.O,
                _ => throw new ArgumentOutOfRangeException()
            };

            TurnChanged?.Invoke(_currentPlayer);

            _playerBySymbol[_currentPlayer].MakeMove(_board, OnPlayerMoved);
        }

        private async void WaitAndShowWinPopup(Symbol winSymbol) {
            await Task.Delay(POPUPS_DELAY_MS);
            await _popupManager.ShowWinPopupAsync(winSymbol);
        }

        private async void WaitAndShowDrawPopup() {
            await Task.Delay(POPUPS_DELAY_MS);
            await _popupManager.ShowDrawPopupAsync();
        }

        public void Start() {
            _currentPlayer = Symbol.X;
            _isGameStarted = true;
            GameStarted?.Invoke();
            TurnChanged?.Invoke(_currentPlayer);
            _playerBySymbol[_currentPlayer].MakeMove(_board, OnPlayerMoved);
        }

        public void Restart() {
            BeforeRestart?.Invoke();
            _board.Reset();
            Start();
        }

        public void TogglePlayerMode(Symbol playerSymbol) {
            if (_isGameStarted) {
                return;
            }

            var player = _playerBySymbol[playerSymbol];
            
            var (newMode, newStrategy) = player.PlayerMode == PlayerMode.Auto
                ? (PlayerMode.Manual, _manualMoveStrategy)
                : (PlayerMode.Auto, _automatedMoveStrategy);
            
            player.SetMode(newMode);
            player.SetStrategy(newStrategy);

            PlayerModeChanged?.Invoke(playerSymbol, newMode);

            UpdatePlayerModeSettings(playerSymbol, newMode);
        }

        private void UpdatePlayerModeSettings(Symbol symbol, PlayerMode newMode) {
            switch (symbol) {
                case Symbol.X:
                    _gameSettings.SetPlayerXMode(newMode);
                    break;
                case Symbol.O:
                    _gameSettings.SetPlayerOMode(newMode);
                    break;
            }
        }

        private void OnPlayerMoved(BoardPosition position) {
            _board.UpdateCell(position, _currentPlayer);
        }
    }
}