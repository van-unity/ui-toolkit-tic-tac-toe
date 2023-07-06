using TicTacToe.Editor.Presentation;

namespace TicTacToe.Editor {
    public class BoardController {
        private readonly BoardModel _model;
        private readonly BoardView _view;

        public BoardController(BoardModel model, BoardView view) {
            _model = model;
            _view = view;
        }
        
        public void PlaceSymbolAt(PlayerSymbol symbol, BoardPosition position) {
            _model.PlaceSymbolAt(symbol, position);
            _view.UpdateCell(position, symbol);
        }
    }
}