using System;

namespace TicTacToe.Editor.Domain {
    public interface IPlayerMoveStrategy {
        void Execute(BoardModel model, BoardPosition clickPosition, Action<BoardPosition> callback);
    }
}