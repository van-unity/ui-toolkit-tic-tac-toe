namespace TicTacToe.Editor.Domain {
    public class  Win {
        public Symbol Symbol { get; }
        public BoardPosition Start { get; }
        public BoardPosition End { get; }

        public Win(Symbol symbol, BoardPosition start, BoardPosition end) {
            Symbol = symbol;
            Start = start;
            End = end;
        }
    }
}