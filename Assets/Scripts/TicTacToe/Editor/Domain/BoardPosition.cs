namespace TicTacToe.Editor.Domain {
    public readonly struct BoardPosition {
        public readonly int rowIndex;
        public readonly int columnIndex;

        public BoardPosition(int rowIndex, int columnIndex) {
            this.columnIndex = columnIndex;
            this.rowIndex = rowIndex;
        }

        public static BoardPosition Empty() => new BoardPosition(0, 0);
        public static BoardPosition NotValid() => new BoardPosition(-1, -1);

        public bool IsValid() => rowIndex != -1 && columnIndex != -1;

        public override string ToString() {
            return $"[{rowIndex}, {columnIndex}]";
        }
    }
}