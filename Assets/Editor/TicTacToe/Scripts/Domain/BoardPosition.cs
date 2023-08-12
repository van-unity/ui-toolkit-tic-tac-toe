using System;

namespace Editor.TicTacToe.Scripts.Domain {
    /// <summary>
    /// Represents a position on the board by row and column indexes.
    /// Provides an instance of an invalid position.
    /// The invalid position will represent non-existing or wrong move.
    /// </summary>
    public readonly struct BoardPosition : IEquatable<BoardPosition> {
        public int RowIndex { get; }
        
        public int ColumnIndex { get; }

        public static BoardPosition Invalid { get; }

        static BoardPosition() {
            Invalid = new BoardPosition(-1, -1);
        }

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

        public static bool operator ==(BoardPosition left, BoardPosition right) {
            return left.Equals(right);
        }

        public static bool operator !=(BoardPosition left, BoardPosition right) {
            return !left.Equals(right);
        }


        public override int GetHashCode() {
            return HashCode.Combine(RowIndex, ColumnIndex);
        }
    }
}