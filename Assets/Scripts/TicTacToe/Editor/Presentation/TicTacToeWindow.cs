using System.Threading;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class TicTacToeWindow : UnityEditor.EditorWindow {
        private readonly int[,] _matrix = new int[3, 3] {
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
        };

        private CancellationTokenSource _cancelOnDestroy;
        private TicTacToeSettings _settings;
        private Game _game;
        private Board _board;

        [MenuItem("TicTacToe/Play")]
        private static void ShowWindow() {
            var window = GetWindow<TicTacToeWindow>();
            window.titleContent = new GUIContent("TicTacToe-UiToolkit");
            window.Show();
        }

        private void OnEnable() {
            _cancelOnDestroy = new CancellationTokenSource();
            _board = new Board((int[,])_matrix.Clone());
            var playerX = new ManualPlayer(_board, PlayerSymbol.X);
            var playerO = new AutomatedPlayer(_board, PlayerSymbol.O);
            _game = new Game(playerX, playerO, _board);
        }

        private void OnDisable() {
            _cancelOnDestroy.Cancel();
            _game.Dispose();
        }

        private void CreateGUI() {
            var restartButton = new Button(() => {
                rootVisualElement.Clear();
                CreateGUI();
            }) { text = "Restart Game" };

            rootVisualElement.Add(restartButton);
            rootVisualElement.Add(_board);
            rootVisualElement.schedule.Execute(() => { _game.Run(); }).ExecuteLater(1000);
        }
    }
}