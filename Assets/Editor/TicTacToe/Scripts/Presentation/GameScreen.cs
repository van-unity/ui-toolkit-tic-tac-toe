using Editor.TicTacToe.Scripts.Domain;
using Editor.TicTacToe.Scripts.Presentation.CustomEvents;
using Editor.TicTacToe.Scripts.Presentation.VisualElementExtensions;
using UnityEngine.UIElements;

namespace Editor.TicTacToe.Scripts.Presentation {
    public class GameScreen : VisualElement {
        private readonly Button _startButton;
        private readonly Button _restartButton;
        private readonly Label _turnLabel;
        private readonly VisualElement _controlPanel;
        private readonly PlayerModeElement _playerXMode;
        private readonly PlayerModeElement _playerOMode;

        public GameScreen(VisualElement boardView, IStyleSettings styleSettings) {
            this.SetStyle(styleSettings.GameScreenStyle);
            this.AddToClassList("game-screen");

            var hud = new VisualElement();
            hud.AddToClassList("hud");
            this.Add(hud);

            var boardContainer = new VisualElement();
            boardContainer.AddToClassList("grid-part");
            this.Add(boardContainer);

            boardContainer.Add(boardView);


            _controlPanel = new VisualElement();
            _controlPanel.AddToClassList("bottom-part");
            this.Add(_controlPanel);

            var playerModesContainer = new VisualElement();
            playerModesContainer.AddToClassList("player-modes-container");

            _playerXMode = new PlayerModeElement(styleSettings);
            _playerXMode.SetSymbol(PlayerSymbol.X.ToString());

            _playerOMode = new PlayerModeElement(styleSettings);
            _playerOMode.SetSymbol(PlayerSymbol.O.ToString());

            _playerXMode.RegisterCallback<ClickEvent>(OnPlayerXTypeClicked);
            _playerOMode.RegisterCallback<ClickEvent>(OnPlayerOTypeClicked);

            playerModesContainer.Add(_playerXMode);
            playerModesContainer.Add(_playerOMode);
            hud.Add(playerModesContainer);


            _turnLabel = new Label();
            _turnLabel.AddToClassList("turn-label");
            hud.Add(_turnLabel);

            _startButton = new Button(OnStartButtonClick) {
                text = "Start"
            };
            _startButton.AddToClassList("button");

            _restartButton = new Button(OnRestartButtonClick) {
                text = "Restart"
            };
            _restartButton.AddToClassList("button");
        }

        private void OnPlayerXTypeClicked(ClickEvent clickEvent) {
            clickEvent.PreventDefault();
            var playerXTypeClickedEvent = new PlayerModeClickedEvent(PlayerSymbol.X, this);
            this.SendEvent(playerXTypeClickedEvent);
        }

        private void OnPlayerOTypeClicked(ClickEvent clickEvent) {
            var playerOTypeClickedEvent = new PlayerModeClickedEvent(PlayerSymbol.O, this);
            this.SendEvent(playerOTypeClickedEvent);
        }

        public void SetPlayerXMode(string playerXType) {
            _playerXMode.SetMode(playerXType);
        }

        public void SetPlayerOMode(string playerOType) {
            _playerOMode.SetMode(playerOType);
        }

        public void UpdateTurnLabel(string turnText) {
            _turnLabel.text = turnText;
        }

        public void SetStartButtonEnabled(bool isEnabled) {
            if (isEnabled) {
                _controlPanel.Add(_startButton);
                return;
            }

            if (_controlPanel.Contains(_startButton)) {
                _controlPanel.Remove(_startButton);
            }
        }

        public void SetReStartButtonEnabled(bool isEnabled) {
            if (isEnabled) {
                _controlPanel.Add(_restartButton);
                return;
            }

            _controlPanel.Remove(_restartButton);
        }

        private void OnStartButtonClick() {
            var clickEvent = new StartButtonClickEvent(this);
            this.SendEvent(clickEvent);
        }

        private void OnRestartButtonClick() {
            var clickEvent = new RestartButtonClicked(this);
            this.SendEvent(clickEvent);
        }
    }
}