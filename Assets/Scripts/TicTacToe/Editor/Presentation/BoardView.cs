using System;
using System.Collections.Generic;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class BoardView : VisualElement {
        private const float HUNDRED_PIXEL_DURATION = 0.075f;
        private const EasingMode EASING_MODE = EasingMode.EaseOutSine;
        private const string STYLE_NAME = "Board";

        private readonly VisualElement _gridLinesContainer;
        private readonly VisualElement _cellsContainer;
        private readonly VisualElement _winningLineContainer;
        private readonly int _rows;
        private readonly int _columns;

        private float _cellWidth;
        private float _cellHeight;

        public event Action<BoardPosition> CellClicked;

        //for this game BoardView should be a square
        //so we could have just one _size field.
        //but we are leaving it this way because maybe we just want to throw some grid for another game or another purpose
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
            var symbolSize = Mathf.CeilToInt(Mathf.Min(_cellWidth, _cellHeight));
            var element = new Cell(symbolSize, symbol.ToString()) {
                style = {
                    width = _cellWidth,
                    height = _cellHeight
                }
            };
            var left = position.columnIndex * _cellWidth;
            var top = position.rowIndex * _cellHeight;
            element.style.left = left;
            element.style.top = top;
            _cellsContainer.Add(element);
        }

        public void Initialize() {
            _cellWidth = layout.width / _columns;
            _cellHeight = layout.height / _rows;
            DrawGrid();
        }

        public void DrawGrid() {
            for (int columnIndex = 1; columnIndex < _columns; columnIndex++) {
                var from = LogicToPixelPos(new BoardPosition(0, columnIndex));
                var to = LogicToPixelPos(new BoardPosition(_rows, columnIndex));
                from.y += .1f * _cellHeight;
                to.y -= .1f * _cellHeight;
                var line = new AnimatedLine(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
                AnimateWidth(line, line.Width);
            }

            for (int rowIndex = 1; rowIndex < _rows; rowIndex++) {
                var from = LogicToPixelPos(new BoardPosition(rowIndex, 0));
                var to = LogicToPixelPos(new BoardPosition(rowIndex, _columns));
                from.x += .1f * _cellWidth;
                to.x -= .1f * _cellWidth;
                var line = new AnimatedLine(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
                AnimateWidth(line, line.Width);
            }
        }

        private void AnimateWidth(VisualElement element, float width) {
            element.style.width = 0;
            element.schedule.Execute(() => {
                var durationInSeconds = width / 100 * HUNDRED_PIXEL_DURATION;
                element.style.transitionProperty =
                    new StyleList<StylePropertyName>(new List<StylePropertyName>() { "width" });
                element.style.transitionTimingFunction = new StyleList<EasingFunction>(new List<EasingFunction>()
                    { new(EASING_MODE) });
                element.style.transitionDuration = new StyleList<TimeValue>(new List<TimeValue>()
                    { new(durationInSeconds, TimeUnit.Second) });
                element.style.width = width;
            }).ExecuteLater(16);
        }

        public void DrawWinningLine(BoardPosition from, BoardPosition to) {
            var fromPixel = LogicToPixelPos(from);
            var toPixel = LogicToPixelPos(to);
            fromPixel.x += _cellWidth * .5f;
            fromPixel.y += _cellHeight * .5f;
            toPixel.x += _cellWidth * .5f;
            toPixel.y += _cellHeight * .5f;
            var line = new AnimatedLine(fromPixel, toPixel);
            line.AddToClassList("winning-line");
            _gridLinesContainer.Add(line);
            AnimateWidth(line, line.Width);
        }

        public Vector2 LogicToPixelPos(BoardPosition logicPos) =>
            new(logicPos.columnIndex * _cellWidth,
                logicPos.rowIndex * _cellHeight);

        public BoardPosition PixelToLogicPos(Vector2 pixelPos) {
            var rowIndex = Mathf.CeilToInt(pixelPos.y / _cellHeight) - 1;
            var columnIndex = Mathf.CeilToInt(pixelPos.x / _cellWidth) - 1;

            return new BoardPosition(rowIndex, columnIndex);
        }
    }
}