using System;
using TicTacToe.Domain;

namespace TicTacToe.Application {
    /// <summary>
    ///  The `ManualPlayerMoveStrategy` class implements the `IPlayerMoveStrategy` interface
    ///  to provide a manual move strategy, allowing a player to manually make a move.
    /// </summary >
    public class ManualPlayerMoveStrategy : IPlayerMoveStrategy {
        private readonly IInputEvents _inputEvents;
        private Action<BoardPosition> _handler;

        public ManualPlayerMoveStrategy(IInputEvents inputEvents) {
            _inputEvents = inputEvents;
        }

        public void Move(Board board, Action<BoardPosition> onMoveCompleted) {
            _handler = position => {
                if (board.IsMoveValid(position)) {
                    onMoveCompleted?.Invoke(position);
                    _inputEvents.CellClicked -= _handler;
                }
            };

            _inputEvents.CellClicked += _handler;
        }

        public void Cancel() {
            if (_handler == null) {
                return;
            }

            _inputEvents.CellClicked -= _handler;
            _handler = null;
        }
    }
}