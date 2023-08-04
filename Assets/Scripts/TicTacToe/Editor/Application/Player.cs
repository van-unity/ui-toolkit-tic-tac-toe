using System;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class Player : IPlayer {
        private IMoveStrategy _moveStrategy;
        public Symbol Symbol { get; }
        public PlayerMode PlayerMode { get; private set; }

        public Player(PlayerMode playerMode, Symbol symbol, IMoveStrategy moveStrategy) {
            PlayerMode = playerMode;
            Symbol = symbol;
            _moveStrategy = moveStrategy;
        }

        public void SetMode(PlayerMode mode) {
            PlayerMode = mode;
        }

        public void SetStrategy(IMoveStrategy strategy) {
            _moveStrategy = strategy;
        }
        
        public void MakeMove(Board board, Action<BoardPosition> callback) {
            _moveStrategy.Move(board, callback);
        }
    }
}