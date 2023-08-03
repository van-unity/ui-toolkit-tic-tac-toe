using System;
using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class EaseAutomatedMoveStrategy : IMoveStrategy {
        private readonly int _delayMS;

        public EaseAutomatedMoveStrategy(int delayMS) {
            _delayMS = delayMS;
        }
        
        public async void Play(Board board, Action<BoardPosition> callback) {
            await Task.Delay(_delayMS);
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