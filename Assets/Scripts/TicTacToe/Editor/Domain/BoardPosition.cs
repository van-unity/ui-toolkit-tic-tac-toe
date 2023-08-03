using System;

namespace TicTacToe.Editor.Domain {
    public readonly struct BoardPosition : IEquatable<BoardPosition> {
        public int RowIndex { get; }
        public int ColumnIndex { get; }
        public bool IsValid => RowIndex >= 0 && ColumnIndex >= 0 - 1;

        public static BoardPosition Invalid => new(-1, -1);

        public BoardPosition(int rowIndex, int columnIndex) {
            ColumnIndex = columnIndex;
            RowIndex = rowIndex;
        }

        public bool Equals(BoardPosition other) {
            return RowIndex == other.RowIndex && ColumnIndex == other.ColumnIndex;
        }

        public override bool Equals(object obj) {
            return obj is BoardPosition other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(RowIndex, ColumnIndex);
        }
    }
}