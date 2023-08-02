using System;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class ManualMoveStrategy : IMoveStrategy {
        private readonly IBoardEventsProvider _boardEventsProvider;

        public ManualMoveStrategy(IBoardEventsProvider boardEventsProvider) {
            _boardEventsProvider = boardEventsProvider;
        }

        public void Play(BoardModel board, Action<BoardPosition> callback) {
            void Handler(BoardPosition position) {
                if (board.IsMoveValid(position)) {
                    callback?.Invoke(position);
                    _boardEventsProvider.CellClicked -= Handler;
                }
            }

            _boardEventsProvider.CellClicked += Handler;
        }
    }
}