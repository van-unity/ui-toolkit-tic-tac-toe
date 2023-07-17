using System.Threading.Tasks;
using TicTacToe.Editor.Domain;

namespace TicTacToe.Editor.Application {
    public class Player : IPlayer {
        private readonly IMoveStrategy _moveStrategy;
        public Symbol Symbol { get; }
        public PlayerType PlayerType { get; }

        public Player(PlayerType playerType, Symbol symbol, IMoveStrategy moveStrategy) {
            PlayerType = playerType;
            Symbol = symbol;
            _moveStrategy = moveStrategy;
        }

        public Task TakeTurn(BoardModel board, BoardPosition clickPosition) =>
            _moveStrategy.ExecuteAsync(this, board, clickPosition);
    }
}