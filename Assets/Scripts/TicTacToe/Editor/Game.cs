using System;
using TicTacToe.Editor.Utils;
using UnityEngine;

namespace TicTacToe.Editor {
    public class Win {
        public PlayerSymbol Symbol { get; }
        public (int row, int col) Start { get; }
        public (int row, int col) End { get; }

        public Win(PlayerSymbol symbol, (int row, int col) start, (int row, int col) end) {
            Symbol = symbol;
            Start = start;
            End = end;
        }
    }

    public class Game : IDisposable {
        private readonly Player _playerX;
        private readonly Player _playerO;
        private readonly Board _board;

        public Game(Player playerX, Player playerO, Board board) {
            _playerX = playerX;
            _playerO = playerO;
            _board = board;
            _playerX.MoveMade += OnPlayerPlayed;
            _playerO.MoveMade += OnPlayerPlayed;
            ;
        }

        public void Run() {
            _playerX.Play();
        }

        private void OnPlayerPlayed(int rowIndex, int columnIndex, PlayerSymbol playerSymbol) {
            var winner = GetWinner(rowIndex, columnIndex);
            if (winner != null) {
                _board.DrawWinningLine(new Vector2(winner.Start.row, winner.Start.col),
                    new Vector2(winner.End.row, winner.End.col));
                return;
            }

            if (IsBoardFull()) {
                return;
            }

            switch (playerSymbol) {
                case PlayerSymbol.X:
                    _playerO.Play();
                    break;
                case PlayerSymbol.O:
                    _playerX.Play();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerSymbol), playerSymbol, null);
            }
        }

        private Win GetWinner(int lastMoveRow, int lastMoveCol) {
            var size = _board.Rows; // assuming the board is square
            PlayerSymbol lastMoveSymbol = _board.ValueAt(lastMoveRow, lastMoveCol);

            // Check the row of the last move
            bool rowIsUniform = true;
            for (int i = 0; i < size; i++) {
                if (_board.ValueAt(lastMoveRow, i) != lastMoveSymbol) {
                    rowIsUniform = false;
                    break;
                }
            }

            if (rowIsUniform) {
                return new Win(lastMoveSymbol, (lastMoveRow, 0), (lastMoveRow, size - 1));
            }

            // Check the column of the last move
            bool colIsUniform = true;
            for (int i = 0; i < size; i++) {
                if (_board.ValueAt(i, lastMoveCol) != lastMoveSymbol) {
                    colIsUniform = false;
                    break;
                }
            }

            if (colIsUniform) {
                return new Win(lastMoveSymbol, (0, lastMoveCol), (size - 1, lastMoveCol));
            }

            // Check the diagonals if the last move was on a diagonal
            bool onDiagonal1 = lastMoveRow == lastMoveCol;
            bool onDiagonal2 = lastMoveRow == size - 1 - lastMoveCol;
            bool diag1IsUniform = true;
            bool diag2IsUniform = true;
            for (int i = 0; i < size; i++) {
                if (onDiagonal1 && _board.ValueAt(i, i) != lastMoveSymbol)
                    diag1IsUniform = false;
                if (onDiagonal2 && _board.ValueAt(i, size - 1 - i) != lastMoveSymbol)
                    diag2IsUniform = false;
            }

            if ((onDiagonal1 && diag1IsUniform)) {
                return new Win(lastMoveSymbol, (0, 0), (size - 1, size - 1));
            }

            if ((onDiagonal2 && diag2IsUniform)) {
                return new Win(lastMoveSymbol, (0, size - 1), (size - 1, 0));
            }

            // No winner
            return null;
        }


        public bool IsBoardFull() {
            for (int i = 0; i < _board.Rows; i++) {
                for (int j = 0; j < _board.Columns; j++) {
                    if (_board.ValueAt(i, j) == PlayerSymbol.None) {
                        // Found an empty spot, so the board is not full
                        return false;
                    }
                }
            }

            // No empty spots were found, so the board is full
            return true;
        }

        public void Dispose() {
            _playerX.MoveMade -= OnPlayerPlayed;
            _playerO.MoveMade -= OnPlayerPlayed;
        }
    }
}