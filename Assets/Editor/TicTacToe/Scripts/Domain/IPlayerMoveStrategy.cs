using System;

namespace Editor.TicTacToe.Scripts.Domain {
    /// <summary>
    /// Defines the strategy for a player's move within the game of Tic Tac Toe.
    /// Implementations can encapsulate different algorithms or behaviors for making moves.
    /// </summary>
    public interface IPlayerMoveStrategy {
        void Move(Board board, Action<BoardPosition> onMoveCompleted);
        
        void Cancel();
    }
}