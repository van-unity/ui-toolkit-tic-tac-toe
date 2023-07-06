using TicTacToe.Editor.Presentation;
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

        public PlayerSymbol ValueAt(BoardPosition position) => _state[position.rowIndex, position.columnIndex];

        public void PlaceSymbolAt(PlayerSymbol symbol, BoardPosition position) {
            _state[position.rowIndex, position.columnIndex] = symbol;
            var symbolSize = Mathf.CeilToInt(Mathf.Min(CellWidth, CellHeight));
            var element = symbol == PlayerSymbol.O
                ? (Label)new OSymbol(symbolSize, CellWidth, CellHeight)
                : new XSymbol(symbolSize, CellWidth, CellHeight);
            element.AddToClassList("player-symbol");
            var left = position.columnIndex * CellWidth;
            var top = position.rowIndex * CellHeight;
            element.style.left = left;
            element.style.top = top;
            _cellsContainer.Add(element);
        }

        private void DrawBoardLines() {
            for (int columnIndex = 1; columnIndex < Columns; columnIndex++) {
                var from = LogicToPixelPos(new BoardPosition(0, columnIndex));
                var to = LogicToPixelPos(new BoardPosition(Rows, columnIndex));
                from.y += .1f * CellHeight;
                to.y -= .1f * CellHeight;
                var line = new AnimatedLine(from, to);
                line.AddToClassList("board-line");
                _linesContainer.Add(line);
            }

            for (int rowIndex = 1; rowIndex < Rows; rowIndex++) {
                var from = LogicToPixelPos(new BoardPosition(rowIndex, 0));
                var to = LogicToPixelPos(new BoardPosition(rowIndex, Columns));
                from.x += .1f * CellWidth;
                to.x -= .1f * CellWidth;
                var line = new AnimatedLine(from, to);
                line.AddToClassList("board-line");
                _linesContainer.Add(line);
            }
        }

        public void DrawWinningLine(BoardPosition from, BoardPosition to) {
            var fromPixel = LogicToPixelPos(from);
            var toPixel = LogicToPixelPos(to);
            fromPixel.x += CellWidth * .5f;
            fromPixel.y += CellHeight * .5f;
            toPixel.x += CellWidth * .5f;
            toPixel.y += CellHeight * .5f;
            var line = new AnimatedLine(fromPixel, toPixel);
            line.AddToClassList("winning-line");
            _linesContainer.Add(line);
        }

        public Vector2 LogicToPixelPos(BoardPosition logicPos) =>
            new(logicPos.columnIndex * CellWidth, logicPos.rowIndex * CellHeight);

        public BoardPosition PixelToLogicPos(Vector2 pixelPos) {
            var rowIndex = Mathf.CeilToInt(pixelPos.y / CellHeight) - 1;
            var columnIndex = Mathf.CeilToInt(pixelPos.x / CellWidth) - 1;

            return new BoardPosition(rowIndex, columnIndex);
        }

        public new void Clear() {
            _cellsContainer.Clear();
        }
    }
}