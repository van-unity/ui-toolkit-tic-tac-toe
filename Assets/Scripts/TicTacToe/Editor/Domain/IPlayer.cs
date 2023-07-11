using System;

namespace TicTacToe.Editor.Domain {
    public interface IPlayer {
        PlayerSymbol Symbol { get; }
        PlayerType PlayerType { get; }
        void Move(BoardPosition clickPosition, Action<PlayerMovedResult> callback);
    }
}