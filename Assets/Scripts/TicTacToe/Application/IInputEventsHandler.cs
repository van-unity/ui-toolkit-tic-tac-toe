using TicTacToe.Domain;

namespace TicTacToe.Application {
    /// <summary>
    /// Defines an interface for handling input events related to the game, such as cell clicks.
    /// </summary>
    public interface IInputEventsHandler {
        void HandleCellClick(BoardPosition clickPos);
    }
}