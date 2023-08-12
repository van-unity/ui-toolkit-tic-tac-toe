using System;
using TicTacToe.Domain;

namespace TicTacToe.Application {
    /// <summary>
    /// Defines an interface for broadcasting input events, such as cell click events.
    /// </summary>
    public interface IInputEvents {
        event Action<BoardPosition> CellClicked;
    }
}