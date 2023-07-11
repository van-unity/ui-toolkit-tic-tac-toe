namespace TicTacToe.Editor.Domain {
    public readonly struct PlayerMovedResult {
        public PlayerSymbol Symbol { get; }
        public BoardPosition MovePosition { get; }

        public PlayerMovedResult(PlayerSymbol symbol, BoardPosition position) {
            Symbol = symbol;
            MovePosition = position;
        }

        public override string ToString() {
            return $"Symbol: {Symbol}, Position: {MovePosition}";
        }
    }
}