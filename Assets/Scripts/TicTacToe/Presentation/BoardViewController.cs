using System.Threading.Tasks;
using TicTacToe.Application;
using TicTacToe.Domain;

namespace TicTacToe.Presentation {
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

            _view.Opened += OnViewOpened;
        }

        private void OnViewOpened() {
            _board.CellUpdated += OnCellUpdated;
            _gameEvents.GameWon += OnGameWon;
            _gameEvents.BeforeRestart += OnBeforeRestart;
            _view.CellClicked += OnCellClicked;
            _view.Closed += OnViewClosed;
            WaitAndInitializeTheView();
        }

        private async void WaitAndInitializeTheView() {
            await Task.Delay(_viewSettings.BoardDrawDelayMS);
            _view?.Initialize();
        }
        
        private void OnBeforeRestart() {
            _view.Reset();
        }

        private void OnCellClicked(BoardPosition clickPosition) {
            _inputEventsHandler.HandleCellClick(clickPosition);
        }

        private void OnCellUpdated(BoardPosition position, PlayerSymbol playerSymbol) {
            _view.UpdateCell(position, playerSymbol);
        }

        private void OnGameWon(Win win) {
            _view.DrawWinningLine(win.WinPositions[0], win.WinPositions[^1]);
        }

        private void OnViewClosed() {
            _board.CellUpdated -= OnCellUpdated;
            _gameEvents.GameWon -= OnGameWon;
            _gameEvents.BeforeRestart -= OnBeforeRestart;
            _view.CellClicked -= OnCellClicked;
            _view.Opened -= OnViewOpened;
            _view.Closed -= OnViewClosed;
        }
    }
}