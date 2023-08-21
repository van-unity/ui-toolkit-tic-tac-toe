using TicTacToe.Domain;
using TicTacToe.Presentation.CustomEvents;

namespace TicTacToe.Presentation {
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

            _view.Opened += OnViewOpened;
        }

        private void OnViewOpened() {
            _view.Closed += OnViewClosed;
            _view.StartClicked += OnStartButtonClick;
            _view.RestartClicked += OnRestartButtonClick;
            
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

        private void OnPlayerModeClicked(PlayerModeClickedEvent evt) {
            _gameController.TogglePlayerMode(evt.PlayerSymbol);
        }
        
        private void OnStartButtonClick() {
            _gameController.Start();
        }

        private void OnRestartButtonClick() {
            _gameController.Restart();
        }
        
        private void OnViewClosed() {
            UnsubscribeFromGameEvents();
            _view.Opened -= OnViewOpened;
            _view.Closed -= OnViewClosed;
            _view.StartClicked -= OnStartButtonClick;
            _view.RestartClicked -= OnRestartButtonClick;
            
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