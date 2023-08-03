using System;

namespace TicTacToe.Editor.Domain {
    public interface IMoveStrategy {
        void Play(Board board, Action<BoardPosition> callback);
    }
}