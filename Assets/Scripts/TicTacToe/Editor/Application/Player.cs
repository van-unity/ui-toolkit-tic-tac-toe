using System;
using System.Threading.Tasks;
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
        
        public void MakeMove(BoardModel boardModel, Action<BoardPosition> callback) {
            _moveStrategy.Play(boardModel, callback);
        }
    }
}