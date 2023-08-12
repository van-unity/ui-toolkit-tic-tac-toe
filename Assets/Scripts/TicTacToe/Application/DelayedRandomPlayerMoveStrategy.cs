using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using TicTacToe.Domain;

namespace TicTacToe.Application {
    /// <summary>
    /// Represents a move strategy for an automated player that chooses a random move after a specified delay.
    /// </summary>
    public class DelayedRandomPlayerMoveStrategy : IPlayerMoveStrategy {
        private readonly int _delayMS;
        private CancellationTokenSource _moveCancellation;

        public DelayedRandomPlayerMoveStrategy(int delayMS) {
            _delayMS = delayMS;
        }

        public async void Move(Board board, Action<BoardPosition> onMoveCompleted) {
            _moveCancellation?.Dispose();
            _moveCancellation = new CancellationTokenSource();

            try {
                await Task.Delay(_delayMS, _moveCancellation.Token);
                if (_moveCancellation.IsCancellationRequested) {
                    return;
                }

                var emptyCells = board.EmptyCells;
                if (emptyCells.Count <= 0) {
                    onMoveCompleted(BoardPosition.Invalid);
                    return;
                }

                var chosenCell = ChooseCell(emptyCells);
                onMoveCompleted(chosenCell);
            }
            catch (TaskCanceledException canceledException) {
                //pass
            }
            catch (Exception e) {
                // handle exceptions 
            }
        }

        private BoardPosition ChooseCell(IReadOnlyList<BoardPosition> emptyCells) {
            var rnd = new Random();
            var randomIndex = rnd.Next(0, emptyCells.Count);

            return emptyCells[randomIndex];
        }

        public void Cancel() {
            _moveCancellation.Cancel();
            _moveCancellation.Dispose();
        }
    }
}