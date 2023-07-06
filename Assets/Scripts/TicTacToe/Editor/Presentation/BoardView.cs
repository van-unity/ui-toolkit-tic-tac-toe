using TicTacToe.Editor.Utils;
using UnityEngine;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class BoardView : VisualElement {
        private const string STYLE_NAME = "Board";

        private readonly VisualElement _linesContainer;
        private readonly VisualElement _cellsContainer;
        private readonly float _cellWidth;
        private readonly float _cellHeight;
        private readonly BoardModel _model;

        public BoardView(BoardModel model) {
            StyleHelperMethods.SetStyleFromPath(this, STYLE_NAME);
            _model = model;
            _cellWidth = layout.width / _model.Columns;
            _cellHeight = layout.height / _model.Rows;
            _linesContainer = new VisualElement();
            _linesContainer.AddToClassList("lines-container");
            _cellsContainer = new VisualElement();
            _cellsContainer.AddToClassList("cells-container");
            this.Add(_cellsContainer);
            this.Add(_linesContainer);
        }

        public void UpdateCell(BoardPosition position, PlayerSymbol symbol) {
            var symbolSize = Mathf.CeilToInt(Mathf.Min(_cellWidth, _cellHeight));
            var element = symbol == PlayerSymbol.O
                ? (Label)new OSymbol(symbolSize, _cellWidth, _cellHeight)
                : new XSymbol(symbolSize, _cellWidth, _cellHeight);
            var left = position.columnIndex * _cellWidth;
            var top = position.rowIndex * _cellHeight;
            element.style.left = left;
            element.style.top = top;
            _cellsContainer.Add(element);
        }

        public void DrawBoardLines() {
            for (int columnIndex = 1; columnIndex < _model.Columns; columnIndex++) {
                var from = LogicToPixelPos(new Vector2(0.1f, columnIndex));
                var to = LogicToPixelPos(new Vector2(_model.Rows - .1f, columnIndex));
                var line = new AnimatedLine(from, to);
                line.AddToClassList("board-line");
                _linesContainer.Add(line);
            }

            for (int rowIndex = 1; rowIndex < _model.Rows; rowIndex++) {
                var from = LogicToPixelPos(new Vector2(rowIndex, .1f));
                var to = LogicToPixelPos(new Vector2(rowIndex, _model.Columns - .1f));
                var line = new AnimatedLine(from, to);
                line.AddToClassList("board-line");
                _linesContainer.Add(line);
            }
        }

        public void DrawWinningLine(Vector2 from, Vector2 to) {
            var fromPixel = LogicToPixelPos(from);
            fromPixel.x += .5f * _cellHeight;
            fromPixel.y += .5f * _cellWidth;
            var toPixel = LogicToPixelPos(to);
            toPixel.x += .5f * _cellHeight;
            toPixel.y += .5f * _cellWidth;
            var line = new AnimatedLine(fromPixel, toPixel);
            line.AddToClassList("winning-line");
            _linesContainer.Add(line);
        }
        
        private Vector2 LogicToPixelPos(Vector2 indexPos) => new(indexPos.x * _cellHeight, indexPos.y * _cellWidth);

        public Vector2Int PixelToLogicPos(Vector2 pixelPos) {
            var rowIndex = Mathf.CeilToInt(pixelPos.y / _cellHeight) - 1;
            var columnIndex = Mathf.CeilToInt(pixelPos.x / _cellWidth) - 1;

            return new Vector2Int(rowIndex, columnIndex);
        }
    }
}