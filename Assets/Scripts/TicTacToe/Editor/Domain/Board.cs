using System;
using System.Collections.Generic;

namespace TicTacToe.Editor.Domain {
    public class Board {
        private readonly int _size;
        private readonly Symbol[,] _state;
        private readonly List<BoardPosition> _emptyCells;

        public IReadOnlyList<BoardPosition> EmptyCells => _emptyCells;
        public bool IsFull => _emptyCells.Count == 0;
        public event Action<BoardPosition, Symbol> CellUpdated;

        public Board(int size) {
            _size = size;
            _state = new Symbol[_size, _size];
            _emptyCells = new List<BoardPosition>(_size * _size);
            InitializeEmptyCellsList();
        }

        private void InitializeEmptyCellsList() {
            for (int rowIndex = 0; rowIndex < _size; rowIndex++) {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++) {
                    _emptyCells.Add(new BoardPosition(rowIndex, columnIndex));
                }
            }
        }

        public void UpdateCell(BoardPosition position, Symbol symbol) {
            _state[position.RowIndex, position.ColumnIndex] = symbol;

            if (symbol != Symbol.Empty) {
                _emptyCells.Remove(position);
            }

            CellUpdated?.Invoke(position, symbol);
        }

        public bool IsMoveValid(BoardPosition movePosition) => IsPositionValid(movePosition) &&
                                                               _state[movePosition.RowIndex,
                                                                   movePosition.ColumnIndex] == Symbol.Empty;

        private bool IsPositionValid(BoardPosition position) =>
            position.IsValid && position.RowIndex < _size && position.ColumnIndex < _size;


        public bool HasWinner(out Win win) {
            //check rows
            for (int rowIndex = 0; rowIndex < _size; rowIndex++) {
                if (CheckLine(rowIndex, 0, (r, c) => new BoardPosition(r, c + 1), out win)) {
                    return true;
                }
            }

            //check columns
            for (int columnIndex = 0; columnIndex < _size; columnIndex++) {
                if (CheckLine(0, columnIndex, (r, c) => new BoardPosition(r + 1, c), out win)) {
                    return true;
                }
            }

            //check diagonal from [0, 0]
            if (CheckLine(0, 0, (r, c) => new BoardPosition(r + 1, c + 1), out win)) {
                return true;
            }

            //check diagonal from [size - 1, 0]
            if (CheckLine(_size - 1, 0, (r, c) => new BoardPosition(r - 1, c + 1), out win)) {
                return true;
            }

            win = Win.Invalid;
            return false;
        }

        private bool CheckLine(int startRow, int startColumn, Func<int, int, BoardPosition> getNext, out Win win) {
            var winningPositions = new BoardPosition[_size];
            var winningSymbol = _state[startRow, startColumn];
            if (winningSymbol == Symbol.Empty) {
                win = Win.Invalid;
                return false;
            }

            var positionIndex = 0;
            winningPositions[positionIndex] = new BoardPosition(startRow, startColumn);

            var nextPosition = getNext(startRow, startColumn);
            while (IsPositionValid(nextPosition)) {
                if (_state[nextPosition.RowIndex, nextPosition.ColumnIndex] != winningSymbol) {
                    win = Win.Invalid;
                    return false;
                }

                winningPositions[++positionIndex] = nextPosition;
                nextPosition = getNext(nextPosition.RowIndex, nextPosition.ColumnIndex);
            }

            win = new Win(winningSymbol, winningPositions);
            return true;
        }

        public void Reset() {
            for (int rowIndex = 0; rowIndex < _size; rowIndex++) {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++) {
                    _state[rowIndex, columnIndex] = Symbol.Empty;
                }
            }

            _emptyCells.Clear();
            InitializeEmptyCellsList();
        }
    }
}