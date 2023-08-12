using System;
using System.Collections.Generic;

namespace TicTacToe.Domain {
    /// <summary>
    /// Represents the game board for Tic Tac Toe, including its size, state, and empty cells.
    /// Provides methods to initialize the board, update individual cells, validate moves, check for a winner, and reset the board.
    /// The board uses a two-dimensional array to maintain the state of the game, with support for an event to notify when a cell is updated.
    /// </summary>
    public class Board {
        private readonly int _size;
        private readonly PlayerSymbol[,] _state;
        private readonly List<BoardPosition> _emptyCells;

        public IReadOnlyList<BoardPosition> EmptyCells => _emptyCells;
        
        public bool IsFull => _emptyCells.Count == 0;

        public event Action<BoardPosition, PlayerSymbol> CellUpdated;

        public Board(int size) {
            _size = size;
            _state = new PlayerSymbol[_size, _size];
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

        public void UpdateCell(BoardPosition position, PlayerSymbol playerSymbol) {
            if (position == BoardPosition.Invalid) {
                return;
            }

            _state[position.RowIndex, position.ColumnIndex] = playerSymbol;

            if (playerSymbol != PlayerSymbol.Empty) {
                _emptyCells.Remove(position);
            }

            CellUpdated?.Invoke(position, playerSymbol);
        }

        public bool IsMoveValid(BoardPosition movePosition) => IsPositionValid(movePosition) &&
                                                               _state[movePosition.RowIndex,
                                                                   movePosition.ColumnIndex] == PlayerSymbol.Empty;

        private bool IsPositionValid(BoardPosition position) =>
            position.RowIndex >= 0 && position.ColumnIndex >= 0 &&
            position.RowIndex < _size && position.ColumnIndex < _size;


        public bool TryGetWinner(out Win win) {
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

            //check diagonal starting from top-left
            if (CheckLine(0, 0, (r, c) => new BoardPosition(r + 1, c + 1), out win)) {
                return true;
            }

            //check diagonal starting from bottom-left
            if (CheckLine(_size - 1, 0, (r, c) => new BoardPosition(r - 1, c + 1), out win)) {
                return true;
            }

            win = Win.Invalid;
            return false;
        }

        private bool CheckLine(int startRow, int startColumn, Func<int, int, BoardPosition> getNext, out Win win) {
            var winningPositions = new BoardPosition[_size];
            var winningSymbol = _state[startRow, startColumn];
            if (winningSymbol == PlayerSymbol.Empty) {
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
                    _state[rowIndex, columnIndex] = PlayerSymbol.Empty;
                }
            }

            _emptyCells.Clear();
            InitializeEmptyCellsList();
        }
    }
}