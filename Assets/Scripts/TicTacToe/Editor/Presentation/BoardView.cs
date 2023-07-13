using System;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class BoardView : VisualElement {
        private const string STYLE_NAME = "Board";

        private readonly VisualElement _gridLinesContainer;
        private readonly VisualElement _cellsContainer;
        private readonly VisualElement _winningLineContainer;
        private readonly int _rows;
        private readonly int _columns;

        public event Action<BoardPosition> CellClicked; 

        public BoardView(int rows, int columns) {
            _rows = rows;
            _columns = columns;
            StyleHelperMethods.SetStyleFromPath(this, STYLE_NAME);
            this.AddToClassList("board");
            _gridLinesContainer = new VisualElement();
            _gridLinesContainer.AddToClassList("grid-lines-container");
            _cellsContainer = new VisualElement();
            _cellsContainer.AddToClassList("cells-container");
            _winningLineContainer = new VisualElement();
            _winningLineContainer.AddToClassList("winning-lines-container");
            this.Add(_cellsContainer);
            this.Add(_gridLinesContainer);
            this.RegisterCallback<ClickEvent>(OnClick);
        }

        private void OnClick(ClickEvent clickEvent) {
            var boardPos = PixelToLogicPos(clickEvent.localPosition);
            CellClicked?.Invoke(boardPos);
        }

        public void UpdateCell(BoardPosition position, Symbol symbol) {
            var cellWidth = GetCellWidth();
            var cellHeight = GetCellHeight();
            var symbolSize = Mathf.CeilToInt(Mathf.Min(cellWidth, cellHeight));
            var element = new Cell(symbolSize, symbol.ToString()) {
                style = {
                    width = cellWidth,
                    height = cellHeight
                }
            };
            var left = position.columnIndex * cellWidth;
            var top = position.rowIndex * cellHeight;
            element.style.left = left;
            element.style.top = top;
            _cellsContainer.Add(element);
        }

        public void DrawGrid() {
            var cellWidth = GetCellWidth();
            var cellHeight = GetCellHeight();
            for (int columnIndex = 1; columnIndex < _columns; columnIndex++) {
                var from = LogicToPixelPos(new BoardPosition(0, columnIndex));
                var to = LogicToPixelPos(new BoardPosition(_rows, columnIndex));
                from.y += .1f * cellHeight;
                to.y -= .1f * cellHeight;
                var line = new AnimatedLine(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
            }

            for (int rowIndex = 1; rowIndex < _rows; rowIndex++) {
                var from = LogicToPixelPos(new BoardPosition(rowIndex, 0));
                var to = LogicToPixelPos(new BoardPosition(rowIndex, _columns));
                from.x += .1f * cellWidth;
                to.x -= .1f * cellWidth;
                var line = new AnimatedLine(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
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
            _gridLinesContainer.Add(line);
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
        private float GetCellHeight() => layout.height / _rows;
    }
}