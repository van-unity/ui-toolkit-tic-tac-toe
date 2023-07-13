using System;
using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class ManualMoveStrategy : IMoveStrategy {
        private readonly BoardModel _board;

        public ManualMoveStrategy(BoardModel board) {
            _board = board;
        }

        public Task ExecuteAsync(IPlayer player, BoardPosition? clickPosition = null) {
            if (clickPosition == null) {
                throw new Exception("Null position");
            }

            _board.UpdateCell(clickPosition.Value, player.Symbol);
            return Task.CompletedTask;
        }
    }
}