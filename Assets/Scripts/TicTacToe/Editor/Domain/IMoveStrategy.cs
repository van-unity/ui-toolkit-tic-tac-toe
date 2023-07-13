using System;
using System.Threading.Tasks;

namespace TicTacToe.Editor.Domain {
    public interface IMoveStrategy {
        Task ExecuteAsync(IPlayer player, BoardPosition? clickPosition = null);
    }
}