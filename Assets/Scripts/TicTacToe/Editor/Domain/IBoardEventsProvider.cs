using System;

namespace TicTacToe.Editor.Domain {
    public interface IBoardEventsProvider {
        event Action<BoardPosition> CellClicked;
    }
}