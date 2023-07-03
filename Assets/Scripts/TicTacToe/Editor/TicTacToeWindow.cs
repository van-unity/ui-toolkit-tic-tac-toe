using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TicTacToe.Editor.Utils;
using TicTacToe.Editor.VisualElementExtensions;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

namespace TicTacToe.Editor {
    public class TicTacToeWindow : UnityEditor.EditorWindow {
        private int[,] _matrix = new int[3, 3] {
            { 0, 0, 0, },
            { 0, 0, 0, },
            { 0, 0, 0, },
        };

        private Dictionary<Vector2Int, VisualElement> _buttonsLookup;

        private VisualElement _board;
        private CancellationTokenSource _cancelOnDestroy;
        private TicTacToeSettings _settings;
        private Game _game;


        [UnityEditor.MenuItem("TicTacToe/Play")]
        private static void ShowWindow() {
            var window = GetWindow<TicTacToeWindow>();
            window.titleContent = new GUIContent("TicTacToe-UiToolkit");
            window.Show();
        }

        private void OnEnable() {
            _buttonsLookup = new Dictionary<Vector2Int, VisualElement>();
            _cancelOnDestroy = new CancellationTokenSource();
        }

        private void OnDisable() {
            _cancelOnDestroy.Cancel();
            _game.Dispose();
        }

        private FloatField _lineDurationField;
        private EnumField _easingField;

        private void CreateGUI() {
            _lineDurationField = new FloatField("LineDurationInSeconds: ") { value = .25f };
            _easingField = new EnumField("LineEasing: ", EasingMode.Linear);
            rootVisualElement.Add(_lineDurationField);
            rootVisualElement.Add(_easingField);
            var reloadButton = new Button(() => {
                _matrix = new int[3, 3] {
                    { 0, 0, 0, },
                    { 0, 0, 0, },
                    { 0, 0, 0, },
                };
                rootVisualElement.Clear();
                CreateGUI();
            }) { text = "Reload" };
            rootVisualElement.Add(reloadButton);

            var board = new Board(_matrix);
            rootVisualElement.Add(board);
            var playerX = new ManualPlayer(board, PlayerSymbol.X);
            var playerO = new AutomatedPlayer(board, PlayerSymbol.O);
            _game = new Game(playerX, playerO, board);

            rootVisualElement.schedule.Execute(() => {
                _game.Run();
            }).ExecuteLater(1000);
        }
    }
}