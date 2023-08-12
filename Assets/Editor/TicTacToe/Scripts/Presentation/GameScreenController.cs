using System.Threading.Tasks;
using Editor.TicTacToe.Scripts.Domain;
using Editor.TicTacToe.Scripts.Presentation.CustomEvents;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class GameScreenController {
        private const string TURN_TEXT_FORMAT = "{0} Turn";
        private const string PLAYER_TYPE_FORMAT = "{0}";

        private readonly GameScreen _view;
        private readonly IGameEvents _gameEvents;
        private readonly IGameController _gameController;
        private readonly IGameSettings _gameSettings;

        public GameScreenController(GameScreen view, IGameEvents gameEvents, IGameController gameController,
            IGameSettings gameSettings) {
            _view = view;
            _gameEvents = gameEvents;
            _gameController = gameController;
            _gameSettings = gameSettings;

            _view.RegisterCallback<AttachToPanelEvent>(OnViewOpened);
        }

        private void OnPlayerModeClicked(PlayerModeClickedEvent evt) {
            _gameController.TogglePlayerMode(evt.PlayerSymbol);
        }

        private void OnRestartButtonClick(RestartButtonClicked evt) {
            _gameController.Restart();
        }

        private void OnViewOpened(AttachToPanelEvent evt) {
            _view.RegisterCallback<DetachFromPanelEvent>(OnViewClosed);
            _view.RegisterCallback<StartButtonClickEvent>(OnStartButtonClick);
            _view.RegisterCallback<RestartButtonClicked>(OnRestartButtonClick);
            _view.RegisterCallback<PlayerModeClickedEvent>(OnPlayerModeClicked);
            
            InitializeTheView();
            SubscribeOnGameEvents();
        }

        private void SubscribeOnGameEvents() {
            _gameEvents.GameStarted += OnGameStarted;
            _gameEvents.TurnChanged += OnTurnChanged;
            _gameEvents.PlayerModeChanged += OnPlayerModeChanged;
        }

        private void InitializeTheView() {
            _view.SetStartButtonEnabled(true);
            _view.UpdateTurnLabel(string.Empty);
            _view.SetPlayerXMode(string.Format(PLAYER_TYPE_FORMAT, _gameSettings.PlayerXMode));
            _view.SetPlayerOMode(string.Format(PLAYER_TYPE_FORMAT, _gameSettings.PlayerOMode));
        }

        private void OnPlayerModeChanged(PlayerSymbol playerPlayerSymbol, PlayerMode playerMode) {
            if (playerPlayerSymbol == PlayerSymbol.X) {
                _view.SetPlayerXMode(string.Format(PLAYER_TYPE_FORMAT, playerMode));
                return;
            }

            _view.SetPlayerOMode(string.Format(PLAYER_TYPE_FORMAT, playerMode));
        }

        private void OnGameStarted() {
            _view.SetStartButtonEnabled(false);
            _view.SetReStartButtonEnabled(true);
        }

        private void OnStartButtonClick(StartButtonClickEvent evt) {
            _gameController.Start();
        }

        private void OnViewClosed(DetachFromPanelEvent evt) {
            UnsubscribeFromGameEvents();
            _view.UnregisterCallback<AttachToPanelEvent>(OnViewOpened);
            _view.UnregisterCallback<DetachFromPanelEvent>(OnViewClosed);
            _view.UnregisterCallback<StartButtonClickEvent>(OnStartButtonClick);
            _view.UnregisterCallback<RestartButtonClicked>(OnRestartButtonClick);
            _view.UnregisterCallback<PlayerModeClickedEvent>(OnPlayerModeClicked);
        }

        private void UnsubscribeFromGameEvents() {
            _gameEvents.TurnChanged -= OnTurnChanged;
            _gameEvents.GameStarted -= OnGameStarted;
            _gameEvents.PlayerModeChanged -= OnPlayerModeChanged;
        }

        private void OnTurnChanged(PlayerSymbol playerSymbol) {
            _view.UpdateTurnLabel(string.Format(TURN_TEXT_FORMAT, playerSymbol));
        }
    }
}