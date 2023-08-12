using System;
using System.Threading;
using System.Threading.Tasks;
using Editor.TicTacToe.Scripts.Domain;
using UnityEngine;

namespace Editor.TicTacToe.Scripts.Application {
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

                var chosenCell = emptyCells[UnityEngine.Random.Range(0, emptyCells.Count)];
                onMoveCompleted(chosenCell);
            }
            catch (TaskCanceledException canceledException) {
                //pass
            }
            catch (Exception exception) {
                Debug.LogError(exception);
            }
        }

        public void Cancel() {
            _moveCancellation.Cancel();
            _moveCancellation.Dispose();
        }
    }
}