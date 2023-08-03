using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class BoardViewController {
        private readonly BoardView _view;
        private readonly BoardModel _boardModel;
        private readonly IBoardEventsHandler _boardEventsHandler;
        private readonly IGameEventsProvider _gameEvents;

        public BoardViewController(BoardView view, BoardModel boardModel, IBoardEventsHandler boardEventsHandler,
            IGameEventsProvider gameEvents) {
            _view = view;
            _boardModel = boardModel;
            _boardEventsHandler = boardEventsHandler;
            _gameEvents = gameEvents;

            _view.RegisterCallback<AttachToPanelEvent>(ViewOpened);
            _view.RegisterCallback<CellClickedEvent>(OnCellClicked);
            _view.RegisterCallback<DetachFromPanelEvent>(ViewClosed);
        }

        private void ViewOpened(AttachToPanelEvent evt) {
            _boardModel.CellUpdated += OnCellUpdated;
            _gameEvents.GameWon += OnGameWon;
        }

        private void OnCellClicked(CellClickedEvent clickEvent) {
            _boardEventsHandler.HandleCellClick(clickEvent.ClickPosition);
        }

        private void OnCellUpdated(BoardPosition position, Symbol symbol) {
            _view.UpdateCell(position, symbol);
        }

        private void OnGameWon(Win win) {
            if (win.IsValid()) {
                _view.DrawWinningLine(win.Positions[0], win.Positions[^1]);
            }
        }

        private void ViewClosed(DetachFromPanelEvent evt) {
            _boardModel.CellUpdated -= OnCellUpdated;
            _gameEvents.GameWon -= OnGameWon;
            _view.UnregisterCallback<DetachFromPanelEvent>(ViewClosed);
            _view.UnregisterCallback<AttachToPanelEvent>(ViewOpened);
            _view.UnregisterCallback<CellClickedEvent>(OnCellClicked);
        }
    }
}