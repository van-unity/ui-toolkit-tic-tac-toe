using System;
using TicTacToe.Editor.Application;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;
using TicTacToe.Editor.VisualElementExtensions;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor {
    public class TicTacToeWindow : EditorWindow {
        private BoardModel _board;
        private IPlayer _playerX;
        private IPlayer _playerO;
        private IGameController _gameController;
        private IGameEventsProvider _gameEvents;
        private IBoardEventsHandler _boardEventsHandler;
        private IBoardEventsProvider _boardEventsProvider;
        private IGameSettings _gameSettings;
        private IMoveStrategy _manualMoveStrategy;
        private IMoveStrategy _automatedMoveStrategy;
        private PopupManager _popupManager;


        private void OnEnable() {
            LoadSettings();
            _board = new BoardModel(_gameSettings.BoardSize);
            var boardEventsManager = new BoardEventsManager();
            _boardEventsProvider = boardEventsManager;
            _boardEventsHandler = boardEventsManager;
            _manualMoveStrategy = new ManualMoveStrategy(_boardEventsProvider);
            _automatedMoveStrategy = new EaseAutomatedMoveStrategy();
            CreatePlayerO();
            CreatePlayerX();

            var popupsContainer = new VisualElement();
            popupsContainer.SetStyleFromPath("PopupsContainer");
            popupsContainer.AddToClassList("popups-container");
            _popupManager = new PopupManager(popupsContainer);

            var gameController = new GameController(_playerX, _playerO, _board, _gameSettings, _manualMoveStrategy,
                _automatedMoveStrategy, _popupManager);
            _gameController = gameController;
            _gameEvents = gameController;
            rootVisualElement.RegisterCallback<GeometryChangedEvent>(evt => evt.PreventDefault());
        }

        private void LoadSettings() {
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
            window.minSize = new Vector2(640, 960);
            window.maxSize = window.minSize;
            window.Show();
        }

        private void CreateGUI() {
            var boardView = new BoardView(_gameSettings.BoardSize, _gameSettings.BoardSize);
            var boardViewController = new BoardViewController(boardView, _board,
                _boardEventsHandler, _gameEvents);

            var gameScreen = new GameScreen(boardView);
            var gameScreenController = new GameScreenController(gameScreen, _gameEvents,
                _gameController, _gameSettings);

            rootVisualElement.Add(gameScreen);
            rootVisualElement.Add(_popupManager.Container);
        }

        private void OnDisable() {
        }
    }
}