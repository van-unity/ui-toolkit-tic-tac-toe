using System;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;
using UnityEngine;

namespace TicTacToe.Editor.Application {
    public class BoardController {
        private readonly BoardModel _model;
        private readonly BoardView _view;

        public event Action<BoardPosition> CellClicked;
        public event Action<Win> Win;
        public event Action Draw;

        public BoardController(BoardModel model, BoardView view) {
            _model = model;
            _view = view;
        }

        public void HandleClick(Vector2 clickPositionPx) {
            var logicPos = _view.PixelToLogicPos(clickPositionPx);
            if (_model.GetValueAt(logicPos) != PlayerSymbol.None) {
                return;
            }

            CellClicked?.Invoke(logicPos);
        }

        public void UpdateCell(BoardPosition position, PlayerSymbol symbol) {
            _model.SetValueAt(position, symbol);
            _view.UpdateCell(position, symbol);

            if (_model.HasWinner(position, out var win)) {
                _view.DrawWinningLine(win.Start, win.End);
                Win?.Invoke(win);
                return;
            }

            if (_model.IsBoardFull()) {
                Draw?.Invoke();
            }
        }
    }
}