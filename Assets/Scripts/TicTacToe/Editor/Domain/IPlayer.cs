using System;
using System.Threading.Tasks;

namespace TicTacToe.Editor.Domain {
    public interface IPlayer {
        Symbol Symbol { get; }
        PlayerMode PlayerMode { get; }
        void SetMode(PlayerMode mode);
        void SetStrategy(IMoveStrategy strategy);

        public void MakeMove(Board model, Action<BoardPosition> callback = null);
    }
}