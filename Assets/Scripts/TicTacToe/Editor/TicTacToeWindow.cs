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
            LoadGameSettings();
            LoadStyleSettings();
            _board = new Board(_gameSettings.BoardSize);
            var boardEventsManager = new BoardEventsManager();
            _boardEventsProvider = boardEventsManager;
            _boardEventsHandler = boardEventsManager;
            _manualMoveStrategy = new ManualMoveStrategy(_boardEventsProvider);
            _automatedMoveStrategy = new EaseAutomatedMoveStrategy(1000);
            CreatePlayerO();
            CreatePlayerX();

            var popupsContainer = new PopupsContainer(_styleSettings);
            _popupManager = new PopupManager(popupsContainer, _styleSettings);

            var gameController = new GameController(_playerX, _playerO, _board, _gameSettings, _manualMoveStrategy,
                _automatedMoveStrategy, _popupManager);
            _gameController = gameController;
            _gameEvents = gameController;
        }

        private void LoadGameSettings() {
            var guids = AssetDatabase.FindAssets($"t:{typeof(GameSettings)}");
            switch (guids.Length) {
                case > 1:
                    Debug.LogWarning("Multiple GameSettings found!");
                    break;
                case 0:
                    throw new NullReferenceException("Unable to find GameSettings!");
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            _gameSettings = AssetDatabase.LoadAssetAtPath<GameSettings>(path);
        }

        private void LoadStyleSettings() {
            var guids = AssetDatabase.FindAssets($"t:{typeof(StyleSettings)}");
            switch (guids.Length) {
                case > 1:
                    Debug.LogWarning("Multiple GameSettings found!");
                    break;
                case 0:
                    throw new NullReferenceException("Unable to find GameSettings!");
            }

            var path = AssetDatabase.GUIDToAssetPath(guids[0]);
            _styleSettings = AssetDatabase.LoadAssetAtPath<StyleSettings>(path);
        }

        private void CreatePlayerO() {
            var playerOStrategy = _gameSettings.PlayerOMode switch {
                PlayerMode.Auto => _automatedMoveStrategy,
                PlayerMode.Manual => _manualMoveStrategy,
                _ => throw new ArgumentOutOfRangeException()
            };
            _playerO = new Player(_gameSettings.PlayerOMode, Symbol.O, playerOStrategy);
        }

        private void CreatePlayerX() {
            var playerXStrategy = _gameSettings.PlayerXMode switch {
                PlayerMode.Auto => _automatedMoveStrategy,
                PlayerMode.Manual => _manualMoveStrategy,
                _ => throw new ArgumentOutOfRangeException()
            };
            _playerX = new Player(_gameSettings.PlayerXMode, Symbol.X, playerXStrategy);
        }

        [MenuItem("TicTacToe/Play")]
        private static void ShowWindow() {
            var window = EditorWindow.GetWindow<TicTacToeWindow>();
            window.titleContent = new GUIContent();
            window.Show();
        }

        private void CreateGUI() {
            this.minSize = new Vector2(_gameSettings.BoardDimensions.x, _gameSettings.BoardDimensions.y);
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