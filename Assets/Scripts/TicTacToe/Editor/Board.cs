using TicTacToe.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor {
    public class Board : VisualElement {
        private readonly VisualElement _linesContainer;
        private readonly VisualElement _cellsContainer;
        private readonly PlayerSymbol[,] _state;

        private float CellWidth => layout.width / Columns;
        private float CellHeight => layout.height / Rows;

        public int Rows { get; }
        public int Columns { get; }

        public Board(int[,] matrix) {
            StyleHelperMethods.SetStyleFromPath(this, "Board");
            this.AddToClassList("board");
            Rows = matrix.GetLength(0);
            Columns = matrix.GetLength(1);
            _linesContainer = new VisualElement();
            _linesContainer.AddToClassList("lines-container");
            _cellsContainer = new VisualElement();
            _cellsContainer.AddToClassList("cells-container");
            this.schedule.Execute(DrawBoardLines).ExecuteLater(500);
            _state = new PlayerSymbol[Rows, Columns];
            for (int rowIndex = 0; rowIndex < Rows; rowIndex++) {
                for (int columnIndex = 0; columnIndex < Columns; columnIndex++) {
                    _state[rowIndex, columnIndex] = matrix[rowIndex, columnIndex] switch {
                        1 => PlayerSymbol.X,
                        2 => PlayerSymbol.O,
                        _ => PlayerSymbol.None
                    };
                }
            }

            this.Add(_cellsContainer);
            this.Add(_linesContainer);
        }

        public PlayerSymbol ValueAt(int rowIndex, int columnIndex) => _state[rowIndex, columnIndex];

        public void PlaceSymbolAt(PlayerSymbol symbol, int rowIndex, int columnIndex) {
            _state[rowIndex, columnIndex] = symbol;
            var element = symbol == PlayerSymbol.O ? OSign() : XSign();
            var left = columnIndex * CellWidth;
            var top = rowIndex * CellHeight;
            element.style.left = left;
            element.style.top = top;
            _cellsContainer.Add(element);
        }

        private VisualElement OSign() {
            var label = new Label("O") {
                style = {
                    width = CellWidth,
                    height = CellHeight,
                    fontSize = Mathf.CeilToInt(Mathf.Min(CellWidth, CellHeight))
                }
            };
            label.AddToClassList("o");
            return label;
        }

        private VisualElement XSign() {
            var label = new Label("X") {
                style = {
                    width = CellWidth,
                    height = CellHeight,
                    fontSize = Mathf.CeilToInt(Mathf.Min(CellWidth, CellHeight))
                }
            };
            label.AddToClassList("x");
            return label;
        }

        private void DrawBoardLines() {
            for (int columnIndex = 1; columnIndex < Columns; columnIndex++) {
                var from = LogicToPixelPos(new Vector2(0.1f, columnIndex));
                var to = LogicToPixelPos(new Vector2(Rows - .1f, columnIndex));
                var line = new AnimatedLine(from, to);
                line.AddToClassList("board-line");
                _linesContainer.Add(line);
            }

            for (int rowIndex = 1; rowIndex < Rows; rowIndex++) {
                var from = LogicToPixelPos(new Vector2(rowIndex, .1f));
                var to = LogicToPixelPos(new Vector2(rowIndex, Columns - .1f));
                var line = new AnimatedLine(from, to);
                line.AddToClassList("board-line");
                _linesContainer.Add(line);
            }
        }

        public void DrawWinningLine(Vector2 from, Vector2 to) {
            from.x += .5f;
            from.y += .5f;
            to.x += .5f;
            to.y += .5f;
            var fromPixel = LogicToPixelPos(from);
            var toPixel = LogicToPixelPos(to);
            var line = new AnimatedLine(fromPixel, toPixel);
            line.AddToClassList("winning-line");
            _linesContainer.Add(line);
        }

        public Vector2 LogicToPixelPos(Vector2 indexPos) => new(indexPos.x * CellHeight, indexPos.y * CellWidth);

        public Vector2Int PixelToLogicPos(Vector2 pixelPos) {
            var rowIndex = Mathf.CeilToInt(pixelPos.y / CellHeight) - 1;
            var columnIndex = Mathf.CeilToInt(pixelPos.x / CellWidth) - 1;

            return new Vector2Int(rowIndex, columnIndex);
        }

        public new void Clear() {
            _cellsContainer.Clear();
        }
    }
}