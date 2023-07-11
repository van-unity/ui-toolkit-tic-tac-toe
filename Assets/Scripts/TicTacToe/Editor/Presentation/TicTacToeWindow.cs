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
        private BoardView _board;
        private GameController _gameController;

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
            var boardModel = new BoardModel(3, 3);
            _board = new BoardView(3, 3);
            var boardController = new BoardController(boardModel, _board);
            _board.SetController(boardController);
            var manualStrategy = new ManualMoveStrategy();
            var autoStrategy = new EaseAutomatedMoveStrategy(1000);
            var playerX = new Player(PlayerType.Manual, PlayerSymbol.X, manualStrategy, boardModel);
            var playerO = new Player(PlayerType.Auto, PlayerSymbol.O, autoStrategy, boardModel);
            _gameController = new GameController(new IPlayer[] { playerX, playerO }, boardController);
        }

        private void OnDisable() {
            _cancelOnDestroy.Cancel();
            _gameController.Dispose();
        }

        private void CreateGUI() {
            var restartButton = new Button(() => {
                rootVisualElement.Clear();
                CreateGame();
                CreateGUI();
            }) { text = "Restart Game" };

            rootVisualElement.Add(restartButton);
            rootVisualElement.Add(_board);
            rootVisualElement.schedule.Execute(() => {
                _board.DrawBoardLines();
                _gameController.Start();
            }).ExecuteLater(1000);
        }
    }
}