namespace TicTacToe.Editor.Domain {
    public interface IBoardEventsHandler {
        void HandleCellClick(BoardPosition clickPos);
    }
}