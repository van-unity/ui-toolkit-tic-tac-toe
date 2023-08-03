using System;
using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class DelayedRandomMoveStrategy : IMoveStrategy {
        private readonly int _delayMS;

        public DelayedRandomMoveStrategy(int delayMS) {
            _delayMS = delayMS;
        }
        
        public async void Move(Board board, Action<BoardPosition> callback) {
            await Task.Delay(_delayMS);
            
            var emptyCells = board.EmptyCells;
            if (emptyCells.Count <= 0) {
                callback(BoardPosition.Invalid);
                return;
            }

            var chosenCell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
            callback(chosenCell);
        }
    }
}