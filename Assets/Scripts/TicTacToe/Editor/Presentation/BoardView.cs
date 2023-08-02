using System;
using System.Collections.Generic;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.VisualElementExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class CellClickedEvent : EventBase<CellClickedEvent> {
        public BoardPosition ClickPosition { get; }

        public CellClickedEvent(BoardPosition clickPosition, IEventHandler target) {
            ClickPosition = clickPosition;
            this.target = target;
        }

        public CellClickedEvent() : this(BoardPosition.Invalid(), null) {
        }
    }

    public class BoardView : VisualElement {
        private const float HUNDRED_PIXEL_DURATION = 0.075f;
        private const EasingMode EASING_MODE = EasingMode.EaseOutSine;
        private const string STYLE_NAME = "Board";
        private const int DELTA_TIME = 16;

        private readonly VisualElement _gridLinesContainer;
        private readonly VisualElement _cellsContainer;
        private readonly VisualElement _winningLineContainer;
        private readonly int _rows;
        private readonly int _columns;

        private float _cellWidth;
        private float _cellHeight;

        //for this game BoardView should be a square
        //so we could have just one _size field.
        //but we are leaving it this way because maybe we just want to throw some grid for another game or another purpose
        public BoardView(int rows, int columns) {
            _rows = rows;
            _columns = columns;
            this.SetStyleFromPath(STYLE_NAME);
            this.AddToClassList("board");
            _gridLinesContainer = new VisualElement();
            _gridLinesContainer.AddToClassList("grid-lines-container");
            _cellsContainer = new VisualElement();
            _cellsContainer.AddToClassList("cells-container");
            _winningLineContainer = new VisualElement();
            _winningLineContainer.AddToClassList("winning-lines-container");
            this.Add(_cellsContainer);
            this.Add(_gridLinesContainer);
            this.Add(_winningLineContainer);
            this.RegisterCallback<ClickEvent>(OnClick);
        }

        private void OnClick(ClickEvent clickEvent) {
            var boardPos = PixelToLogicPos(clickEvent.localPosition);
            var cellClickedEvent = new CellClickedEvent(boardPos, this);
            this.SendEvent(cellClickedEvent);
        }

        public void UpdateCell(BoardPosition position, Symbol symbol) {
            var symbolSize = Mathf.CeilToInt(Mathf.Min(_cellWidth, _cellHeight));
            var element = new Cell(symbolSize, symbol.ToString()) {
                style = {
                    width = _cellWidth,
                    height = _cellHeight
                }
            };
            var left = position.ColumnIndex * _cellWidth;
            var top = position.RowIndex * _cellHeight;
            element.style.left = left;
            element.style.top = top;
            element.style.opacity = 0;
            element.style.scale = new StyleScale(new Vector2(.9f, .9f));
            _cellsContainer.Add(element);
            element.schedule
                .Execute(() => {
                    element.style.opacity = 1;
                    element.style.scale = new StyleScale(new Vector2(1, 1));
                })
                .ExecuteLater(16);
        }

        public void Initialize() {
            this.schedule.Execute(() => {
                _cellWidth = layout.width / _columns;
                _cellHeight = layout.height / _rows;
                DrawGrid();
            }).ExecuteLater(DELTA_TIME);
        }

        private void DrawGrid() {
            for (int columnIndex = 1; columnIndex < _columns; columnIndex++) {
                var from = LogicToPixelPos(new BoardPosition(0, columnIndex));
                var to = LogicToPixelPos(new BoardPosition(_rows, columnIndex));
                from.y += .1f * _cellHeight;
                to.y -= .1f * _cellHeight;
                var line = new Line(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
                AnimateLineLength(line, line.Length, (columnIndex - 1) * 250);
            }

            for (int rowIndex = 1; rowIndex < _rows; rowIndex++) {
                var from = LogicToPixelPos(new BoardPosition(rowIndex, 0));
                var to = LogicToPixelPos(new BoardPosition(rowIndex, _columns));
                from.x += .1f * _cellWidth;
                to.x -= .1f * _cellWidth;
                var line = new Line(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
                AnimateLineLength(line, line.Length, (rowIndex - 1) * 250);
            }
        }

        private void AnimateLineLength(Line line, float width, int delayMS = 0) {
            line.Length = 0;
            line.schedule.Execute(() => {
                var durationInSeconds = width / 100 * HUNDRED_PIXEL_DURATION;
                line.style.transitionProperty =
                    new StyleList<StylePropertyName>(new List<StylePropertyName>() { "width" });
                line.style.transitionTimingFunction = new StyleList<EasingFunction>(new List<EasingFunction>()
                    { new(EASING_MODE) });
                line.style.transitionDuration = new StyleList<TimeValue>(new List<TimeValue>()
                    { new(durationInSeconds, TimeUnit.Second) });
                line.Length = width;
            }).ExecuteLater(DELTA_TIME + delayMS);
        }

        public void DrawWinningLine(BoardPosition from, BoardPosition to) {
            var fromPixel = LogicToPixelPos(from);
            var toPixel = LogicToPixelPos(to);
            fromPixel.x += _cellWidth * .5f;
            fromPixel.y += _cellHeight * .5f;
            toPixel.x += _cellWidth * .5f;
            toPixel.y += _cellHeight * .5f;
            var line = new Line(fromPixel, toPixel);
            line.AddToClassList("winning-line");
            _winningLineContainer.Add(line);
            AnimateLineLength(line, line.Length);
        }

        private Vector2 LogicToPixelPos(BoardPosition logicPos) =>
            new(logicPos.ColumnIndex * _cellWidth,
                logicPos.RowIndex * _cellHeight);

        private BoardPosition PixelToLogicPos(Vector2 pixelPos) {
            var rowIndex = Mathf.CeilToInt(pixelPos.y / _cellHeight) - 1;
            var columnIndex = Mathf.CeilToInt(pixelPos.x / _cellWidth) - 1;

            return new BoardPosition(rowIndex, columnIndex);
        }

        public void Reset() {
            _winningLineContainer.Clear();
            _cellsContainer.Clear();
        }
    }
}