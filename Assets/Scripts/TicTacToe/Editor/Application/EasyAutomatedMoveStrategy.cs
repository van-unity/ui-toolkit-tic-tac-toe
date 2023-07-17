using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class EaseAutomatedMoveStrategy : IMoveStrategy {
        private readonly int _delayInMs;

        public EaseAutomatedMoveStrategy(int delayInMs) {
            _delayInMs = delayInMs;
        }

        public async Task ExecuteAsync(IPlayer player, BoardModel board, BoardPosition? clickPosition = null) {
            await Task.Delay(_delayInMs);

            var emptyCells = board.GetEmptyCells();
            if (emptyCells.Count <= 0) {
                return;
            }

            var chosenCell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
            board.UpdateCell(chosenCell, player.Symbol);
        }
    }
}