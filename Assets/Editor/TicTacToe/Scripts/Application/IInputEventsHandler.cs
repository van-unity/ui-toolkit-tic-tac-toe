using Editor.TicTacToe.Scripts.Domain;

namespace Editor.TicTacToe.Scripts.Application {
    /// <summary>
    /// Defines an interface for handling input events related to the game, such as cell clicks.
    /// </summary>
    public interface IInputEventsHandler {
        void HandleCellClick(BoardPosition clickPos);
    }
}