using System.Threading.Tasks;
using TicTacToe.Editor.Domain;
using TicTacToe.Editor.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace TicTacToe.Editor.Presentation {
    public class GameScreenController {
        private const string TURN_TEXT_FORMAT = "{0} Turn";
        private const string PLAYER_TYPE_FORMAT = "{0}";

        private readonly GameScreen _view;
        private readonly IGameEventsProvider _gameEvents;
        private readonly IGameController _gameController;
        private readonly IGameSettings _gameSettings;

        public GameScreenController(GameScreen view, IGameEventsProvider gameEvents, IGameController gameController,
            IGameSettings gameSettings) {
            _view = view;
            _gameEvents = gameEvents;
            _gameController = gameController;
            _gameSettings = gameSettings;

            _view.RegisterCallback<AttachToPanelEvent>(ViewOpened);
            _view.RegisterCallback<DetachFromPanelEvent>(ViewClosed);
            _view.RegisterCallback<StartButtonClickEvent>(OnStartButtonClick);
            _view.RegisterCallback<RestartButtonClicked>(OnRestartButtonClick);
            _view.RegisterCallback<PlayerModeClickedEvent>(OnPlayerModeClicked);
        }

        private void OnPlayerModeClicked(PlayerModeClickedEvent evt) {
            _gameController.TogglePlayerMode(evt.Symbol);
        }

        private void OnRestartButtonClick(RestartButtonClicked evt) {
            _gameController.Restart();
        }

        private void ViewOpened(AttachToPanelEvent evt) {
            InitializeTheView();
            SubscribeOnGameEvents();
            WaitAndDrawTheBoard();
        }

        private void SubscribeOnGameEvents() {
            _gameEvents.GameStarted += OnGameStarted;
            _gameEvents.TurnChanged += OnTurnChanged;
            _gameEvents.PlayerModeChanged += OnPlayerModeChanged;
            _gameEvents.BeforeRestart += OnBeforeRestart;
        }

        private void OnBeforeRestart() {
            _view.ClearTheBoard();
        }

        private void InitializeTheView() {
            _view.SetStartButtonEnabled(true);
            _view.UpdateTurnLabel(string.Empty);
            _view.SetPlayerXMode(string.Format(PLAYER_TYPE_FORMAT, _gameSettings.PlayerXMode));
            _view.SetPlayerOMode(string.Format(PLAYER_TYPE_FORMAT, _gameSettings.PlayerOMode));
        }

        private void OnPlayerModeChanged(Symbol playerSymbol, PlayerMode playerMode) {
            if (playerSymbol == Symbol.X) {
                _view.SetPlayerXMode(string.Format(PLAYER_TYPE_FORMAT, playerMode));
                return;
            }

            _view.SetPlayerOMode(string.Format(PLAYER_TYPE_FORMAT, playerMode));
        }

        private async void WaitAndDrawTheBoard() {
            await Task.Delay(_gameSettings.BoardDrawDelayMS);
            _view.InitializeBoard();
        }

        private void OnGameStarted() {
            _view.SetStartButtonEnabled(false);
            _view.SetReStartButtonEnabled(true);
        }

        private void OnStartButtonClick(StartButtonClickEvent evt) {
            _gameController.Start();
        }

        private void ViewClosed(DetachFromPanelEvent evt) {
            UnsubscribeFromGameEvents();
            _view.UnregisterCallback<AttachToPanelEvent>(ViewOpened);
            _view.UnregisterCallback<DetachFromPanelEvent>(ViewClosed);
            _view.UnregisterCallback<StartButtonClickEvent>(OnStartButtonClick);
            _view.UnregisterCallback<RestartButtonClicked>(OnRestartButtonClick);
            _view.UnregisterCallback<PlayerModeClickedEvent>(OnPlayerModeClicked);
        }

        private void UnsubscribeFromGameEvents() {
            _gameEvents.TurnChanged -= OnTurnChanged;
            _gameEvents.GameStarted -= OnGameStarted;
            _gameEvents.PlayerModeChanged -= OnPlayerModeChanged;
            _gameEvents.BeforeRestart -= OnBeforeRestart;
        }

        private void OnTurnChanged(Symbol symbol) {
            _view.UpdateTurnLabel(string.Format(TURN_TEXT_FORMAT, symbol));
        }
    }
}