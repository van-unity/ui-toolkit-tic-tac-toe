using System;

namespace TicTacToe.Editor.Domain {
    public interface IMoveStrategy {
        void Move(Board board, Action<BoardPosition> callback);
    }
}