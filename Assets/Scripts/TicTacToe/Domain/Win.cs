using System.Collections.Generic;

namespace TicTacToe.Domain {
    /// <summary>
    /// Represents the result of a win.
    /// Contains the winning symbol and the positions that formed the win.
    /// Provides an instance of an invalid win, which can be used to represent no win.
    /// </summary>
    public readonly struct Win {
        private readonly BoardPosition[] _winPositions;
        
        public PlayerSymbol Symbol { get; }
        
        public IReadOnlyList<BoardPosition> WinPositions => _winPositions;

        public static Win Invalid { get; }

        static Win() {
            Invalid = new Win(PlayerSymbol.Empty, null);
        }

        public Win(PlayerSymbol symbol, BoardPosition[] winPositions) {
            Symbol = symbol;
            _winPositions = (BoardPosition[])winPositions?.Clone();
        }
    }
}