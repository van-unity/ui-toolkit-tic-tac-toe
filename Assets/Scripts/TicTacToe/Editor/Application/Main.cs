using System;
using System.Collections.Generic;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Application {
    public static class Main {
        private static Symbol _userSymbol;
        private static GameController _gameController;
        private static TicTacToeWindow _window;

        public static BoardModel Board { get; private set; }
        public static BoardView BoardView { get; private set; }
        public static IPlayer PlayerX { get; private set; }
        public static IPlayer PlayerO { get; private set; }
        public static int BoardSize { get; private set; }

        public static Dictionary<Symbol, PlayerType> PlayerTypeBySymbol { get; private set; }

        [MenuItem("TicTacToe/Play")]
        private static void Initialize() {
            PlayerTypeBySymbol = new Dictionary<Symbol, PlayerType> {
                { Symbol.X, PlayerType.Manual },
                { Symbol.O, PlayerType.Auto },
            };
            BoardSize = 3;
            CreateGame();
            CreatePlayerO();
            CreatePlayerX();
            _gameController = new GameController(new[] { PlayerX, PlayerO }, BoardView, Board);
            ShowWindow();
            _window.rootVisualElement.RegisterCallback<DetachFromPanelEvent>(OnViewDetached);
            _window.rootVisualElement.schedule.Execute(() => {
                BoardView.Initialize();
                _gameController.Start();
            }).ExecuteLater(1000);
            _window.PlayerTypeChanged += OnPlayerTypeChanged;
        }

        private static void CreateGame() {
            Board = new BoardModel(BoardSize);
            BoardView = new BoardView(BoardSize, BoardSize);
        }

        private static void CreatePlayerO() {
            IMoveStrategy playerOStrategy = PlayerTypeBySymbol[Symbol.O] switch {
                PlayerType.Auto => new EaseAutomatedMoveStrategy(1000),
                PlayerType.Manual => new ManualMoveStrategy()
            };
            PlayerO = new Player(PlayerTypeBySymbol[Symbol.O], Symbol.O, playerOStrategy);
        }

        private static void CreatePlayerX() {
            IMoveStrategy playerXStrategy = PlayerTypeBySymbol[Symbol.X] switch {
                PlayerType.Auto => new EaseAutomatedMoveStrategy(1000),
                PlayerType.Manual => new ManualMoveStrategy()
            };
            PlayerX = new Player(PlayerTypeBySymbol[Symbol.X], Symbol.X, playerXStrategy);
        }

        private static void ShowWindow() {
            _window = EditorWindow.GetWindow<TicTacToeWindow>();
            _window.titleContent = new GUIContent("TicTacToe-UiToolkit");
            _window.Show();
        }

        private static void OnViewDetached(DetachFromPanelEvent detachFromPanelEvent) {
            if(_window.autoRepaintOnSceneChange)
            PlayerX = null;
            PlayerO = null;
            Board = null;
            BoardView = null;
            PlayerTypeBySymbol.Clear();
            _gameController = null;
            _window.PlayerTypeChanged -= OnPlayerTypeChanged;
        }

        private static void OnPlayerTypeChanged(Symbol symbol, PlayerType type) {
            Debug.LogError($"{symbol}, {type}");
            PlayerTypeBySymbol[symbol] = type;
            switch (symbol) {
                case Symbol.X:
                    CreatePlayerX();
                    break;
                case Symbol.O:
                    CreatePlayerO();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(symbol), symbol, $"Can't create player for {symbol}");
            }

            foreach (var playerType in PlayerTypeBySymbol) {
                Debug.LogError($"{playerType.Key} => {playerType.Value}");
            }
        }

        public static void Restart() {
            CreateGame();
            _gameController?.Dispose();
            _gameController = new GameController(new[] { PlayerX, PlayerO }, BoardView, Board);
            _window.rootVisualElement.schedule.Execute(() => {
                BoardView.Initialize();
                _gameController.Start();
            }).ExecuteLater(1000);
        }
    }
}