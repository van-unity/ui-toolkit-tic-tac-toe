using System;
using TicTacToe.Editor.Utils;
using UnityEngine;

namespace TicTacToe.Editor {
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

        private void OnPlayerPlayed(BoardPosition position, PlayerSymbol playerSymbol) {
            var winner = GetWinner(position);
            if (winner != null) {
                _board.DrawWinningLine(new BoardPosition(winner.Start.rowIndex, winner.Start.columnIndex),
                    new BoardPosition(winner.End.rowIndex, winner.End.columnIndex));
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

        private Win GetWinner(BoardPosition lastMovePos) {
            var size = _board.Rows; // assuming the board is square
            PlayerSymbol lastMoveSymbol = _board.ValueAt(lastMovePos);

            // Check the row of the last move
            bool rowIsUniform = true;
            for (int i = 0; i < size; i++) {
                if (_board.ValueAt(new BoardPosition(lastMovePos.rowIndex, i)) != lastMoveSymbol) {
                    rowIsUniform = false;
                    break;
                }
            }

            if (rowIsUniform) {
                return new Win(lastMoveSymbol, new BoardPosition(lastMovePos.rowIndex, 0),
                    new BoardPosition(lastMovePos.rowIndex, size - 1));
            }

            // Check the column of the last move
            bool colIsUniform = true;
            for (int i = 0; i < size; i++) {
                if (_board.ValueAt(new BoardPosition(i, lastMovePos.columnIndex)) != lastMoveSymbol) {
                    colIsUniform = false;
                    break;
                }
            }

            if (colIsUniform) {
                return new Win(lastMoveSymbol, new BoardPosition(0, lastMovePos.columnIndex),
                    new BoardPosition(size - 1, lastMovePos.columnIndex));
            }

            // Check the diagonals if the last move was on a diagonal
            bool onDiagonal1 = lastMovePos.rowIndex == lastMovePos.columnIndex;
            bool onDiagonal2 = lastMovePos.rowIndex == size - 1 - lastMovePos.columnIndex;
            bool diag1IsUniform = true;
            bool diag2IsUniform = true;
            for (int i = 0; i < size; i++) {
                if (onDiagonal1 && _board.ValueAt(new BoardPosition(i, i)) != lastMoveSymbol)
                    diag1IsUniform = false;
                if (onDiagonal2 && _board.ValueAt(new BoardPosition(i, size - 1 - i)) != lastMoveSymbol)
                    diag2IsUniform = false;
            }

            if ((onDiagonal1 && diag1IsUniform)) {
                return new Win(lastMoveSymbol, new BoardPosition(0, 0), new BoardPosition(size - 1, size - 1));
            }

            if ((onDiagonal2 && diag2IsUniform)) {
                return new Win(lastMoveSymbol, new BoardPosition(0, size - 1), new BoardPosition(size - 1, 0));
            }

            // No winner
            return null;
        }


        public bool IsBoardFull() {
            for (int i = 0; i < _board.Rows; i++) {
                for (int j = 0; j < _board.Columns; j++) {
                    if (_board.ValueAt(new BoardPosition(i, j)) == PlayerSymbol.None) {
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