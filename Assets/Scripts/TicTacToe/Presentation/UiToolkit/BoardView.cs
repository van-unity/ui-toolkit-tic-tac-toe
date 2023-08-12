using System.Collections.Generic;
using TicTacToe.Domain;
using TicTacToe.Presentation.UiToolkit.CustomEvents;
using TicTacToe.Presentation.UiToolkit.VisualElementExtensions;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation.UiToolkit {
    public class BoardView : VisualElement {
        private const float GRID_LINE_OFFSET = .1f;

        private readonly VisualElement _gridLinesContainer;
        private readonly VisualElement _cellsContainer;
        private readonly VisualElement _winningLineContainer;
        private readonly int _rows;
        private readonly int _columns;
        private readonly CellPool _cellPool;
        private readonly List<Cell> _cells;

        private float _cellWidth;
        private float _cellHeight;
        private int _symbolSize;

        //for this game BoardView should be a square
        //so we could have just one _size field.
        //but we are leaving it this way because maybe we just want to throw some grid for another game or another purpose
        public BoardView(int rows, int columns, IStyleSettings styleSettings, CellPool cellPool) {
            _rows = rows;
            _columns = columns;
            _cellPool = cellPool;
            _cells = new List<Cell>(rows * columns);
            this.SetStyle(styleSettings.BoardStyle);
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
            clickEvent.PreventDefault();
            var boardPos = PixelToLogicPos(clickEvent.localPosition);
            var cellClickedEvent = new CellClickedEvent(boardPos, this);
            this.SendEvent(cellClickedEvent);
        }

        public void UpdateCell(BoardPosition position, PlayerSymbol playerSymbol) {
            var pixelPos = LogicToPixelPos(position);

            var cell = _cellPool.GetCell();
            cell.style.width = _cellWidth;
            cell.style.height = _cellHeight;
            cell.style.left = pixelPos.x;
            cell.style.top = pixelPos.y;
            cell.SetSymbol(playerSymbol.ToString());
            cell.SetSymbolSize(_symbolSize);
            cell.Hide();
            _cellsContainer.Add(cell);
            this.schedule
                .Execute(cell.Show)
                .ExecuteLater(16);
            _cells.Add(cell);
        }

        public void Initialize() {
            this.schedule.Execute(() => {
                _cellWidth = layout.width / _columns;
                _cellHeight = layout.height / _rows;
                _symbolSize = Mathf.CeilToInt(Mathf.Min(_cellWidth, _cellHeight));
                DrawGrid();
            }).ExecuteLater(TimeSettings.DELTA_TIME_MS);
        }

        private void DrawGrid() {
            for (int columnIndex = 1; columnIndex < _columns; columnIndex++) {
                var from = LogicToPixelPos(new BoardPosition(0, columnIndex));
                var to = LogicToPixelPos(new BoardPosition(_rows, columnIndex));
                from.y += GRID_LINE_OFFSET * _cellHeight;
                to.y -= GRID_LINE_OFFSET * _cellHeight;
                var line = new Line(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
                AnimateLineLength(line, line.Length, (columnIndex - 1) * 250);
            }

            for (int rowIndex = 1; rowIndex < _rows; rowIndex++) {
                var from = LogicToPixelPos(new BoardPosition(rowIndex, 0));
                var to = LogicToPixelPos(new BoardPosition(rowIndex, _columns));
                from.x += GRID_LINE_OFFSET * _cellWidth;
                to.x -= GRID_LINE_OFFSET * _cellWidth;
                var line = new Line(from, to);
                line.AddToClassList("grid-line");
                _gridLinesContainer.Add(line);
                AnimateLineLength(line, line.Length, (rowIndex - 1) * 250);
            }
        }

        private void AnimateLineLength(Line line, float width, int delayMS = 0) {
            line.Length = 0;
            line.schedule
                .Execute(() => { line.Length = width; })
                .ExecuteLater(TimeSettings.DELTA_TIME_MS + delayMS);
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
            foreach (var cell in _cells) {
                _cellPool.ReturnCell(cell);
            }

            _cells.Clear();
            _winningLineContainer.Clear();
            _cellsContainer.Clear();
        }
    }
}