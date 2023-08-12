using System;
using TicTacToe.Domain;

namespace TicTacToe.Application {
    /// <summary>
    /// Represents a player in the game of Tic-Tac-Toe, encapsulating the behavior of both manual and automated players.
    /// </summary>
    public class Player : IPlayer {
        private IPlayerMoveStrategy _moveStrategy;
        
        public PlayerMode PlayerMode { get; private set; }

        public Player(PlayerMode playerMode, IPlayerMoveStrategy moveStrategy) {
            _moveStrategy = moveStrategy;
            PlayerMode = playerMode;
        }

        public void SetMode(PlayerMode mode) {
            PlayerMode = mode;
        }

        public void SetStrategy(IPlayerMoveStrategy strategy) {
            _moveStrategy = strategy;
        }

        public void MakeMove(Board board, Action<BoardPosition> callback) {
            _moveStrategy.Move(board, callback);
        }

        public void CancelMove() {
            _moveStrategy.Cancel();
        }
    }
}