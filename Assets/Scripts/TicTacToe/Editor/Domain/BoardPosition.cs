namespace TicTacToe.Editor.Domain {
    public readonly struct BoardPosition {
        public int RowIndex { get; }
        public int ColumnIndex { get; }

        public BoardPosition(int rowIndex, int columnIndex) {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }

        public override string ToString() => $"[{RowIndex}, {ColumnIndex}]";
        
        public static BoardPosition Invalid() => new(-1, -1);
    }
}