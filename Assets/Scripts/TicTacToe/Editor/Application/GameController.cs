using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;

namespace TicTacToe.Editor.Application {
    public class GameController : IGameController, IGameEventsProvider, IDisposable {
        private readonly Dictionary<Symbol, IPlayer> _playersLookup;
        private readonly BoardModel _board;
        private readonly IGameSettings _gameSettings;
        private readonly IMoveStrategy _manualMoveStrategy;
        private readonly IMoveStrategy _automatedMoveStrategy;
        private readonly IPopupManager _popupManager;

        private Symbol _currentPlayer;
        public bool IsGameStarted { get; private set; }

        public event Action GameStarted;
        public event Action<Symbol> TurnChanged;
        public event Action<Win> GameWon;
        public event Action GameDraw;
        public event Action<Symbol, PlayerMode> PlayerModeChanged;
        public event Action BeforeRestart;

        public GameController(IPlayer playerX, IPlayer playerO, BoardModel boardModel, IGameSettings gameSettings,
            IMoveStrategy manualMoveStrategy, IMoveStrategy automatedMoveStrategy, IPopupManager popupManager) {
            _playersLookup = new Dictionary<Symbol, IPlayer>() {
                { Symbol.X, playerX },
                { Symbol.O, playerO }
            };
            _board = boardModel;
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
                IsGameStarted = false;
                WaitAndShowWinPopup(win.Symbol);
                return;
            }

            if (_board.IsFull()) {
                GameDraw?.Invoke();
                IsGameStarted = false;
                WaitAndShowDrawPopup();
                return;
            }

            _playersLookup[_currentPlayer].MakeMove(_board, OnPlayerMoved);
        }

        private async void WaitAndShowWinPopup(Symbol winSymbol) {
            await Task.Delay(750);
            await _popupManager.ShowWinPopupAsync(this, winSymbol);
        }

        private async void WaitAndShowDrawPopup() {
            await Task.Delay(750);
            await _popupManager.ShowDrawPopupAsync(this);
        }

        public void Start() {
            _currentPlayer = Symbol.X;
            IsGameStarted = true;
            GameStarted?.Invoke();
            TurnChanged?.Invoke(_currentPlayer);
            _playersLookup[_currentPlayer].MakeMove(_board, OnPlayerMoved);
        }

        public void Restart() {
            BeforeRestart?.Invoke();
            _board.Reset();
            Start();
        }

        public void TogglePlayerMode(Symbol playerSymbol) {
            if (IsGameStarted) {
                return;
            }

            var player = _playersLookup[playerSymbol];
            var newMode = player.PlayerMode == PlayerMode.Auto ? PlayerMode.Manual : PlayerMode.Auto;
            switch (newMode) {
                case PlayerMode.Manual:
                    player.SetMode(PlayerMode.Manual);
                    player.SetStrategy(_manualMoveStrategy);
                    break;
                case PlayerMode.Auto:
                    player.SetMode(PlayerMode.Auto);
                    player.SetStrategy(_automatedMoveStrategy);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            switch (playerSymbol) {
                case Symbol.X:
                    _gameSettings.SetPlayerXMode(newMode);
                    break;
                case Symbol.O:
                    _gameSettings.SetPlayerOMode(newMode);
                    break;
            }

            PlayerModeChanged?.Invoke(playerSymbol, newMode);
        }

        private void OnPlayerMoved(BoardPosition position) {
            if (!_board.IsMoveValid(position)) {
                return;
            }

            var current = _currentPlayer;

            _currentPlayer = _currentPlayer switch {
                Symbol.O => Symbol.X,
                Symbol.X => Symbol.O,
                _ => throw new ArgumentOutOfRangeException()
            };
            TurnChanged?.Invoke(_currentPlayer);

            _board.UpdateCell(position, current);
        }

        public void Dispose() {
            _board.CellUpdated -= BoardOnCellUpdated;
        }
    }
}