using System;
using System.Threading.Tasks;

namespace TicTacToe.Editor.Domain {
    public interface IPlayer {
        Symbol Symbol { get; }
        PlayerType PlayerType { get; }
        Task TakeTurn(BoardModel board, BoardPosition clickPosition);
    }
}