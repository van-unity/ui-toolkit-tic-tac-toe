using System;
using System.Collections.Generic;

namespace TicTacToe.Editor.Domain {
    public class BoardModel {
        private readonly int _size;
        private  Symbol[,] _state;

        public event Action<BoardPosition, Symbol> CellUpdated;

        public BoardModel(int size) {
            _size = size;
            _state = new Symbol[_size, _size];
        }

        public void UpdateCell(BoardPosition position, Symbol symbol) {
            _state[position.RowIndex, position.ColumnIndex] = symbol;

            CellUpdated?.Invoke(position, symbol);
        }

        public bool IsMoveValid(BoardPosition position) =>
            _state[position.RowIndex, position.ColumnIndex] == Symbol.Empty;

        public bool IsFull() {
            for (int rowIndex = 0; rowIndex < _size; rowIndex++) {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++) {
                    if (_state[rowIndex, columnIndex] == Symbol.Empty) {
                        // Found an empty spot, so the board is not full
                        return false;
                    }
                }
            }

            // No empty spots were found, so the board is full
            return true;
        }

        public bool HasWinner(out Win win) {
            var winningPositions = new BoardPosition[_size];
            Symbol winnerSymbol;
            var currentPositionIndex = 0;

            //check rows
            for (int rowIndex = 0; rowIndex < _size; rowIndex++) {
                winnerSymbol = _state[rowIndex, 0];
                if (winnerSymbol == Symbol.Empty) {
                    break;
                }

                winningPositions[currentPositionIndex++] = new BoardPosition(rowIndex, 0);
                for (int columnIndex = 1; columnIndex < _size; columnIndex++) {
                    if (_state[rowIndex, columnIndex] == winnerSymbol) {
                        winningPositions[currentPositionIndex++] = new BoardPosition(rowIndex, columnIndex);
                    }
                }

                if (currentPositionIndex == _size) {
                    win = new Win(winnerSymbol, winningPositions);
                    return true;
                }

                currentPositionIndex = 0;
            }

            currentPositionIndex = 0;

            //check columns
            for (int columnIndex = 0; columnIndex < _size; columnIndex++) {
                winnerSymbol = _state[0, columnIndex];
                if (winnerSymbol == Symbol.Empty) {
                    break;
                }

                winningPositions[currentPositionIndex++] = new BoardPosition(0, columnIndex);
                for (int rowIndex = 1; rowIndex < _size; rowIndex++) {
                    if (_state[rowIndex, columnIndex] == winnerSymbol) {
                        winningPositions[currentPositionIndex++] = new BoardPosition(rowIndex, columnIndex);
                    }
                }

                if (currentPositionIndex == _size) {
                    win = new Win(winnerSymbol, winningPositions);
                    return true;
                }

                currentPositionIndex = 0;
            }

            currentPositionIndex = 0;

            //check diagonals
            winnerSymbol = _state[0, 0];
            if (winnerSymbol != Symbol.Empty) {
                winningPositions[currentPositionIndex++] = new BoardPosition(0, 0);
                for (int index = 1; index < _size; index++) {
                    if (_state[index, index] == winnerSymbol) {
                        winningPositions[currentPositionIndex++] = new BoardPosition(index, index);
                    }
                }

                if (currentPositionIndex == _size) {
                    win = new Win(winnerSymbol, winningPositions);
                    return true;
                }
            }

            currentPositionIndex = 0;
            winnerSymbol = _state[0, _size - 1];
            if (winnerSymbol != Symbol.Empty) {
                winningPositions[currentPositionIndex++] = new BoardPosition(0, _size - 1);
                for (int i = 1; i < _size; i++) {
                    var rowIndex = i;
                    var columnIndex = _size - 1 - i;
                    if (_state[rowIndex, columnIndex] == winnerSymbol) {
                        winningPositions[currentPositionIndex++] = new BoardPosition(rowIndex, columnIndex);
                    }
                }

                if (currentPositionIndex == _size) {
                    win = new Win(winnerSymbol, winningPositions);
                    return true;
                }
            }

            win = Win.Invalid();
            return false;
        }


        public List<BoardPosition> GetEmptyCells() {
            var emptyCells = new List<BoardPosition>();
            for (int rowIndex = 0; rowIndex < _size; rowIndex++) {
                for (int columnIndex = 0; columnIndex < _size; columnIndex++) {
                    if (_state[rowIndex, columnIndex] == Symbol.Empty) {
                        emptyCells.Add(new BoardPosition(rowIndex, columnIndex));
                    }
                }
            }

            return emptyCells;
        }

        public void Reset() {
            _state = new Symbol[_size, _size];
        }
    }
}