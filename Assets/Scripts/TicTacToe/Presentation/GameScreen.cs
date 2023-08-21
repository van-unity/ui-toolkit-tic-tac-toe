using System;
using TicTacToe.Domain;
using TicTacToe.Presentation.CustomEvents;
using TicTacToe.Presentation.VisualElementExtensions;
using UnityEngine.UIElements;

namespace TicTacToe.Presentation {
    public class GameScreen : VisualElement {
        private readonly Button _startButton;
        private readonly Button _restartButton;
        private readonly Label _turnLabel;
        private readonly VisualElement _controlPanel;
        private readonly PlayerModeElement _playerXMode;
        private readonly PlayerModeElement _playerOMode;

        public event Action Opened;
        public event Action Closed;
        public event Action StartClicked;
        public event Action RestartClicked;

        public GameScreen(VisualElement boardView, StyleSheet gameScreenStyle, StyleSheet playerModeStyle) {
            this.SetStyle(gameScreenStyle);
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

            _playerXMode = new PlayerModeElement(playerModeStyle);
            _playerXMode.SetSymbol(PlayerSymbol.X.ToString());

            _playerOMode = new PlayerModeElement(playerModeStyle);
            _playerOMode.SetSymbol(PlayerSymbol.O.ToString());

            _playerXMode.RegisterCallback<ClickEvent>(OnPlayerXTypeClicked);
            _playerOMode.RegisterCallback<ClickEvent>(OnPlayerOTypeClicked);

            playerModesContainer.Add(_playerXMode);
            playerModesContainer.Add(_playerOMode);
            hud.Add(playerModesContainer);


            _turnLabel = new Label();
            _turnLabel.AddToClassList("turn-label");
            hud.Add(_turnLabel);

            _startButton = new Button(() => StartClicked?.Invoke()) {
                text = "Start"
            };
            _startButton.AddToClassList("button");

            _restartButton = new Button(() => RestartClicked?.Invoke()) {
                text = "Restart"
            };
            _restartButton.AddToClassList("button");

            this.RegisterCallback<AttachToPanelEvent>(_ => Opened?.Invoke());
            this.RegisterCallback<DetachFromPanelEvent>(_ => Closed?.Invoke());
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
    }
}