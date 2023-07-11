using System;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class ManualMoveStrategy : IPlayerMoveStrategy {
        public void Execute(BoardModel model, BoardPosition clickPosition, Action<BoardPosition> callback) {
            callback.Invoke(clickPosition);
        }
    }
}