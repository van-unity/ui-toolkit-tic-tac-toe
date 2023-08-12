using System;
using Editor.TicTacToe.Scripts.Domain;

namespace Editor.TicTacToe.Scripts.Application {
    /// <summary>
    /// Defines an interface for broadcasting input events, such as cell click events.
    /// </summary>
    public interface IInputEvents {
        event Action<BoardPosition> CellClicked;
    }
}