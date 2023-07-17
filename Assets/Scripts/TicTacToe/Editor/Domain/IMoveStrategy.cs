using System;
using System.Threading.Tasks;

namespace TicTacToe.Editor.Domain {
    public interface IMoveStrategy {
        Task ExecuteAsync(IPlayer player, BoardModel board = null, BoardPosition? clickPosition = null);
    }
}