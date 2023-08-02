using System;
using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class EaseAutomatedMoveStrategy : IMoveStrategy {
        public async void Play(BoardModel board, Action<BoardPosition> callback) {
            await Task.Delay(1000);
            var emptyCells = board.GetEmptyCells();
            if (emptyCells.Count <= 0) {
                callback(BoardPosition.Invalid());
                return;
            }

            var chosenCell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
            callback(chosenCell);
        }
    }
}