using System;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class Player : IPlayer {
        private readonly IPlayerMoveStrategy _moveStrategy;
        private readonly BoardModel _boardModel;
        public PlayerSymbol Symbol { get; }
        public PlayerType PlayerType { get; }

        public Player(PlayerType playerType, PlayerSymbol symbol, IPlayerMoveStrategy moveStrategy,
            BoardModel boardModel) {
            PlayerType = playerType;
            Symbol = symbol;
            _moveStrategy = moveStrategy;
            _boardModel = boardModel;
        }

        public void Move(BoardPosition clickPosition, Action<PlayerMovedResult> callback) {
            _moveStrategy.Execute(_boardModel, clickPosition,
                position => { callback.Invoke(new PlayerMovedResult(Symbol, position)); });
        }
    }
}