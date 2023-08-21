using System;
using TicTacToe.Application;
using TicTacToe.Domain;
using TicTacToe.Infrastructure;
using TicTacToe.Presentation;
using TicTacToe.Presentation.VisualElementExtensions;
using TicTacToe.Utils;
using UnityEditor;
using UnityEngine;

namespace TicTacToe.Editor {
    public class TicTacToeWindow : EditorWindow {
        private IGameSettings _gameSettings;
        private IStyleSettings _styleSettings;
        private IViewSettings _viewSettings;
        private Board _board;
        private GameController _gameController;
        private InputManager _inputManager;
        private IPlayerMoveStrategy _manualPlayerMoveStrategy;
        private IPlayerMoveStrategy _automatedPlayerMoveStrategy;
        private PopupManager _popupManager;

        private void OnEnable() {
            LoadSettings();
            CreateGame();
        }

        private void LoadSettings() {
            var assetLoader = new AssetDatabaseAssetLoader();
            _gameSettings = assetLoader.LoadAsset<GameSettings>();
            _styleSettings = assetLoader.LoadAsset<StyleSettings>();
            _viewSettings = assetLoader.LoadAsset<ViewSettings>();
        }

        private void CreateGame() {
            _board = new Board(_gameSettings.BoardSize);
            _inputManager = new InputManager();
            _manualPlayerMoveStrategy = new ManualPlayerMoveStrategy(_inputManager);
            _automatedPlayerMoveStrategy = new DelayedRandomPlayerMoveStrategy(_gameSettings.AutomatedPlayerDelayMS);
            var playerX = CreatePlayer(_gameSettings.PlayerXMode);
            var playerO = CreatePlayer(_gameSettings.PlayerOMode);
            _gameController = new GameController(playerX, playerO, _board, _manualPlayerMoveStrategy,
                _automatedPlayerMoveStrategy);
        }

        private IPlayer CreatePlayer(PlayerMode mode) {
            var strategy = mode switch {
                PlayerMode.Auto => _automatedPlayerMoveStrategy,
                PlayerMode.Manual => _manualPlayerMoveStrategy,
                _ => throw new ArgumentOutOfRangeException(nameof(mode),
                    $"The player mode '{mode}' is not supported. Please use either 'PlayerMode.Auto' or 'PlayerMode.Manual'.")
            };

            return new Player(mode, strategy);
        }

        [MenuItem(MenuNames.SHOW_WINDOW)]
        private static void ShowWindow() {
            var window = EditorWindow.GetWindow<TicTacToeWindow>();
            window.titleContent = new GUIContent(MenuNames.WINDOW_TITLE);
            window.Show();
        }

        private void CreateGUI() {
            rootVisualElement.AddToStyle(_styleSettings.RootStyle);
            rootVisualElement.AddToClassList("root");
            this.minSize = new Vector2(_viewSettings.WindowDimensions.x, _viewSettings.WindowDimensions.y);
            this.maxSize = this.minSize;
            var cellPool = new CellPool(_gameSettings.BoardSize * _gameSettings.BoardSize, _styleSettings);
            var boardView = new BoardView(_gameSettings.BoardSize, _gameSettings.BoardSize, _styleSettings.BoardStyle, cellPool);
            var boardViewController = new BoardViewController(boardView, _board,
                _inputManager, _gameController, _viewSettings);

            var gameScreen = new GameScreen(boardView, _styleSettings.GameScreenStyle, _styleSettings.PlayerModeStyle);
            var gameScreenController = new GameScreenController(gameScreen, _gameController,
                _gameController, _gameSettings);

            rootVisualElement.Add(gameScreen);

            _popupManager = new PopupManager(rootVisualElement, _styleSettings, _gameController);
        }

        private void OnDestroy() {
            _gameController.Dispose();
            _popupManager.Dispose();
        }
    }
}