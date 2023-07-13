using System;
using System.Collections.Generic;

namespace TicTacToe.Editor.Domain {
    public class BoardModel {
        private readonly Symbol[,] _state;
        private readonly int _rows;
        private readonly int _columns;

        public event Action<BoardPosition, Symbol> CellUpdated; 

        public BoardModel(int rows, int columns) {
            _rows = rows;
            _columns = columns;
            _state = new Symbol[_rows, _columns];
        }

        public void UpdateCell(BoardPosition position, Symbol symbol) {
            _state[position.rowIndex, position.columnIndex] = symbol;
            
            CellUpdated?.Invoke(position, symbol);
        }
        
        public Symbol GetValueAt(int rowIndex, int columnIndex) => _state[rowIndex, columnIndex];
        public Symbol GetValueAt(BoardPosition position) => _state[position.rowIndex, position.columnIndex];

        public bool IsMoveValid(int rowIndex, int columnIndex) => _state[rowIndex, columnIndex] == Symbol.None;

        public bool IsMoveValid(BoardPosition position) =>
            _state[position.rowIndex, position.columnIndex] == Symbol.None;

        public bool IsBoardFull() {
            for (int rowIndex = 0; rowIndex < _rows; rowIndex++) {
                for (int columnIndex = 0; columnIndex < _columns; columnIndex++) {
                    if (_state[rowIndex, columnIndex] == Symbol.None) {
                        // Found an empty spot, so the board is not full
                        return false;
                    }
                }
            }

            // No empty spots were found, so the board is full
            return true;
        }

        public bool HasWinner(BoardPosition lastMovePos, out Win win) {
            Symbol lastMoveSymbol = _state[lastMovePos.rowIndex, lastMovePos.columnIndex];

            // Check the row of the last move
            var rowIsUniform = true;
            for (int i = 0; i < _rows; i++) {
                if (_state[lastMovePos.rowIndex, i] == lastMoveSymbol) {
                    continue;
                }

                rowIsUniform = false;
                break;
            }

            if (rowIsUniform) {
                win = new Win(lastMoveSymbol, new BoardPosition(lastMovePos.rowIndex, 0),
                    new BoardPosition(lastMovePos.rowIndex, _rows - 1));
                return true;
            }

            // Check the column of the last move
            var colIsUniform = true;
            for (int i = 0; i < _rows; i++) {
                if (_state[i, lastMovePos.columnIndex] == lastMoveSymbol) {
                    continue;
                }

                colIsUniform = false;
                break;
            }

            if (colIsUniform) {
                win = new Win(lastMoveSymbol, new BoardPosition(0, lastMovePos.columnIndex),
                    new BoardPosition(_rows - 1, lastMovePos.columnIndex));
                return true;
            }

            // Check the diagonals if the last move was on a diagonal
            var onDiagonal1 = lastMovePos.rowIndex == lastMovePos.columnIndex;
            var onDiagonal2 = lastMovePos.rowIndex == _rows - 1 - lastMovePos.columnIndex;
            var diag1IsUniform = true;
            var diag2IsUniform = true;
            for (int i = 0; i < _rows; i++) {
                if (onDiagonal1 && _state[i, i] != lastMoveSymbol)
                    diag1IsUniform = false;
                if (onDiagonal2 && _state[i, _rows - 1 - i] != lastMoveSymbol)
                    diag2IsUniform = false;
            }

            if (onDiagonal1 && diag1IsUniform) {
                win = new Win(lastMoveSymbol, new BoardPosition(0, 0), new BoardPosition(_rows - 1, _rows - 1));
                return true;
            }

            if (onDiagonal2 && diag2IsUniform) {
                win = new Win(lastMoveSymbol, new BoardPosition(0, _rows - 1), new BoardPosition(_rows - 1, 0));
                return true;
            }

            // No winner
            win = null;
            return false;
        }

        public List<BoardPosition> GetEmptyCells() {
            var emptyCells = new List<BoardPosition>();
            for (int rowIndex = 0; rowIndex < _rows; rowIndex++) {
                for (int columnIndex = 0; columnIndex < _columns; columnIndex++) {
                    if (_state[rowIndex, columnIndex] == Symbol.None) {
                        emptyCells.Add(new BoardPosition(rowIndex, columnIndex));
                    }
                }
            }

            return emptyCells;
        }
    }
}