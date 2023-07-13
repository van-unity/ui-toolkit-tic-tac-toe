using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class EaseAutomatedMoveStrategy : IMoveStrategy {
        private readonly BoardModel _board;
        private readonly int _delayInMs;

        public EaseAutomatedMoveStrategy(BoardModel board, int delayInMs) {
            _board = board;
            _delayInMs = delayInMs;
        }

        public async Task ExecuteAsync(IPlayer player, BoardPosition? clickPosition = null) {
            await Task.Delay(_delayInMs);

            var emptyCells = _board.GetEmptyCells();
            if (emptyCells.Count <= 0) {
                return;
            }

            var chosenCell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
            _board.UpdateCell(chosenCell, player.Symbol);
        }
    }
}