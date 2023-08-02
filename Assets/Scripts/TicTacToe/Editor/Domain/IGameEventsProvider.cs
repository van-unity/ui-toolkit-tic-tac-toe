using System;

namespace TicTacToe.Editor.Domain {
    public interface IGameEventsProvider {
        event Action<Symbol> TurnChanged;
        event Action<Win> GameWon;
        event Action GameDraw;
        event Action GameStarted;
        event Action<Symbol, PlayerMode> PlayerModeChanged;
    }
}