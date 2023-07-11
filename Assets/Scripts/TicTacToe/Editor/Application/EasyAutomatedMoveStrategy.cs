using System;
using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class EaseAutomatedMoveStrategy : IPlayerMoveStrategy {
        private readonly int _delayInMs;

        public EaseAutomatedMoveStrategy(int delayInMs) {
            _delayInMs = delayInMs;
        }

        public async void Execute(BoardModel model, BoardPosition clickPosition, Action<BoardPosition> callback) {
            await Task.Delay(_delayInMs);
            
            for (int rowIndex = 0; rowIndex < model.Rows; rowIndex++) {
                for (int columnIndex = 0; columnIndex < model.Columns; columnIndex++) {
                    if (model.IsMoveValid(rowIndex, columnIndex)) {
                        callback(new BoardPosition(rowIndex, columnIndex));
                        return;
                    }
                }
            }


            callback(BoardPosition.NotValid());
        }
    }
}