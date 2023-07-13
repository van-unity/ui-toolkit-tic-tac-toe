using System.Threading;
using TicTacToe.Editor.Application;
using TicTacToe.Editor.Domain;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class TicTacToeWindow : UnityEditor.EditorWindow {
        private CancellationTokenSource _cancelOnDestroy;
        
        // private Game _game;
        private BoardView _boardView;
        private GameController _gameController;
        private VisualElement _popupsContainer;

        [MenuItem("TicTacToe/Play")]
        private static void ShowWindow() {
            var window = GetWindow<TicTacToeWindow>();
            window.titleContent = new GUIContent("TicTacToe-UiToolkit");
            window.Show();
        }

        private void OnEnable() {
            _cancelOnDestroy = new CancellationTokenSource();
            CreateGame();
        }

        private void CreateGame() {
            var board = new BoardModel(3, 3);
            _boardView = new BoardView(3, 3);
            var manualStrategy = new ManualMoveStrategy(board);
            var autoStrategy = new EaseAutomatedMoveStrategy(board, 1000);
            var playerX = new Player(PlayerType.Manual, Symbol.X, manualStrategy);
            var playerO = new Player(PlayerType.Auto, Symbol.O, autoStrategy);
            _gameController = new GameController(new IPlayer[] { playerX, playerO }, _boardView, board);
        }

        private void OnDisable() {
            _cancelOnDestroy.Cancel();
        }

        private void CreateGUI() {
            var restartButton = new Button(() => {
                rootVisualElement.Clear();
                CreateGame();
                CreateGUI();
            }) { text = "Restart Game" };

            _popupsContainer = new VisualElement();
            rootVisualElement.Add(restartButton);
            rootVisualElement.Add(_boardView);
            rootVisualElement.Add(_popupsContainer);
            rootVisualElement.schedule.Execute(() => {
                _boardView.DrawGrid();
                _gameController.Start();
            }).ExecuteLater(1000);
        }
    }
}