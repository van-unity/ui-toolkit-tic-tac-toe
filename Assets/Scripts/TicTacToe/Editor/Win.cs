namespace TicTacToe.Editor {
    public class Win {
        public PlayerSymbol Symbol { get; }
        public BoardPosition Start { get; }
        public BoardPosition End { get; }

        public Win(PlayerSymbol symbol, BoardPosition start, BoardPosition end) {
            Symbol = symbol;
            Start = start;
            End = end;
        }
    }
}