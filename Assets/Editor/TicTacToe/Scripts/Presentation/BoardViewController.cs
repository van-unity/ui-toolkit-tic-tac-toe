using Editor.TicTacToe.Scripts.Application;
using Editor.TicTacToe.Scripts.Domain;
using Editor.TicTacToe.Scripts.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class BoardViewController {
        private readonly BoardView _view;
        private readonly Board _board;
        private readonly IInputEventsHandler _inputEventsHandler;
        private readonly IGameEvents _gameEvents;
        private readonly IViewSettings _viewSettings;

        public BoardViewController(BoardView view, Board board, IInputEventsHandler inputEventsHandler,
            IGameEvents gameEvents, IViewSettings viewSettings) {
            _view = view;
            _board = board;
            _inputEventsHandler = inputEventsHandler;
            _gameEvents = gameEvents;
            _viewSettings = viewSettings;

            _view.RegisterCallback<AttachToPanelEvent>(ViewOpened);
            _view.RegisterCallback<CellClickedEvent>(OnCellClicked);
            _view.RegisterCallback<DetachFromPanelEvent>(ViewClosed);
        }

        private void ViewOpened(AttachToPanelEvent evt) {
            _board.CellUpdated += OnCellUpdated;
            _gameEvents.GameWon += OnGameWon;
            _gameEvents.BeforeRestart += OnBeforeRestart;
            _view.schedule
                .Execute(() => { _view.Initialize(); })
                .ExecuteLater(_viewSettings.BoardDrawDelayMS);
        }

        private void OnBeforeRestart() {
            _view.Reset();
        }

        private void OnCellClicked(CellClickedEvent clickEvent) {
            _inputEventsHandler.HandleCellClick(clickEvent.ClickPosition);
        }

        private void OnCellUpdated(BoardPosition position, PlayerSymbol playerSymbol) {
            _view.UpdateCell(position, playerSymbol);
        }

        private void OnGameWon(Win win) {
            _view.DrawWinningLine(win.WinPositions[0], win.WinPositions[^1]);
        }

        private void ViewClosed(DetachFromPanelEvent evt) {
            _board.CellUpdated -= OnCellUpdated;
            _gameEvents.GameWon -= OnGameWon;
            _gameEvents.BeforeRestart -= OnBeforeRestart;
            _view.UnregisterCallback<DetachFromPanelEvent>(ViewClosed);
            _view.UnregisterCallback<AttachToPanelEvent>(ViewOpened);
            _view.UnregisterCallback<CellClickedEvent>(OnCellClicked);
        }
    }
}