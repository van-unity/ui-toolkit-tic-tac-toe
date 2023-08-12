using System;

namespace Editor.TicTacToe.Scripts.Domain {
    /// <summary>
    /// Defines events related important game lifecycle.
    /// </summary>
    public interface IGameEvents {
        event Action<PlayerSymbol> TurnChanged;
        event Action<Win> GameWon;
        event Action GameDraw;
        event Action GameStarted;
        event Action<PlayerSymbol, PlayerMode> PlayerModeChanged;
        event Action BeforeRestart;
    }
}