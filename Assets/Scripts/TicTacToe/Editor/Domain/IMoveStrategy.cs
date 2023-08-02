using System;

namespace TicTacToe.Editor.Domain {
    public interface IMoveStrategy {
        void Play(BoardModel board, Action<BoardPosition> callback);
    }
}