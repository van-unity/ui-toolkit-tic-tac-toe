using System;
using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class ManualMoveStrategy : IMoveStrategy {
        public Task ExecuteAsync(IPlayer player, BoardModel board, BoardPosition? clickPosition) {
            if (clickPosition == null) {
                throw new ArgumentNullException(nameof(clickPosition), "Click position cannot be null.");
            }

            board.UpdateCell(clickPosition.Value, player.Symbol);
            return Task.CompletedTask;
        }
    }
}