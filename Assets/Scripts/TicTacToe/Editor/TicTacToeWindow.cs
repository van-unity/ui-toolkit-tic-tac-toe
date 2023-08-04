using System;
using TicTacToe.Editor.Application;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;
using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor {
    public class TicTacToeWindow : EditorWindow {
        private IGameSettings _gameSettings;
        private IStyleSettings _styleSettings;
        private Board _board;
        private IPlayer _playerX;
        private IPlayer _playerO;
        private IGameController _gameController;
        private IGameEventsProvider _gameEvents;
        private IBoardEventsHandler _boardEventsHandler;
        private IBoardEventsProvider _boardEventsProvider;
        private IMoveStrategy _manualMoveStrategy;
        private IMoveStrategy _automatedMoveStrategy;
        private IPopupManager _popupManager;

        private void OnEnable() {
            _gameSettings = AssetLoader.LoadAsset<GameSettings>();
            _styleSettings = AssetLoader.LoadAsset<StyleSettings>();
            _board = new Board(_gameSettings.BoardSize);
            var boardEventsManager = new BoardEventsManager();
            _boardEventsProvider = boardEventsManager;
            _boardEventsHandler = boardEventsManager;
            _manualMoveStrategy = new ManualMoveStrategy(_boardEventsProvider);
            _automatedMoveStrategy = new DelayedRandomMoveStrategy(_gameSettings.AutomatedPlayerDelayMS);
            _playerX = CreatePlayer(Symbol.X, _gameSettings.PlayerXMode);
            _playerO = CreatePlayer(Symbol.O, _gameSettings.PlayerOMode);

            var popupsContainer = new PopupsContainer(_styleSettings);
            _popupManager = new PopupManager(popupsContainer, _styleSettings);

            var gameController = new GameController(_playerX, _playerO, _board, _gameSettings, _manualMoveStrategy,
                _automatedMoveStrategy, _popupManager);
            _gameController = gameController;
            _gameEvents = gameController;
        }

        private IPlayer CreatePlayer(Symbol symbol, PlayerMode mode) {
            var strategy = mode switch {
                PlayerMode.Auto => _automatedMoveStrategy,
                PlayerMode.Manual => _manualMoveStrategy,
                _ => throw new ArgumentOutOfRangeException(nameof(mode),
                    $"The player mode '{mode}' is not supported. Please use either 'PlayerMode.Auto' or 'PlayerMode.Manual'.")
            };

            return new Player(mode, symbol, strategy);
        }

        [MenuItem("TicTacToe/Play")]
        private static void ShowWindow() {
            var window = EditorWindow.GetWindow<TicTacToeWindow>();
            window.titleContent = new GUIContent();
            window.Show();
        }

        private void CreateGUI() {
            this.minSize = new Vector2(_gameSettings.WindowDimensions.x, _gameSettings.WindowDimensions.y);
            this.maxSize = this.minSize;
            var boardView = new BoardView(_gameSettings.BoardSize, _gameSettings.BoardSize, _styleSettings);
            var boardViewController = new BoardViewController(boardView, _board,
                _boardEventsHandler, _gameEvents);

            var gameScreen = new GameScreen(boardView, _styleSettings);
            var gameScreenController = new GameScreenController(gameScreen, _gameEvents,
                _gameController, _gameSettings);

            rootVisualElement.Add(gameScreen);
            rootVisualElement.Add(_popupManager.Container);
        }
    }
}