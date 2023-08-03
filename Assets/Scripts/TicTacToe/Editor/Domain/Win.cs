namespace TicTacToe.Editor.Domain {
    public readonly struct Win {
        public Symbol Symbol { get; }
        public BoardPosition[] Positions { get; }

        public bool IsValid => Symbol != Symbol.Empty && Positions is { Length: > 0 };
        
        public static Win Invalid => new(Symbol.Empty, null);

        public Win(Symbol symbol, BoardPosition[] positions) {
            Symbol = symbol;
            Positions = positions;
        }

    }
}