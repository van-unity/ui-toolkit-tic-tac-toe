using TicTacToe.Editor.Application;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class BoardView : VisualElement {
        private const string STYLE_NAME = "Board";

        private readonly VisualElement _linesContainer;
        private readonly VisualElement _cellsContainer;
        private readonly int _rows;
        private readonly int _columns;
        private BoardController _controller;

        public BoardView(int rows, int columns) {
            _rows = rows;
            _columns = columns;
            StyleHelperMethods.SetStyleFromPath(this, STYLE_NAME);
            this.AddToClassList("board");
            _linesContainer = new VisualElement();
            _linesContainer.AddToClassList("lines-container");
            _cellsContainer = new VisualElement();
            _cellsContainer.AddToClassList("cells-container");
            this.Add(_cellsContainer);
            this.Add(_linesContainer);
            this.RegisterCallback<ClickEvent>(OnClick);
        }

        private void OnClick(ClickEvent clickEvent) {
            _controller.HandleClick(clickEvent.localPosition);
        }

        public void SetController(BoardController controller) {
            _controller = controller;
        }

        public void UpdateCell(BoardPosition position, PlayerSymbol symbol) {
            var cellWidth = GetCellWidth();
            var cellHeight = GetCellHeight();
            var symbolSize = Mathf.CeilToInt(Mathf.Min(cellWidth, cellHeight));
            var element = symbol == PlayerSymbol.O
                ? (Label)new OSymbol(symbolSize, cellWidth, cellHeight)
                : new XSymbol(symbolSize, cellWidth, cellHeight);
            element.AddToClassList("player-symbol");
            var left = position.columnIndex * cellWidth;
            var top = position.rowIndex * cellHeight;
            element.style.left = left;
            element.style.top = top;
            _cellsContainer.Add(element);
        }

        public void DrawBoardLines() {
            var cellWidth = GetCellWidth();
            var cellHeight = GetCellHeight();
            for (int columnIndex = 1; columnIndex < _columns; columnIndex++) {
                var from = LogicToPixelPos(new BoardPosition(0, columnIndex));
                var to = LogicToPixelPos(new BoardPosition(_rows, columnIndex));
                from.y += .1f * cellHeight;
                to.y -= .1f * cellHeight;
                var line = new AnimatedLine(from, to);
                line.AddToClassList("board-line");
                _linesContainer.Add(line);
            }

            for (int rowIndex = 1; rowIndex < _rows; rowIndex++) {
                var from = LogicToPixelPos(new BoardPosition(rowIndex, 0));
                var to = LogicToPixelPos(new BoardPosition(rowIndex, _columns));
                from.x += .1f * cellWidth;
                to.x -= .1f * cellWidth;
                var line = new AnimatedLine(from, to);
                line.AddToClassList("board-line");
                _linesContainer.Add(line);
            }
        }

        public void DrawWinningLine(BoardPosition from, BoardPosition to) {
            var cellWidth = GetCellWidth();
            var cellHeight = GetCellHeight();
            var fromPixel = LogicToPixelPos(from);
            var toPixel = LogicToPixelPos(to);
            fromPixel.x += cellWidth * .5f;
            fromPixel.y += cellHeight * .5f;
            toPixel.x += cellWidth * .5f;
            toPixel.y += cellHeight * .5f;
            var line = new AnimatedLine(fromPixel, toPixel);
            line.AddToClassList("winning-line");
            _linesContainer.Add(line);
        }

        public Vector2 LogicToPixelPos(BoardPosition logicPos) =>
            new(logicPos.columnIndex * GetCellWidth(),
                logicPos.rowIndex * GetCellHeight());

        public BoardPosition PixelToLogicPos(Vector2 pixelPos) {
            var cellWidth = GetCellWidth();
            var cellHeight = GetCellHeight();
            var rowIndex = Mathf.CeilToInt(pixelPos.y / cellHeight) - 1;
            var columnIndex = Mathf.CeilToInt(pixelPos.x / cellWidth) - 1;

            return new BoardPosition(rowIndex, columnIndex);
        }

        private float GetCellWidth() => layout.width / _columns;
        private float GetCellHeight() => layout.width / _rows;
    }
}