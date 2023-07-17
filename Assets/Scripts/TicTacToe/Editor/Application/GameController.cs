using System;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation;

namespace TicTacToe.Editor.Application {
    public class GameController : IDisposable{
        private readonly IPlayer[] _players;
        private readonly BoardModel _board;
        private readonly BoardView _boardView;

        private int _currentPlayerIndex;
        private bool _isGameEnded;

        public GameController(IPlayer[] players, BoardView boardView, BoardModel boardModel) {
            _players = players;
            _currentPlayerIndex = 0;
            _board = boardModel;
            _boardView = boardView;
            _board.CellUpdated += BoardOnCellUpdated;
            _boardView.CellClicked += BoardViewOnCellClicked;
        }

        private async void BoardViewOnCellClicked(BoardPosition position) {
            if (_isGameEnded) {
                return;
            }

            if (_players[_currentPlayerIndex].PlayerType != PlayerType.Manual) {
                return;
            }

            if (!_board.IsMoveValid(position)) {
                return;
            }

            await _players[_currentPlayerIndex].TakeTurn(_board, position);
        }

        private void BoardOnCellUpdated(BoardPosition position, Symbol symbol) {
            _boardView.UpdateCell(position, symbol);
            if (_board.HasWinner(position, out var win)) {
                _boardView.DrawWinningLine(win.Start, win.End);
                return;
            }

            _currentPlayerIndex = _currentPlayerIndex == 0 ? 1 : 0;
            PlayIfAuto();
        }

        public void Start() {
            PlayIfAuto();
        }

        private async void PlayIfAuto() {
            if (_players[_currentPlayerIndex].PlayerType == PlayerType.Auto) {
                await _players[_currentPlayerIndex].TakeTurn(_board, BoardPosition.Empty());
            }
        }

        public void Dispose() {
            _board.CellUpdated -= BoardOnCellUpdated;
            _boardView.CellClicked -= BoardViewOnCellClicked;
        }
    }
}