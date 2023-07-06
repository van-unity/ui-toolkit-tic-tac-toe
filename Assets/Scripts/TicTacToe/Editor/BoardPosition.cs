namespace TicTacToe.Editor {
    public readonly struct BoardPosition {
        public readonly int rowIndex;
        public readonly int columnIndex;

        public BoardPosition(int rowIndex, int columnIndex) {
            this.columnIndex = columnIndex;
            this.rowIndex = rowIndex;
        }
    }
}