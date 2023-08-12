using System;
using TicTacToe.Domain;

namespace TicTacToe.Application {
    ///<summary>
    /// The InputManager class is responsible for managing player input events within the game.
    /// It implements both the IInputEvents and IInputEventsHandler interfaces to handle
    /// and propagate cell click events.
    /// </summary>
    public class InputManager : IInputEvents, IInputEventsHandler {
        public event Action<BoardPosition> CellClicked;

        public void HandleCellClick(BoardPosition clickPos) {
            CellClicked?.Invoke(clickPos);
        }
    }
}