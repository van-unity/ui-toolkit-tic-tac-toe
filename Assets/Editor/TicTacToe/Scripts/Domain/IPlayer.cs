using System;

namespace Editor.TicTacToe.Scripts.Domain {
    /// <summary>
    /// Defines the behavior of a player in the Tic Tac Toe game.
    /// Allows setting the player's mode (human/computer) and move strategy,
    /// as well as handling move actions.
    /// </summary>
    public interface IPlayer {
        PlayerMode PlayerMode { get; }
        void SetMode(PlayerMode mode);
        void SetStrategy(IPlayerMoveStrategy strategy);
        void MakeMove(Board model, Action<BoardPosition> callback = null);
        void CancelMove();
    }
}