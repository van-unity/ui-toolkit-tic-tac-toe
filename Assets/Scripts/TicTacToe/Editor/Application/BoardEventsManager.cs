using System;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class BoardEventsManager : IBoardEventsProvider, IBoardEventsHandler {
        public event Action<BoardPosition> CellClicked;

        public void HandleCellClick(BoardPosition clickPos) {
            CellClicked?.Invoke(clickPos);
        }
    }
}